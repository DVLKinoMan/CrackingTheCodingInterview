using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CrackingTheCodingInterview.Domain
{
    public static class ModerateProblems16Chapter
    {
        // 16.6 Smallest Difference: Given two arrays of integers, compute the pair of values (one value in each
        //     array) with the smallest (non-negative) difference. Return the difference.
        //     EXAMPLE
        //     Input: {1, 3, 15, 11, 2}, {23, 127,235, 19, 8}
        // Output: 3. That is, the pair (11, 8). 
        public static int SmallestDifference(int[] arr1, int[] arr2)
        {
            int min = int.MaxValue;
            Array.Sort(arr1);
            Array.Sort(arr2);
            int index1 = 0;
            int index2 = 0;
            while (index1 != arr1.Length && index2 != arr2.Length)
            {
                min = Math.Min(Math.Abs(arr1[index1] - arr2[index2]), min);
                if (arr1[index1] > arr2[index2])
                    index2++;
                else index1++;
            }

            return min;
        }

        //16.8 English Int: Given any integer, print an English phrase that describes the integer (e.g., "One Thousand, Two Hundred Thirty Four"). 
        public static string IntegerOnEnglish(int number)
        {
            if (number == 0)
                return string.Empty;
            var numberNames = new Dictionary<int, string>()
            {
                {1, "One"},
                {2, "Two"},
                {3, "Three"},
                {4, "Four"},
                {5, "Five"},
                {6, "Six"},
                {7, "Seven"},
                {8, "Eight"},
                {9, "Nine"},
                {10, "Ten"},
                {11, "Eleven"},
                {12, "Twelve"},
                {13, "Thirteen"},
                {14, "Fourteen"},
                {15, "Fifteen"},
                {16, "Sixteen"},
                {17, "Seventeen"},
                {18, "Eighteen"},
                {19, "Nineteen"},
                {20, "Twenty"},
                {30, "Thirty"},
                {40, "Forty"},
                {50, "Fifty"},
                {60, "Sixty"},
                {70, "Seventy"},
                {80, "Eighty"},
                {90, "Ninety"}
            };
            var builder = new StringBuilder();
            if (number < 0)
            {
                builder.Append("Minus ");
                number = -number;
            }

            Func((int) Math.Pow(10, 9), "Billion");
            Func((int) Math.Pow(10, 6), "Million");
            Func(1000, "Thousand");
            Func(100, "Hundred");
            if (number != 0)
                builder.Append(numberNames.ContainsKey(number)
                    ? numberNames[number]
                    : $"{numberNames[number / 10 * 10]} {(number % 10 == 0 ? "" : numberNames[number % 10])}");

            return builder.ToString().TrimEnd();

            void Func(int num, string name = null)
            {
                int decimalPoint = number / num;
                string _decimal = IntegerOnEnglish(decimalPoint);
                if (_decimal != string.Empty)
                {
                    builder.Append($"{_decimal} {name} ");
                    number -= decimalPoint * num;
                }
            }
        }

        // 16.16 Sub Sort: Given an array of integers, write a method to find indices m and n such that if you sorted
        // elements m through n, the entire array would be sorted. Minimize n - m (that is, find the smallest
        //     such sequence).
        // EXAMPLE
        //     lnput:1, 2, 4, 7, 10, 11, 7, 12, 6, 7, 16, 18, 19
        // Output: (3, 9) 
        public static int SubSort(int[] arr)
        {
            int min = int.MaxValue, max = int.MinValue;
            for (int i = 1; i < arr.Length; i++)
                if (arr[i] < arr[i - 1])
                {
                    min = Math.Min(min, arr[i]);
                    max = Math.Max(max, arr[i - 1]);
                }

            int m = -1, n = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > min && m == -1)
                    m = i;
                if (arr[i] > max)
                {
                    n = i - 1;
                    break;
                }
            }

            return n - m;
        }
        
        // 16.17 Contiguous Sequence: You are given an array of integers (both positive and negative). Find the
        // contiguous sequence with the largest sum. Return the sum.
        //     EXAMPLE
        // lnput:2, -8, 3, -2, 4, -10
        // Output: 5 ( i. e • , { 3, -2, 4})
        public static int ContiguousLargestSum(int[] arr)
        {
            int[] dp = new int[arr.Length];
            dp[0] = arr[0];
            for (int i = 1; i < arr.Length; i++)
                dp[i] = Math.Max(dp[i - 1] + arr[i], arr[i]);
            return dp.Max();
        }
        
        // 16.24 Pairs with Sum: Design an algorithm to find all pairs of integers within an array which sum to a
        // specified value. 
        public static List<(int, int)> PairsWithSum(int[] arr, int value)
        {
            var list = new List<(int,int)>();
            var set = new HashSet<int>();
            foreach (var num in arr)
            {
                if(set.Contains(num))
                    list.Add((num, value-num));
                set.Add((value - num));
            }

            return list;
        }
    }
}