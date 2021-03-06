﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace CrackingTheCodingInterview.Domain
{
    public static class RecursionAndDynamicProgramming
    {
        // 8.1 Triple Step: A child is running up a staircase with n steps and can hop either 1 step, 2 steps, or 3
        // steps at a time. Implement a method to count how many possible ways the child can run up the
        // stairs. 
        public static int CountWays(int n)
        {
            var dict = new Dictionary<int, int>();
            return Count(n);

            int Count(int i)
            {
                if (i < 0)
                    return 0;
                if (n == 0)
                    return 1;
                if (dict.ContainsKey(i))
                    return dict[i];
                return dict[i] = Count(i - 1) + Count(i - 2) + Count(i - 3);
            }
        }

        // 8.2 Robot in a Grid: Imagine a robot sitting on the upper left corner of grid with r rows and c columns.
        //     The robot can only move in two directions, right and down, but certain cells are "off limits" such that
        // the robot cannot step on them. Design an algorithm to find a path for the robot from the top left to
        //     the bottom right. 
        public static int Robot(int[,] grid)
        {
            int min = int.MaxValue;
            Dfs(0, 0, 0);
            return min;

            void Dfs(int r, int c, int steps)
            {
                if (r >= grid.Length || c >= grid.GetLength(1) || grid[r, c] == -1)
                    return;
                if (r == grid.Length && c == grid.GetLength(1))
                {
                    min = Math.Min(min, steps);
                    return;
                }

                Dfs(r + 1, c, steps + 1);
                Dfs(r, c + 1, steps + 1);
            }
        }

        // 8.3 Magic Index: A magic index in an array A[ 1 .•. n-1] is defined to be an index such that A[ i] =
        // i. Given a sorted array of distinct integers, write a method to find a magic index, if one exists, in
        // array A.
        //     FOLLOW UP
        //     What if the values are not distinct? 
        public static int MagicFast(int[] array)
        {
            return Magic(0, array.Length - 1);

            int Magic(int st, int end)
            {
                if (end < st)
                    return -1;
                int mid = (st + end) / 2;
                if (array[mid] == mid)
                    return mid;
                if (array[mid] > mid)
                    return Magic(st, mid - 1);
                return Magic(mid + 1, end);
            }
        }

        // 8.4 Power Set: Write a method to return all subsets of a set. 

        // 8.5 Recursive Multiply: Write a recursive function to multiply two positive integers without using
        // the * operator (or / operator). You can use addition, subtraction, and bit shifting, but you should
        // minimize the number of those operations. 
        public static int Multiply(int num1, int num2)
        {
            if (num2 == 1)
                return num1;
            return num2 + Multiply(num1, num2 - 1);
        }

        // 8.6 Towers of Hanoi: In the classic problem of the Towers of Hanoi, you have 3 towers and N disks of
        //     different sizes which can slide onto any tower. The puzzle starts with disks sorted in ascending order
        // of size from top to bottom (i.e., each disk sits on top of an even larger one). You have the following
        // constraints:
        // (1) Only one disk can be moved at a time.
        // (2) A disk is slid off the top of one tower onto another tower.
        // (3) A disk cannot be placed on top of a smaller disk.
        //     Write a program to move the disks from the first tower to the last using Stacks.
        public static void TowersOfHanoi(int howManyElements)
        {
            var stacks = new[]
                {new Stack<int>(Enumerable.Range(1, howManyElements).Reverse()), new Stack<int>(), new Stack<int>()};
            var set = new HashSet<int>() {0, 1, 2};
            MoveStack(howManyElements);

            void MoveStack(int howManyElements, int stackIndex = 0, int destIndex = 2)
            {
                if (howManyElements == 0)
                    return;
                var destIndex2 = set.First(s => s != stackIndex && s != destIndex);
                MoveStack(howManyElements - 1, stackIndex, destIndex2);
                stacks[destIndex].Push(stacks[stackIndex].Pop());
                MoveStack(howManyElements - 1, destIndex2, destIndex);
            }
        }

        // 8.7 Permutations without Dups: Write a method to compute all permutations of a string of unique
        //     characters. 

        // 8.8 Permutations with Duplicates: Write a method to compute all permutations of a string whose
        // characters are not necessarily unique. The list of permutations should not have duplicates. 

        // 8.9 Parens: Implement an algorithm to print all valid (i.e., properly opened and closed) combinations
        //     of n pairs of parentheses.
        //     EXAMPLE
        // Input: 3
        // Output: ((())), ( () () ) , ( () ) () , () ( () ) , () () () 
        public static List<string> Parens(int num)
        {
            var list = new List<string>() {"()"};
            Dfs();

            return list;

            void Dfs(int currNum = 1)
            {
                if (currNum == num)
                    return;
                var newList = new List<string>();
                while (list.Count != 0)
                {
                    string str = list[0];
                    list.RemoveAt(0);
                    newList.Add("(" + str + ")");
                    var str2 = str + "()";
                    var str3 = "()" + str;
                    newList.Add(str2);
                    if (str2 != str3)
                        newList.Add(str3);
                }

                list = newList;
                Dfs(currNum + 1);
            }
        }

        // 8.10 Paint Fill: Implement the"paint fill"function that one might see on many image editing programs.
        //     That is, given a screen (represented by a two-dimensional array of colors), a point, and a new color,
        // fill in the surrounding area until the color changes from the original color. 

        // 8.11 Coins: Given an infinite number of quarters (25 cents), dimes (1 O cents), nickels (5 cents), and
        //     pennies (1 cent), write code to calculate the number of ways of representing n cents. 
        public static int CoinNumbers(int n)
        {
            if (n <= 0)
                return 0;
            int n1 = CoinNumbers(n - 1);
            int n2 = CoinNumbers(n - 25);
            int n3 = CoinNumbers(n - 10);
            int n4 = CoinNumbers(n - 5);
            return (n1 == 0 ? 0 : n1 + 1) + (n3 == 0 ? 0 : n3 + 1) + (n2 == 0 ? 0 : n2 + 1) + (n4 == 0 ? 0 : n4 + 1);
        }

        //8.12 Eight Queens:Write an algorithm to print all ways of arranging eight queens on an 8x8 chess board
        // so that none of them share the same row, column, or diagonal. In this case, "diagonal" means all
        // diagonals, not just the two that bisect the board. 

        // 8.13 Stack of Boxes: You have a stack of n boxes, with widths w1
        // , heights hi
        //     , and depths di
        //     . The boxes
        //     cannot be rotated and can only be stacked on top of one another if each box in the stack is strictly
        //     larger than the box above it in width, height, and depth. Implement a method to compute the
        //     height of the tallest possible stack. The height of a stack is the sum of the heights of each box. 

        // 8.14 Boolean Evaluation: Given a boolean expression consisting of the symbols 0 (false), 1 (true), &
        // (AND), I (OR), and /\ (XOR), and a desired boolean result value result, implement a function to
        // count the number of ways of parenthesizing the expression such that it evaluates to result. The
        //     expression should be fully parenthesized (e.g., ( 0) A( 1)) but not extraneously (e.g., ( ( ( 0)) /\ ( 1)) ).
        // EXAMPLE
        //     countEval("l/\01011", false) -> 2
        // countEval("0&0&0&1All0", true)-> 10 
        public static int BooleanEvaluation(string expression, bool result)
        {
            if (string.IsNullOrEmpty(expression))
                return 0;
            if (expression.Length == 1)
                return (result ? expression == "1" : expression == "0") ? 1 : 0;
            int ways = 0;
            for (int i = 1; i < expression.Length; i+=2)
            {
                var left = expression.Substring(0, i);
                var right = expression.Substring(i + 1);

                int leftTrue = BooleanEvaluation(left, true);
                int leftFalse = BooleanEvaluation(left, false);
                int rightTrue = BooleanEvaluation(right, true);
                int rightFalse = BooleanEvaluation(right, false);
                
                int total = (leftTrue + leftFalse) * (rightTrue + rightFalse);

                int totalTrue = expression[i] switch
                {
                    '^' => leftTrue * rightFalse + leftFalse * rightTrue,
                    '&' => leftTrue * rightTrue,
                    _ => leftTrue * rightTrue + leftFalse * rightTrue + leftTrue * rightFalse
                };
                ways += result ? totalTrue : total - totalTrue;
            }

            return ways;
        }
    }
}