using System;
using System.Text;

namespace CrackingTheCodingInterview.Domain
{
    public static class BitManipulation
    {
        // 5.1 Insertion: You are given two 32-bit numbers, N and M, and two bit positions, i and
        // j. Write a method to insert M into N such that M starts at bit j and ends at bit i. You
        // can assume that the bits j through i have enough space to fit all of M. That is, if
        // M = 10011, you can assume that there are at least 5 bits between j and i. You would not, for
        // example, have j = 3 and i = 2, because M could not fully fit between bit 3 and bit 2.
        // EXAMPLE
        //     Input: N 10000000000, M = 10011, i = 2, j = 6 
        //     Output: N = 10001001100
        // Hints: #137, #769, #215
        // 10011, i 2, j 6 
        public static int Insertion(int n, int m, int i, int j)
        {
            for (int k = i; k <= j; k++)
                n &= GetNumberWithOneZero(k);

            //Alternate Version:
            // int allOnes = ~0;
            // int left = allOnes << (j + 1);
            // int right = ((1 << i) - 1);

            return n | m << i;

            int GetNumberWithOneZero(int index) => ~(1 << index);
        }

        // 5.2: Binary to String: Given a real number between O and 1 (e.g., 0.72) that is passed in as a double, print
        // the binary representation. If the number cannot be represented accurately in binary with at most 32
        // characters, print "ERROR:'
        public static string InBinary(double number)
        {
            if (number >= 1 || number <= 0)
                return "ERROR";

            var builder = new StringBuilder();
            while (number > 0)
            {
                double r = number * 2;
                if (r > 1)
                {
                    builder.Append("1");
                    number = r - 1;
                }
                else
                {
                    builder.Append("0");
                    number = r;
                }
            }

            return builder.ToString();
        }

        //5.3 Flip Bit to Win: You have an integer and you can flip exactly one bit from a 0 to a 1. Write code to
        // find the length of the longest sequence of ls you could create.
        //     EXAMPLE
        // Input: 1775
        // Output: 8
        public static int LongestSequenceOfOnes(int number)
        {
            int onesCount = 0, prevSeq = 0, res = 1;
            while (number != 0)
            {
                if ((number & 1) == 1)
                    onesCount++;
                else
                {
                    prevSeq = (number & 2) == 0 ? 0 : onesCount;
                    onesCount = 0;
                }

                res = Math.Max(res, onesCount + prevSeq + 1);
                number >>= 1;
            }

            return res;
        }

        //5.4 Next Number: Given a positive integer, print the next smallest and the next largest number that
        // have the same number of 1 bits in their binary representation
        public static int GetNext(int n)
        {
            int c = n, c0 = 0, c1 = 0;
            //count zero bits to the end
            while (((c & 1) == 0) && (c != 0))
            {
                c0++;
                c >>= 1;
            }

            //count one bits to the end
            while ((c & 1) == 1)
            {
                c1++;
                c >>= 1;
            }

            //error
            if (c0 + c1 == 31 || c0 + c1 == 0)
                return -1;

            int p = c0 + c1; //position of rightmost non-trailing zero

            n |= (1 << p); //flip rightmost non-trailing zero
            n &= ~((1 << p) - 1); //Clear all bits to the right of p
            n |= (1 << (c1 - 1)) - 1; //Insert (c1 - 1) ones on the right

            return n;
            
            //Second version:
            return n + (1 << c0) + (1 << (c1 - 1)) - 1;
        }
        
        public static int GetPrevious(int n)
        {
            int temp = n, c0 = 0, c1 = 0;
            while ((temp & 1) == 1)
            {
                c1++;
                temp >>= 1;
            }

            if (temp == 0)
                return -1;
            
            while (((temp & 1) == 0) && (temp!=0))
            {
                c0++;
                temp >>= 1;
            }
            
            int p = c0 + c1; //position of rightmost non-trailing one
            n &= ((~0) << (p + 1)); // clears from bit p onwards

            int mask = (1 << (c1 + 1)) - 1; // sequence of (c1+1) ones
            n |= mask << (c0 - 1);

            return n;
            
            //Second version:
            return n - (1 << c1) - (1 << (c0 - 1)) + 1;
        }
    }
}