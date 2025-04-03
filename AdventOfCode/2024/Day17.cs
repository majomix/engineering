using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day17
{
    /// <summary>
    /// This seems to be a 3-bit computer: its program is a list of 3-bit numbers (0 through 7), like 0,1,2,3. The computer also has three registers
    /// named A, B, and C, but these registers aren't limited to 3 bits and can instead hold any integer.
    ///
    /// The computer knows eight instructions, each identified by a 3-bit number (called the instruction's opcode). Each instruction also reads
    /// the 3-bit number after it as an input; this is called its operand.
    ///
    /// A number called the instruction pointer identifies the position in the program from which the next opcode will be read.
    /// It starts at 0, pointing at the first 3-bit number in the program. Except for jump instructions, the instruction pointer increases by 2 after each
    /// instruction is processed (to move past the instruction's opcode and its operand).
    ///
    /// If the computer tries to read an opcode past the end of the program, it instead halts.
    ///
    /// There are two types of operands; each instruction specifies the type of its operand. The value of a literal operand is the operand itself.
    ///
    /// 1. The adv instruction (opcode 0) performs division.
    /// The numerator is the value in the A register. The denominator is found by raising 2 to the power of the instruction's combo operand.
    /// (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.)
    /// The result of the division operation is truncated to an integer and then written to the A register.
    /// 
    /// 2. The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand, then stores the result in register B.
    ///
    /// 3. The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits), then writes that value
    /// to the B register.
    ///
    /// 4. The jnz instruction (opcode 3) does nothing if the A register is 0. However, if the A register is not zero, it jumps by setting the instruction pointer
    /// to the value of its literal operand; if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
    /// 
    /// 5. The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C, then stores the result in register B.
    /// (For legacy reasons, this instruction reads an operand but ignores it.)
    /// 
    /// 6. The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value.
    /// (If a program outputs multiple values, they are separated by commas.)
    /// 
    /// 7. The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register.
    /// (The numerator is still read from the A register.)
    ///
    /// 8. The cdv instruction (opcode 7) works exactly like the adv instruction except that the result is stored in the C register.
    /// (The numerator is still read from the A register.)
    ///
    /// The Historians' strange device has finished initializing its debugger and is displaying some information about the program it is trying to run.
    ///
    /// Your first task is to determine what the program is trying to output. To do this, initialize the registers to the given values, then run the given program,
    /// collecting any output produced by out instructions. Always join the values produced by out instructions with commas.
    /// </summary>
    public static string RunProgram(string[] input)
    {
        var processor = new ChronospatialCpu(input);

        return processor.RunProgram();
    }

    public static Memory DebugMemory(string[] input)
    {
        var processor = new ChronospatialCpu(input);

        processor.RunProgram();

        return processor.Memory;
    }

    /// <summary>
    ///
    /// </summary>
    public static int Task2(string[] input)
    {
        return 0;
    }

}

public class Day17xxx
{
    public string RunPart(string[] input)
    {
        long[] regs = new long[3];
        for (int i = 0; i < 3; i++)
        {
            regs[i] = long.Parse(input[i].Split(' ')[2]);
        }


        char[] s = new char[] { ' ', ',' };
        int[] program = input[4].Split(s).Skip(1).Select(int.Parse).ToArray();

        return string.Join(',', RunProgram(program, regs).Select(x => x.ToString()));
    }

    // Runs the given program with the given registers, makes a local copy of the registers to not update values globally.
    private List<long> RunProgram(int[] program, long[] regs)
    {
        long[] localRegs = (long[])regs.Clone();
        int pointer = 0;
        List<long> output = new();
        while (pointer < program.Length)
        {
            int operand = program[pointer + 1];
            pointer = PerformOpCode((OpCode)program[pointer], operand, localRegs, pointer, output);
        }

        return output;
    }

