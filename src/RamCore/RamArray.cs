using System.Collections;

namespace RamCore
{
    public class RamArray<T> : IEnumerable<T>, IEnumerator<T>
    {
        private T[] array;
        private int arraySize;
        private const int defaultArraySizeCapacity = 2_000;
        private int currentIndex;
        private bool endOfItem;
        private bool beginOfItem;
        private int capacity => array.Length;

        public RamArray(int capacity)
        {
            array = new T[capacity];
            //arraySize = capacity;
            Reset();
        }

        public RamArray() : this(defaultArraySizeCapacity)
        {
        }

        public T Current
        {
            get
            {
                if (currentIndex == -1)
                {
                    currentIndex = 0;
                }
                else if (currentIndex == arraySize)
                {
                    currentIndex = arraySize - 1;
                }
                return array[currentIndex];
            }
        }


        public void Reset()
        {
            currentIndex = -1;
            endOfItem = false;
            beginOfItem = true;
        }

        object IEnumerator.Current => Current;

        public T this[int index] => array[currentIndex]; // for example list[10]

        private void SetCurrent(int increase)
        {
            currentIndex += increase;
            endOfItem = currentIndex > 0 && currentIndex >= arraySize - 1;
            beginOfItem = currentIndex == 0;
        }

        private void SetIndex(int index)
        {
            currentIndex = 0;
            SetCurrent(index);
        }

        public bool NextItem()
        {
            if (!endOfItem)
            {
                SetCurrent(1);
                return true;
            }

            GoToEnd();
            return false;
        }

        public bool PreviousItem()
        {
            if (!beginOfItem)
            {
                SetCurrent(-1);
                return true;
            }
            GoToStart();
            return false;
        }

        public void GoToStart()
        {
            Reset();
        }
        public void GoToEnd()
        {

            SetIndex(arraySize);
        }

        public void LoadFromArray(T[] array)
        {
            Load(array);
        }

        private void Load(T[] newArray)
        {
            ArgumentNullException.ThrowIfNull(newArray);
            array = newArray;
            arraySize = array.Length;
            Reset();

        }

        private void ReSize(int size)
        {
            Array.Resize(ref array, size);
        }

        public static implicit operator RamArray<T>(T[] array)
        {
            var q = new RamArray<T>();
            q.LoadFromArray(array);
            return q;
        }
        //   public static implicit operator 


        public IEnumerator<T> GetEnumerator()
        {
            GoToStart();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            return NextItem();
        }

        public int Add(T item)
        {

            if (arraySize == capacity)
            {
                ReSize(arraySize * 2);
            }

            array[arraySize] = item;
            arraySize++;
            return arraySize;
        }

        public int AddRange(T[] items)
        {
            if (arraySize + items.Length > capacity)
            {
                ReSize(capacity * 2);
            }

            for (int i = 0; i < items.Length; i++)
            {
                array[arraySize] = items[i];
                arraySize++;
            }

            return arraySize;
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < arraySize - 1; i++)
            {
                array[i] = array[i + 1];
            }

            arraySize--;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(array);
        }
    }
}