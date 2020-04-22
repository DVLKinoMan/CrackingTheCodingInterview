using System;
using CrackingTheCodingInterview.Domain.Classes;

namespace CrackingTheCodingInterview.Domain
{
    public static class SortingAndSearching
    {
        // 10.1 Sorted Merge: You are given two sorted arrays, A and B, where A has a large enough buffer at the
        // end to hold B. Write a method to merge B into A in sorted order. 
        public static void SortedMerge(int[] A, int[] B)
        {
            int indexA = B.Length - 1;
            int indexB = B.Length - 1;
            int indexMerged = A.Length - 1;
            while (indexB >= 0)
            {
                if (indexA >= 0 && A[indexA] > B[indexB])
                    A[indexMerged--] = A[indexA--];
                else A[indexMerged--] = B[indexB--];
            }
        }

        // 10.2 Group Anagrams: Write a method to sort an array ot strings so that all tne anagrnms are next to
        //     each other. 
        public static void GroupAnagrams(string[] arr)
        {
            int i = 0;
            while (i < arr.Length)
            {
                int nextIndex = i + 1;
                for (int j = nextIndex; j < arr.Length; j++)
                    if (AreAnagrams(arr[i], arr[j]))
                    {
                        var k = arr[nextIndex];
                        arr[nextIndex++] = arr[j];
                        arr[j] = k;
                    }

                i = nextIndex;
            }

            bool AreAnagrams(string str, string str2)
            {
                if (str.Length != str2.Length)
                    return false;

                var counts = new int[26];
                foreach (var num in str)
                    counts[num - 'a']++;

                foreach (var num in str2)
                    counts[num - 'a']--;

                foreach (var num in counts)
                    if (num != 0)
                        return false;

                return true;
            }
        }

        // 10.3 Search in Rotated Array: Given a sorted array of n integers that has been rotated an unknown
        //     number of times, write code to find an element in the array. You may assume that the array was
        // originally sorted in increasing order.
        //     EXAMPLE
        // lnput:findSin{lS, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14}
        // Output: 8 (the index of 5 in the array)
        public static int SearchInRotatedArray(int[] arr, int num)
        {
            int st = 0, end = arr.Length - 1;
            while (st <= end)
            {
                int mid = (st + end) / 2;
                if (arr[mid] == num)
                    return mid;
                if (num > arr[mid])
                {
                    if (num > arr[^1] && arr[mid] > num)
                        end = mid - 1;
                    else st = mid + 1;
                }
                else
                {
                    if (num < arr[0] && arr[mid] < num)
                        st = mid + 1;
                    else end = mid - 1;
                }
            }

            return -1;
        }

        //10.4 Sorted Search, No Size: You are given an array like data structure Listy which lacks a size
        // method. It does, however, have an elementAt ( i) method that returns the element at index i in
        // 0( 1) time. If i is beyond the bounds of the data structure, it returns -1. (For this reason, the data
        // structure only supports positive integers.) Given a Li sty which contains sorted, positive integers,
        //     find the index at which an element x occurs. If x occurs multiple times, you may return any index. 
        public static int Search(Listy list, int value)
        {
            int index = 1;
            while (list.ElementAt(index) != -1 && list.ElementAt(index) < value)
                index *= 2;

            return BinarySearch(index / 2, index);

            int BinarySearch(int low, int high)
            {
                int mid;
                while (low <= high)
                {
                    mid = (low + high) / 2;
                    int middle = list.ElementAt(mid);
                    if (middle > value && middle == -1)
                        high = mid - 1;
                    else if (middle < value)
                        low = mid + 1;
                    else return mid;
                }

                return -1;
            }
        }

        //10.5 Sparse Search: Given a sorted array of strings that is interspersed with empty strings, write a
        // method to find the location of a given string.
        // EXAMPLE
        //     Input: ball, {"at",
        //     ""}
        // Output: 4 
        public static int Search(string[] strings, string str, int first, int last)
        {
            if (first > last) return -1;
            /* Move mid to the middle */
            int mid = (last + first) / 2;

            /* If mid is empty, find closest non-empty string. */
            if (strings[mid] == "")
            {
                int left = mid - 1;
                int right = mid + 1;
                while (true)
                {
                    if (left < first && right > last)
                        return -1;
                    else if (right <= last && strings[right] != "")
                    {
                        mid = right;
                        break;
                    }
                    else if (left >= first && strings[left] != "")
                    {
                        mid = left;
                        break;
                    }

                    right++;
                    left--;
                }
            }

            /* Check for string, and recurse if necessary*/
            if (str == strings[mid])
                return mid;
            if (strings[mid].CompareTo(str) < 0)
                return Search(strings, str, mid + 1, last);
            return Search(strings, str, first, mid - 1);
        }

        public static int Search(String[] strings, String str)
        {
            if (strings == null || str == null || str == "")
                return -1;
            return Search(strings, str, 0, strings.Length - 1);
        }
    }
}