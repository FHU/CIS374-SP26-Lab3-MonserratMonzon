using System;

namespace Lab3;

public class MaxHeap<T> where T: IComparable<T>
{
    private T[] array;
    private const int initialSize = 8;

    public int Count { get; private set; }

    public int Capacity => array.Length;

    public bool IsEmpty => Count == 0;

    public MaxHeap(T[] initialArray = null)
    {
        array = new T[initialSize];
        Count = 0;

        if (initialArray == null)
            return;

        foreach (var item in initialArray)
        {
            Add(item);
        }
    }

    public T Peek()
    {
        if (IsEmpty)
            throw new Exception("Heap is empty.");

        return array[0];
    }

    public void Add(T item)
    {
        if (Count == array.Length)
            DoubleArrayCapacity();

        array[Count] = item;
        TrickleUp(Count);
        Count++;
    }

    public T ExtractMax()
    {
        if (IsEmpty)
            throw new Exception("Heap is empty.");

        T result = array[0];
        Count--;
        array[0] = array[Count];
        if (!IsEmpty)
            TrickleDown(0);

        return result;
    }

    public T ExtractMin()
    {
        if (IsEmpty)
            throw new Exception("Heap is empty.");

        int minIndex = 0;
        for (int i = 1; i < Count; i++)
        {
            if (array[i].CompareTo(array[minIndex]) < 0)
                minIndex = i;
        }

        T result = array[minIndex];
        Count--;
        array[minIndex] = array[Count];

        if (minIndex < Count)
        {
            TrickleDown(minIndex);
            TrickleUp(minIndex);
        }

        return result;
    }

    public bool Contains(T value)
    {
        for (int i = 0; i < Count; i++)
        {
            if (array[i].Equals(value))
                return true;
        }

        return false;
    }

    public void Update(T oldValue, T newValue)
    {
        int index = -1;
        for (int i = 0; i < Count; i++)
        {
            if (array[i].Equals(oldValue))
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            throw new Exception("Value not found in heap.");

        array[index] = newValue;
        TrickleDown(index);
        TrickleUp(index);
    }

    public void Remove(T value)
    {
        int index = -1;
        for (int i = 0; i < Count; i++)
        {
            if (array[i].Equals(value))
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            throw new Exception("Value not found in heap.");

        Count--;
        array[index] = array[Count];

        if (index < Count)
        {
            TrickleDown(index);
            TrickleUp(index);
        }
    }

    private void TrickleUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = Parent(index);
            if (array[index].CompareTo(array[parentIndex]) <= 0)
                break;

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void TrickleDown(int index)
    {
        while (true)
        {
            int left = LeftChild(index);
            int right = RightChild(index);
            int largest = index;

            if (left < Count && array[left].CompareTo(array[largest]) > 0)
                largest = left;

            if (right < Count && array[right].CompareTo(array[largest]) > 0)
                largest = right;

            if (largest == index)
                break;

            Swap(index, largest);
            index = largest;
        }
    }

    private static int Parent(int position)
    {
        return (position - 1) / 2;
    }

    private static int LeftChild(int position)
    {
        return 2 * position + 1;
    }

    private static int RightChild(int position)
    {
        return 2 * position + 2;
    }

    private void Swap(int index1, int index2)
    {
        var temp = array[index1];
        array[index1] = array[index2];
        array[index2] = temp;
    }

    private void DoubleArrayCapacity()
    {
        Array.Resize(ref array, array.Length * 2);
    }
}


