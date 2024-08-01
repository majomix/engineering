using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.StacksAndQueues
{
    /// <summary>
    /// Implement a data structure SetOfStacks which should be composed of several stacks and should create a new stack once the previous one exceeds capacity.
    /// Push and pop should behave identically to a single stack.
    ///
    /// Solutions:
    /// * store a stack of small stacks
    /// </summary>
    internal class SetOfStacks
    {
        private readonly int _capacity;
        private readonly Stack<Stack<int>> _stacks = new();

        public SetOfStacks(int capacity)
        {
            _capacity = capacity;
        }

        public void Push(int value)
        {
            _stacks.TryPeek(out var stackToUse);

            if (stackToUse == null || stackToUse.Count == _capacity)
            {
                stackToUse = new Stack<int>();
                _stacks.Push(stackToUse);
            }

            stackToUse.Push(value);
        }

        public int Pop()
        {
            var stackToUse = _stacks.Peek();

            var value = stackToUse.Pop();

            if (stackToUse.Count == 0)
            {
                _stacks.Pop();
            }

            return value;
        }
    }

    [TestFixture]
    internal class Task3_3StackOfPlatesTests
    {
        [Test]
        public void SetOfStacks_PushPopPeekCount()
        {
            // arrange
            var stack = new SetOfStacks(3);

            // act
            stack.Push(9);
            stack.Push(8);
            stack.Push(7);
            stack.Push(6);
            stack.Push(5);
            stack.Push(4);
            stack.Push(3);
            stack.Push(2);
            stack.Push(1);

            // assert
            stack.Pop().Should().Be(1);
            stack.Pop().Should().Be(2);
            stack.Pop().Should().Be(3);
            stack.Pop().Should().Be(4);
            stack.Pop().Should().Be(5);
            stack.Pop().Should().Be(6);
            stack.Pop().Should().Be(7);
            stack.Pop().Should().Be(8);
            stack.Pop().Should().Be(9);
        }
    }
}