    private int PerformOpCode(OpCode opcode, int operand, long[] regs, int pointer, List<long> output)
    {
        switch (opcode)
        {
            case OpCode.ADV:
                long numerator = regs[0];
                long denominator = (long)Math.Pow(2, ComboOperand(operand, regs));
                regs[0] = numerator / denominator;
                break;

            case OpCode.BXL:
                regs[1] = regs[1] ^ operand;
                break;

            case OpCode.BST:
                regs[1] = ComboOperand(operand, regs) % 8;
                break;

            case OpCode.JNZ:
                if (regs[0] == 0) break;
                pointer = operand;
                return pointer;

            case OpCode.BXC:
                regs[1] = regs[1] ^ regs[2];
                break;

            case OpCode.OUT:
                output.Add(ComboOperand(operand, regs) % 8);
                break;

            case OpCode.BDV:
                numerator = regs[0];
                denominator = (long)Math.Pow(2, ComboOperand(operand, regs));
                regs[1] = numerator / denominator;
                break;

            case OpCode.CDV:
                numerator = regs[0];
                denominator = (long)Math.Pow(2, ComboOperand(operand, regs));
                regs[2] = numerator / denominator;
                break;
        }

        return pointer + 2;
    }

    private long ComboOperand(int value, long[] regs)
    {
        return value switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => regs[0],
            5 => regs[1],
            6 => regs[2],
            _ => throw new ArgumentException($"Not a valid operand: {value}")
        };
    }

    private enum OpCode
    {
        ADV = 0,
        BXL = 1,
        BST = 2,
        JNZ = 3,
        BXC = 4,
        OUT = 5,
        BDV = 6,
        CDV = 7
    }
}

public class ChronospatialCpu
{
    private readonly Memory _memory;
    private readonly ChronospatialProgram _program;
    private readonly Dictionary<int, Instruction> _instructionSet;

    private int _instructionPointer;

    public Memory Memory => _memory;

    public ChronospatialCpu(string[] input)
    {
        _memory = new Memory(input);
        _program = new ChronospatialProgram(input[4]);
        _instructionSet = new()
        {
            { 0, new AdvInstruction() },
            { 1, new BxlInstruction() },
            { 2, new BstInstruction() },
            { 3, new JnzInstruction() },
            { 4, new BxcInstruction() },
            { 5, new OutInstruction() },
            { 6, new BdvInstruction() },
            { 7, new CdvInstruction() }
        };
    }

    public string RunProgram()
    {
        while (_instructionPointer < _program.Program.Length)
        {
            var opCode = _program.Program[_instructionPointer++];
            _instructionSet[opCode].Execute(_memory, _program, ref _instructionPointer);
        }

        return string.Join(",", _memory.StandardOutput);
    }
}

public class ChronospatialProgram
{
    public int[] Program { get; set; }

    public ChronospatialProgram(string line)
    {
        Program = ParseInstructions(line);
    }

    private int[] ParseInstructions(string line)
    {
        var split = line.Split(':', StringSplitOptions.TrimEntries);

        var instructionCodes = split[1].Split(',', StringSplitOptions.TrimEntries);

        return instructionCodes.Select(int.Parse).ToArray();
    }
}

public class Memory
{
    public int RegisterA { get; set; }
    public int RegisterB { get; set; }
    public int RegisterC { get; set; }
    public List<int> StandardOutput { get; } = new();

    public Memory(string[] input)
    {
        RegisterA = ParseRegisterValue(input[0]);
        RegisterB = ParseRegisterValue(input[1]);
        RegisterC = ParseRegisterValue(input[2]);
    }

    private int ParseRegisterValue(string line)
    {
        var split = line.Split(':', StringSplitOptions.TrimEntries);

        return int.Parse(split[1]);
    }
}

public interface Instruction
{
    void Execute(Memory memory, ChronospatialProgram program, ref int instructionPointer);
}

public abstract class BaseInstruction : Instruction
{
    public void Execute(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        if (instructionPointer < program.Program.Length)
        {
            ExecuteInternal(memory, program, ref instructionPointer);
        }
    }

    protected int ReadLiteralOperand(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        return program.Program[instructionPointer++];
    }

    protected int ReadComboOperand(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        var operand = program.Program[instructionPointer++];

        if (operand is >= 0 and <= 3)
            return operand;

        if (operand == 4)
            return memory.RegisterA;

        if (operand == 5)
            return memory.RegisterB;

        if (operand == 6)
            return memory.RegisterC;

        if (operand == 7)
            throw new Exception("Reserved");

        throw new Exception("Unknown combo operand");
    }

    protected abstract void ExecuteInternal(Memory memory, ChronospatialProgram program, ref int instructionPointer);
}

