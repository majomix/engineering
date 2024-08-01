using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.StacksAndQueues
{
    /// <summary>
    /// Implement three stacks using single array.
    ///
    /// Solutions:
    /// * split by thirds
    /// * adaptive positioning
    /// </summary>
    internal class PreallocatedIntegerMultiStack
    {
        private readonly int _numberOfStacks;
        private readonly int _stackSize;
        private readonly int[] _array;
        private readonly int[] _indices;

        public PreallocatedIntegerMultiStack(int numberOfStacks, int stackSize)
        {
            _numberOfStacks = numberOfStacks;
            _stackSize = stackSize;
            _array = new int[numberOfStacks * stackSize];
            _indices = new int[numberOfStacks];
        }

        public void Push(int stackIndex, int value)
        {
            EnsureStackIndexExists(stackIndex);

            if (IsFull(stackIndex))
                throw new InvalidOperationException($"Stack {stackIndex} full!");

            var index = GetIndex(stackIndex);
            _array[++index] = value;
            ++_indices[stackIndex];
        }

        public int Pop(int stackIndex)
        {
            EnsureStackIndexExists(stackIndex);

            if (IsEmpty(stackIndex))
                throw new InvalidOperationException($"Stack {stackIndex} empty!");

            var index = GetIndex(stackIndex);
            var value = _array[index];
            --_indices[stackIndex];

            return value;
        }

        public bool IsFull(int stackIndex)
        {
            EnsureStackIndexExists(stackIndex);

            return _indices[stackIndex] == _stackSize;
        }

        public bool IsEmpty(int stackIndex)
        {
            EnsureStackIndexExists(stackIndex);

            return _indices[stackIndex] == 0;
        }

        private void EnsureStackIndexExists(int stackIndex)
        {
            if (stackIndex >= _numberOfStacks)
                throw new InvalidOperationException($"Stack #{stackIndex} does not exist!");
        }

        private int GetIndex(int stackIndex)
        {
            return stackIndex * _stackSize + _indices[stackIndex];
        }
    }


    [TestFixture]
    public class Task3_1TrippleStackTests
    {
        [Test]
        public void Stack_PushPopPeekCount()
        {
            // arrange
            var stack = new PreallocatedIntegerMultiStack(3, 10);

            // act
            stack.Push(0, 1);
            stack.Push(0, 2);
            stack.Push(0, 3);
            stack.Push(1, 4);
            stack.Push(1, 5);
            stack.Push(1, 6);
            stack.Push(2, 7);
            stack.Push(2, 8);
            stack.Push(2, 9);

            // assert
            stack.Pop(2).Should().Be(9);
            stack.Pop(2).Should().Be(8);
            stack.Pop(2).Should().Be(7);
            stack.Pop(1).Should().Be(6);
            stack.Pop(1).Should().Be(5);
            stack.Pop(1).Should().Be(4);
            stack.Pop(0).Should().Be(3);
            stack.Pop(0).Should().Be(2);
            stack.Pop(0).Should().Be(1);
        }

        [Test]
        public void Stack_RepeatedPushPop_Count()
        {
            // arrange
            var stack = new PreallocatedIntegerMultiStack(3, 10);
            stack.Push(0, 1);
            stack.Pop(0);
            stack.Push(0, 2);
            stack.Pop(0);
            stack.Push(0, 3);
            stack.Push(0, 4);

            // act
            var finalPop = stack.Pop(0);

            // assert
            finalPop.Should().Be(4);
        }
    }
}
