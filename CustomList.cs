﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomList_class
{
    class CustomList<T>
    {
        public const int InitialCapasity = 2;

        public CustomList()
        {
            this.Items = new T[InitialCapasity];
            this.Count = 0;
        }
        public T[] Items { get; private set; }

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                if (index >= this.Count || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                return this.Items[index];
            }
            set
            {
                if (index >= this.Count || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                this.Items[index] = value;
            }
        }

        public void Add(T element)
        {
            EnsureCapacity();

            var lastIndex = Count;
            Items[lastIndex] = element;

            Count++;
        }

        public T RemoveAt(int index)
        {
            if (IsNotValidIndex(index))
            {
                throw new IndexOutOfRangeException();
            }

            var removedElement = Items[index];

            for (int i = index; i < Count - 1; i++)
            {
                Items[i] = Items[i + 1];
            }

            Count--;

            Items[Count] = default;

            Shrink();

            return removedElement;
        }

        public void Insert(int index, T item)
        {
            if (IsNotValidIndex(index))
            {
                throw new IndexOutOfRangeException();
            }

            EnsureCapacity();

            for (int i = Count - 1; i >= index; i--)
            {
                Items[i + 1] = Items[i];
            }

            Items[index] = item;
        }

        public bool Contains(T element)
        {
            return Items.Any(x => x.Equals(element));
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            if (IsNotValidIndex(firstIndex) || IsNotValidIndex(secondIndex))
            {
                throw new IndexOutOfRangeException();
            }

            var firstElement = Items[firstIndex];

            Items[firstIndex] = Items[secondIndex];

            Items[secondIndex] = firstElement;
        }

        private bool IsNotValidIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                return true;
            }

            return false;
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
    }
}
