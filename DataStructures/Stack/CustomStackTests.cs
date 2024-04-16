using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Stack
{
    [TestFixture]
    internal class CustomStackTests
    {
        [Test]
        public void Stack_PushPopPeekCount()
        {
            // arrange
            var stack = new CustomStack<int>();

            // act
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);
            stack.Push(7);
            stack.Push(8);
            stack.Push(9);

            // assert
            stack.Count.Should().Be(9);
            stack.Pop().Should().Be(9);
            stack.Pop().Should().Be(8);
            stack.Pop().Should().Be(7);
            stack.Pop().Should().Be(6);
            stack.Pop().Should().Be(5);
            stack.Pop().Should().Be(4);
            stack.Peek().Should().Be(3);
            stack.Peek().Should().Be(3);
            stack.Pop().Should().Be(3);
            stack.Pop().Should().Be(2);
            stack.Pop().Should().Be(1);

            stack.Count.Should().Be(0);
            stack.Peek().Should().Be(default);
            stack.Pop().Should().Be(default);
        }

        [Test]
        public void Stack_Push_Clear_Count()
        {
            // arrange
            var stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);

            // act
            stack.Clear();
            
            // assert
            stack.Count.Should().Be(0);
        }
    }
}
