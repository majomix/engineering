using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.StacksAndQueues
{
    /// <summary>
    /// An animal shelter which holds only dogs and cats operates on a strictly FIFO basis.
    /// People must adopt either the oldest of all animals or the oldest dog, or the oldest cat.
    ///
    /// Create the data structures to maintain this system and implement operations enqueue, dequeue, dequeueCat and dequeueDog.
    /// You may use built-in LinkedList data structure.
    /// </summary>
    internal class AnimalShelter
    {
        private readonly LinkedList<ShelterDog> _dogs = new();
        private readonly LinkedList<ShelterCat> _cats = new();

        public void Enqueue(ShelterAnimal animal)
        {
            animal.EnterShelter(this);
        }

        internal void EnqueueDog(ShelterDog dog)
        {
            dog.MarkEntrance(DateTime.Now);

            _dogs.AddLast(dog);
        }

        internal void EnqueueCat(ShelterCat cat)
        {
            cat.MarkEntrance(DateTime.Now);

            _cats.AddLast(cat);
        }

        public ShelterAnimal Dequeue()
        {
            var oldestCat = _cats.First;
            var oldestDog = _dogs.First;

            if (oldestCat == null)
            {
                return oldestDog!.Value;
            }

            if (oldestDog == null)
            {
                return oldestCat.Value;
            }

            if (oldestCat.Value.IsInShelterLongerThan(oldestDog.Value))
            {
                return DequeueCat();
            }

            return DequeueDog();
        }

        public ShelterCat DequeueCat()
        {
            var cat = _cats.First;

            _cats.RemoveFirst();

            return cat!.Value;
        }

        public ShelterDog DequeueDog()
        {
            var dog = _dogs.First;

            _dogs.RemoveFirst();

            return dog!.Value;
        }
    }

    internal abstract class ShelterAnimal
    {
        private DateTime _enteredShelterAt;

        public abstract void EnterShelter(AnimalShelter shelter);

        public void MarkEntrance(DateTime dateTime)
        {
            _enteredShelterAt = dateTime;
        }

        public bool IsInShelterLongerThan(ShelterAnimal? otherShelterAnimal)
        {
            return otherShelterAnimal == null || _enteredShelterAt.Ticks < otherShelterAnimal._enteredShelterAt.Ticks;
        }
    }

    internal class ShelterDog : ShelterAnimal
    {
        public override void EnterShelter(AnimalShelter shelter)
        {
            shelter.EnqueueDog(this);
        }
    }

    internal class ShelterCat : ShelterAnimal
    {
        public override void EnterShelter(AnimalShelter shelter)
        {
            shelter.EnqueueCat(this);
        }
    }

    [TestFixture]
    internal class Task3_6AnimalShelterTests
    {
        [Test]
        public void AnimalShelterTest()
        {
            // arrange
            var shelter = new AnimalShelter();
            var cat1 = new ShelterCat();
            var cat2 = new ShelterCat();
            var cat3 = new ShelterCat();
            var cat4 = new ShelterCat();
            var cat5 = new ShelterCat();
            var dog1 = new ShelterDog();
            var dog2 = new ShelterDog();
            var dog3 = new ShelterDog();
            var dog4 = new ShelterDog();
            var dog5 = new ShelterDog();

            // act
            shelter.Enqueue(dog1);
            shelter.Enqueue(cat1);
            shelter.Enqueue(cat2);
            shelter.Enqueue(cat3);
            shelter.Enqueue(cat4);
            shelter.Enqueue(dog2);
            shelter.Enqueue(dog3);
            shelter.Enqueue(dog4);
            shelter.Enqueue(dog5);
            shelter.Enqueue(cat5);

            // assert
            shelter.Dequeue().Should().Be(dog1);
            shelter.Dequeue().Should().Be(cat1);
            shelter.DequeueDog().Should().Be(dog2);
            shelter.DequeueCat().Should().Be(cat2);
            shelter.DequeueCat().Should().Be(cat3);
            shelter.DequeueCat().Should().Be(cat4);
            shelter.Dequeue().Should().Be(dog3);
            shelter.Dequeue().Should().Be(dog4);
            shelter.Dequeue().Should().Be(dog5);
            shelter.Dequeue().Should().Be(cat5);
        }
    }
}
