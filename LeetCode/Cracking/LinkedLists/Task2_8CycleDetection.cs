using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.LinkedLists
{
    /// <summary>
    /// Given a linked list which might contain a loop, implement an algorithm that detects the loop.
    ///
    /// Solutions:
    /// * hashset
    /// * tortoise & hare
    /// </summary>
    internal class Task2_8CycleDetection
    {
        public bool ContainsCycleThroughHashSet(CrackingLinkedListNode? head)
        {
            var hashSet = new HashSet<CrackingLinkedListNode>();

            var current = head;
            
            while (current != null)
            {
                if (!hashSet.Add(current))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public bool ContainsCycleThroughFloyd(CrackingLinkedListNode? head)
        {
            var tortoise = head;
            var hare = head;

            while (hare != null && hare.Next != null)
            {
                tortoise = tortoise!.Next;
                hare = hare.Next.Next;

                if (tortoise == hare)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Part 2: Given a linked list which might contain a loop, implement an algorithm that returns the node at the beginning of the loop.
        /// </summary>
        public CrackingLinkedListNode? GetBeginningOfCycleThroughHashSet(CrackingLinkedListNode? head)
        {
            var hashSet = new HashSet<CrackingLinkedListNode>();

            var current = head;

            while (current != null)
            {
                if (!hashSet.Add(current))
                {
                    return current;
                }

                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// For every p steps of tortoise, hare takes 2p steps.
        /// When tortoise enters the loop after k steps, hare has taken 2k steps since beginning.
        /// Since hare has taken 2k steps and it took tortoise k steps to reach beginning of the loop, hare must be k steps into the loop (2k - k).
        /// Since k may be larger than loop size, let's denote that hare has taken K = k % loopSize steps inside the loop.
        ///
        /// !!! At the point when tortoise enters the loop, tortoise is at element 0 of the loop and hare is at element K of the loop.
        /// Thus, tortoise is K steps behind hare and hare is loopSize - K steps behind tortoise.
        /// Hare catches up to tortoise at pace 1 step per unit of time.
        ///
        /// If hare is loopSize - K steps behind tortoise and catches up at rate 1 step per unit of time, they will meet after loopSize - K steps.
        /// If tortoise is at element 0, after loopSize - K steps both tortoise and hare will be K steps before the element 0.
        /// This is the collision spot.
        ///
        /// Because k = K + m * loopSize for any integer m, it is also correct to say that the collision spot is k steps from the loop start.
        /// Therefore, both collision spot and head of the linked list are equally k nodes away from the start of the loop.
        ///
        /// If we keep one pointer at collision spot, move other pointer to head of the linked list and advance both at the same pace of 1 step,
        /// they will both meet at the start of the loop.
        /// </summary>
        public CrackingLinkedListNode? GetBeginningOfCycleThroughFloyd(CrackingLinkedListNode? head)
        {
            var tortoise = head;
            var hare = head;
            CrackingLinkedListNode? collisionSpot = null;

            // find collision spot
            while (hare != null && hare.Next != null)
            {
                tortoise = tortoise!.Next;
                hare = hare.Next.Next;

                if (tortoise == hare)
                {
                    collisionSpot = hare;
                    break;
                }
            }

            // no collision spot means no cycle
            if (collisionSpot == null)
                return null;

            // reset pointers according to description of the algorithm
            tortoise = head;
            hare = collisionSpot;
            while (tortoise != hare)
            {
                tortoise = tortoise!.Next;
                hare = hare!.Next;
            }

            return tortoise;
        }
    }

    [TestFixture]
    public class Task2_8CycleDetectionTests
    {
        [Test]
        public void ContainsCycleThroughHashSet_ContainsCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var nodeFive = list.Next!.Next!.Next!.Next!.Next!;
            var nodeNine = nodeFive.Next!.Next!.Next!.Next!;
            nodeNine.Next = nodeFive;

            // act
            var result = sut.ContainsCycleThroughHashSet(list);

            // assert
            result.Should().BeTrue();
        }


        [Test]
        public void ContainsCycleThroughHashSet_DoesNotContainCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            // act
            var result = sut.ContainsCycleThroughHashSet(list);

            // assert
            result.Should().BeFalse();
        }

        [Test]
        public void ContainsCycleThroughFloyd_ContainsCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var nodeFive = list.Next!.Next!.Next!.Next!.Next!;
            var nodeNine = nodeFive.Next!.Next!.Next!.Next!;
            nodeNine.Next = nodeFive;

            // act
            var result = sut.ContainsCycleThroughFloyd(list);

            // assert
            result.Should().BeTrue();
        }


        [Test]
        public void ContainsCycleThroughFloyd_DoesNotContainCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            // act
            var result = sut.ContainsCycleThroughFloyd(list);

            // assert
            result.Should().BeFalse();
        }

        [Test]
        public void GetBeginningOfCycleThroughHashSet_ContainsCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var nodeFive = list.Next!.Next!.Next!.Next!.Next!;
            var nodeNine = nodeFive.Next!.Next!.Next!.Next!;
            nodeNine.Next = nodeFive;

            // act
            var result = sut.GetBeginningOfCycleThroughHashSet(list);

            // assert
            result.Should().Be(nodeFive);
        }

        [Test]
        public void GetBeginningOfCycleThroughHashSet_DoesNotContainCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            // act
            var result = sut.GetBeginningOfCycleThroughHashSet(list);

            // assert
            result.Should().BeNull();
        }

        [Test]
        public void GetBeginningOfCycleThroughFloyd_ContainsCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var nodeFive = list.Next!.Next!.Next!.Next!.Next!;
            var nodeNine = nodeFive.Next!.Next!.Next!.Next!;
            nodeNine.Next = nodeFive;

            // act
            var result = sut.GetBeginningOfCycleThroughFloyd(list);

            // assert
            result.Should().Be(nodeFive);
        }

        [Test]
        public void GetBeginningOfCycleThroughFloyd_DoesNotContainCycle()
        {
            // arrange
            var sut = new Task2_8CycleDetection();
            var list = CrackingLinkedListProvider.CreateLinkedListWithContent(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            // act
            var result = sut.GetBeginningOfCycleThroughFloyd(list);

            // assert
            result.Should().BeNull();
        }
    }
}