public class AdvInstruction : BaseInstruction
{
    protected override void ExecuteInternal(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        var operand = ReadComboOperand(memory, program, ref instructionPointer);
        var denominator = Math.Pow(2, operand);

        memory.RegisterA = (int)(memory.RegisterA / denominator);
    }
}

public class BxlInstruction : BaseInstruction
{
    protected override void ExecuteInternal(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        var operand = ReadLiteralOperand(memory, program, ref instructionPointer);

        memory.RegisterB ^= operand;
    }
}

public class BstInstruction : BaseInstruction
{
    protected override void ExecuteInternal(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        var operand = ReadComboOperand(memory, program, ref instructionPointer);

        memory.RegisterB = operand % 8;
    }
}

public class JnzInstruction : BaseInstruction
{
    protected override void ExecuteInternal(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        if (memory.RegisterA == 0)
            return;

        var operand = ReadLiteralOperand(memory, program, ref instructionPointer);
        instructionPointer = operand;
    }
}

public class BxcInstruction : BaseInstruction
{
    protected override void ExecuteInternal(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        _ = ReadLiteralOperand(memory, program, ref instructionPointer);
        memory.RegisterB ^= memory.RegisterC;
    }
}

public class OutInstruction : BaseInstruction
{
    protected override void ExecuteInternal(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        var operand = ReadComboOperand(memory, program, ref instructionPointer);

        memory.StandardOutput.Add(operand % 8);
    }
}

public class BdvInstruction : BaseInstruction
{
    protected override void ExecuteInternal(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        var operand = ReadComboOperand(memory, program, ref instructionPointer);
        var denominator = Math.Pow(2, operand);

        memory.RegisterB = (int)(memory.RegisterA / denominator);
    }
}

public class CdvInstruction : BaseInstruction
{
    protected override void ExecuteInternal(Memory memory, ChronospatialProgram program, ref int instructionPointer)
    {
        var operand = ReadComboOperand(memory, program, ref instructionPointer);
        var denominator = Math.Pow(2, operand);

        memory.RegisterC = (int)(memory.RegisterA / denominator);
    }
}

[TestFixture]
internal class Day17Tests
{
    [Test]
    public void Day17Task1Example1()
    {
        string[] input =
        {
            "Register A: 729",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 0,1,5,4,3,0"
        };

        Day17.RunProgram(input).Should().Be("4,6,3,5,6,3,5,2,1,0");
    }

    [Test]
    public void Day17Task1Example2()
    {
        string[] input =
        {
            "Register A: 0",
            "Register B: 0",
            "Register C: 9",
            "",
            "Program: 2,6"
        };

        Day17.DebugMemory(input).RegisterB.Should().Be(1);
    }

    [Test]
    public void Day17Task1Example3()
    {
        string[] input =
        {
            "Register A: 10",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 5,0,5,1,5,4"
        };

        Day17.RunProgram(input).Should().Be("0,1,2");
    }

    [Test]
    public void Day17Task1Example4()
    {
        string[] input =
        {
            "Register A: 2024",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 0,1,5,4,3,0"
        };

        Day17.RunProgram(input).Should().Be("4,2,5,6,7,7,7,7,3,1,0");
    }

    [Test]
    public void Day17Task1Example5()
    {
        string[] input =
        {
            "Register A: 0",
            "Register B: 29",
            "Register C: 0",
            "",
            "Program: 1,7"
        };

        Day17.DebugMemory(input).RegisterB.Should().Be(26);
    }

    [Test]
    public void Day17Task1Example7()
    {
        string[] input =
        {
            "Register A: 0",
            "Register B: 2024",
            "Register C: 43690",
            "",
            "Program: 4,0"
        };

        Day17.DebugMemory(input).RegisterB.Should().Be(44354);
    }

    [Test]
    public void Day17Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day17.txt");
        var result = Day17.RunProgram(input);

        result.Should().Be("5,4,1,2,7,1,5,5,1");
    }

    [Test]
    public void Day17Task2Example()
    {
        string[] input =
        {
        };

        Day17.Task2(input).Should().Be(0);
    }

    [Test]
    public void Day17Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day17.txt");
        var result = Day17.Task2(input);
        result.Should().Be(0);
    }
}
