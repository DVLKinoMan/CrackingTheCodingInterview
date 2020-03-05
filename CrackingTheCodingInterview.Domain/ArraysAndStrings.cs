using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingTheCodingInterview.Domain
{
    public static class ArraysAndStrings
    {
        // 1.1 Implement an algorithm to determine if a string has all unique characters. What if you
        //cannot use additional data structures?
        public static bool HasAllUniqueCharacters1(string str)
        {
            if (str.Length > 128)
                return false;

            var charSet = new bool[128];
            foreach (var ch in str)
            {
                if (charSet[ch] == true)
                    return false;

                charSet[ch] = true;
            }

            return true;
        }

        //Bit Vector Solution
        public static bool HasAllUniqueCharacters2(string str)
        {
            int checker = 0;
            foreach (var ch in str)
            {
                int val = 1 << ch - 'a';
                if ((checker & val) != 0)
                    return false;
                checker |= val;
            }

            return true;
        }

        // 1.2 Given two strings, write a method to decide if one is a permutation of the
        // other.
        public static bool CheckPermutation(string a, string b)
        {
            if (a.Length != b.Length)
                return false;

            var arr = new int[128];

            foreach (var ch in a)
                arr[ch]++;

            foreach (var ch in b)
            {
                arr[ch]--;
                if (arr[ch] < 0)
                    return false;
            }

            return true;
        }

        // 1.3 Write a method to replace all spaces in a string with '%20'. You may assume that the string
        // has sufficient space at the end to hold the additional characters, and that you are given the "true"
        // length of the string. (Note: If implementing in Java, please use a character array so that you can
        //     perform this operation in place.)
        // EXAMPLE
        //     Input: "Mr John Smith ", 13
        // Output: "Mr%20John%20Smith"
        public static string URLify(string str, int len)
        {
            var builder = new StringBuilder();
            foreach (var ch in str)
                builder.Append(ch == ' ' ? "%20" : ch.ToString());
            return builder.ToString();
        }

        // 1.4 Given a string, write a function to check if it is a permutation of a palindrome. A palindrome is a word or phrase that is the same forwards and backwards. A permutation
        // is a rearrangement of letters. The palindrome does not need to be limited to just dictionary words.
        // EXAMPLE
        //     Input: Tact Coa
        // Output: True (permutations: "taco cat", "atco eta", etc.) 
        public static bool IsPalindromePermutation1(string str)
        {
            int[] arr = new int[128];
            foreach (var ch in str)
                if (ch != ' ')
                    arr[ch]++;

            bool isOddCount = false;
            foreach (var count in arr)
                if (count % 2 == 1)
                {
                    if (str.Length % 2 == 1 || isOddCount)
                        return false;
                    isOddCount = true;
                }

            return true;
        }

        public static bool IsPalindromePermutation2(string str)
        {
            int bitVector = 0;
            foreach (var ch in str)
            {
                int mask = 1 << ch - 'a';
                if ((bitVector & mask) != 0)
                    //Learn 2: ~ is shifting bits to opposite
                    bitVector &= ~mask; //int.MaxValue ^ mask;
                else bitVector |= mask;
            }

            // if (bitVector == 0)
            //     return true;
            //
            // if (str.Length % 2 == 0)
            //     return false;

            return bitVector == 0 ||
                   //Learn 3: checking if binary representation contains only one 1 bit
                   (bitVector & (bitVector - 1)) == 0; //Has one 1

            // bool wasOneOdd = false;
            // while (bitVector != 0)
            // {
            //     if ((bitVector & 1) == 1)
            //     {
            //         if (wasOneOdd)
            //             return false;
            //         wasOneOdd = true;
            //     }
            //
            //     bitVector >>= 1;
            // }
            //
            // return true;
        }

        // 1.5 There are three types of edits that can be performed on strings: insert a character,
        // remove a character, or replace a character. Given two strings, write a function to check if they are
        // one edit (or zero edits) away. 
        // EXAMPLE:
        // pale, ple -> true
        // pales, pale -> true
        // pale, bale -> true
        // pale, bake -> false 
        public static bool OneEditAway(string a, string b)
        {
            int dist = Math.Abs(a.Length - b.Length);
            if (dist > 1)
                return false;

            if (dist == 0)
            {
                bool diff = false;
                for (int i = 0; i < a.Length; i++)
                    if (a[i] != b[i])
                    {
                        if (diff)
                            return false;
                        diff = true;
                    }

                return true;
            }

            if (a.Length > b.Length)
                return OneEditInsert(b, a);

            return OneEditInsert(a, b);

            //Learn 1: Removing is equal to inserting in this case
            bool OneEditInsert(string s1, string s2)
            {
                int index1 = 0, index2 = 0;
                while (index1 < s1.Length && index2 < s2.Length)
                {
                    if (s1[index1] != s2[index2])
                    {
                        if (index1 != index2)
                            return false;
                        index2++;
                    }
                    else
                    {
                        index1++;
                        index2++;
                    }
                }

                return true;
            }
        }

        // 1.6 Implement a method to perform basic string compression using the counts
        // of repeated characters. For example, the string aabcccccaaa would become a2blc5a3. If the
        // "compressed" string would not become smaller than the original string, your method should return
        // the original string. You can assume the string has only uppercase and lowercase letters (a - z). 
        public static string StringCompression(string str)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                int count = 1;
                while (i + 1 < str.Length && str[i] == str[i + 1])
                {
                    count++;
                    i++;
                }

                builder.Append($"{str[i]}{count}");
            }

            string res = builder.ToString();
            return res.Length >= str.Length ? str : res;
        }

        // 1.7 Given an image represented by an NxN matrix, where each pixel in the image is 4
        // bytes, write a method to rotate the image by 90 degrees. Can you do this in place? 
        public static void RotateMatrix(int[,] matrix)
        {
            int n = matrix.Length;
            for (int layer = 0; layer < n / 2; layer++)
            {
                int first = layer;
                int last = n - 1 - layer;
                for (int i = first; i < last; i++)
                {
                    int offset = i - first;
                    int top = matrix[first, i];

                    matrix[first, i] = matrix[last - offset, first];
                    matrix[last - offset, first] = matrix[last, last - offset];
                    matrix[last, last - offset] = matrix[i, last];
                    matrix[i, last] = top;
                }
            }
        }

        // 1.8 Write an algorithm such that if an element in an MxN matrix is 0, its entire row and
        // column are set to 0. 
        public static int[][] ZeroMatrix(int[][] matrix)
        {
            int m = matrix.Length, n = matrix[0].Length;
            var visitedCols = new HashSet<int>();
            for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
            {
                if (!visitedCols.Contains(j) && matrix[i][j] == 0)
                {
                    for (int j2 = 0; j2 < n; j2++)
                        matrix[i][j2] = 0;
                    for (int i2 = 0; i2 < m; i2++)
                        matrix[i2][j] = 0;
                    visitedCols.Add(j);
                    break;
                }
            }

            return matrix;
        }

        // 1.9 Assume you have a method isSubstring which checks if one word is a substring
        // of another. Given two strings, sl and s2, write code to check if s2 is a rotation of sl using only one
        // call to isSubstring (e.g., "waterbottle" is a rotation of"erbottlewat").
        public static bool IsRotation(string s1, string s2)
        {
            int len = s1.Length;
            /* Check that sl and s2 are equal length and not empty*/
            if (len == s2.Length && len > 0)
            {
                /* Concatenate sl and sl within new buffer */
                var slsl = s1 + s1;
                return IsSubstring(slsl, s2);
            }

            return false;

            //Some fake method
            static bool IsSubstring(string slsl, string s)
            {
                return true;
            }
        }
    }
}