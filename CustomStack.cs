using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomStack_class
{
    class CustomStack<T> : IEnumerable<T>
    {
        public const int InitialCapasity = 4;

        public CustomStack()
        {
            this.Items = new T[InitialCapasity];
            this.Count = 0;
        }

        public T[] Items { get; private set; }
        public int Count { get; private set; }

        public void Push(T element)
        {
            EnsureCapacity();

            var lastIndex = Count;
            Items[lastIndex] = element;

            Count++;
        }

        public T Pop()
        {
            ThrowWhenEmpty();

            if (Count <= 0)
            {
                throw new IndexOutOfRangeException();
            }

            var removedElement = Items[Count - 1];

            Count--;

            Items[Count] = default;

            Shrink();

            return removedElement;
        }

        public T Peek()
        {
            ThrowWhenEmpty();

            var lastElement = Items[Count - 1];

            return lastElement;
        }

        public void ForEach(Action<T> action)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                action(Items[i]);
            }
        }

        private void ThrowWhenEmpty()
        {
            if (Count == 0)
            {
                throw new Exception("Stack is empty.");
            }
        }

        public void EnsureCapacity()
        {
            if (Count == Items.Length)
            {
                var tempArray = new T[Items.Length * 2];

                Array.Copy(Items, tempArray, Items.Length);

                Items = tempArray;

            }

        }
        public void Shrink()
        {
            if (Count * 4 >= Items.Length)
            {
                return;
            }

            var tempArray = new T[Items.Length / 4];

            Array.Copy(Items, tempArray, Count);

            Items = tempArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count-1; i >= 0; i--)
            {
                yield return this.Items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }
    }
}
