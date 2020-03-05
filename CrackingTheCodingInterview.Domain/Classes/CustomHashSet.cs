using System;

namespace CrackingTheCodingInterview.Domain.Classes
{
    public class CustomHashSet<T>
    {
        // private struct Slot
        // {
        //     public T Value;
        //     public int? Next;
        //     public int HashCode;
        // }
        //
        // private T[] _arr = new T[0];
        // private Slot[] _slots = new Slot[0];
        //
        // private int _size = 0;
        //
        //
        // public bool Add(T value)
        // {
        //     if (Contains(value))
        //         return false;
        //
        //     _size++;
        //     Array.Resize(ref _arr, _size);
        //     Array.Resize(ref _slots, _size);
        //     _arr[_size - 1] = value;
        //     
        // }
        //
        // public bool Contains(T value)
        // {
        //     int index = GetIndex(value);
        //     if (_arr[index].Equals(value))
        //         return true;
        //
        //     int j = index + 1;
        //     while (j != index)
        //     {
        //         if (j == _size)
        //         {
        //             j = 0;
        //             continue;
        //         }
        //
        //         if (_arr[j].Equals(value))
        //             return true;
        //
        //         j++;
        //     }
        //
        //     return false;
        // }
        //
        // private int GetIndex(T value)
        // {
        //     return value.GetHashCode() % _size;
        // }
    }
}