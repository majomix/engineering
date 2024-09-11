using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.StacksAndQueues
{
    /// <summary>
    /// Sort a stack such that the smallest items are on the top.
    /// You can use an additional temporary stack, but you may not copy the elements into any other data structure.
    /// The stack supports push, pop, peek and isEmpty.
    ///
    /// Discussion:
    /// Use second stack to sort items descending. Then copy them back to the original stack.
    /// Use original stack as temporary storage.
    ///
    /// 1. S1: T 8 2 9 1 5; S2: T
    /// 2. S1: T 2 9 1 5; S2: T 8
    /// 3. S1: T 8 9 1 5; S2: T 2
    /// 4. S1: T 9 1 5; S2: T 8 2
    /// 5. S1: T 1 5; S2: T 9 8 2
    /// 6. S1: T 2 8 9 5; S2: T 1
    /// 7. S1: T 5; S2: T 9 8 2 1
    /// 8. S1: T 8 9; S2: T 5 2 1
    /// 9. S1: T; S2: T 9 8 5 2 1
    /// 10. S1: T 1 2 5 8 9
    /// </summary>
    internal class Task3_5SortStack
    {
        public Stack<int> SortStack(Stack<int> stack)
        {
            var temporaryStack = new Stack<int>();

            while (stack.Count != 0)
            {
                var itemToMove = stack.Pop();
                while (temporaryStack.Count != 0 && temporaryStack.Peek() > itemToMove)
                {
                    stack.Push(temporaryStack.Pop());
                }

                temporaryStack.Push(itemToMove);
            }

            while (temporaryStack.Count != 0)
            {
                stack.Push(temporaryStack.Pop());
            }

            return stack;
        }
    }

    [TestFixture]
    internal class Task3_5SortStackTests
    {
        [Test]
        public void SortStackTest()
        {
            // arrange
            var sut = new Task3_5SortStack();
            var stack = new Stack<int>();
            stack.Push(5);
            stack.Push(1);
            stack.Push(9);
            stack.Push(2);
            stack.Push(8);

            // act
            var sortedStack = sut.SortStack(stack);

            // assert
            sortedStack.Should().BeInAscendingOrder();
        }
    }
}
