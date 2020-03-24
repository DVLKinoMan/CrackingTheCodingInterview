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

            while (((temp & 1) == 0) && (temp != 0))
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

        // 5.5 Debugger: Explain what the following code does: ((n & (n-1 )) == 0).
        // If value n is a power of two

        // 5.6 Conversion: Write a function to determine the number of bits you would need to flip to convert
        // integer A to integer B.
        //     EXAMPLE
        //     Input: 29 (or: 11101), 15 (or: 01111)
        //     Output: 2
        public static int NumberOfBitsToFlip(int a, int b)
        {
            int num = a ^ b, count = 0;
            while (num != 0)
            {
                count += num & 1;
                num >>= 1;
            }

            return count;

            //Second version:
            // int count = 0;
            // for (int c = a ^ b; c != 0; c = c & (c - 1))
            //     count++;
            //
            // return count;
        }

        // 5.7 Pairwise Swap: Write a program to swap odd and even bits in an integer with as few instructions as
        // possible (e.g., bit 0 and bit 1 are swapped, bit 2 and bit 3 are swapped, and so on). 
        public static int SwapOddEvenBits(int x)
        {
            return (int) (((x & 0xaaaaaaaa) >> 1) | ((x & 0x55555555) << 1));
        }

        // 5.8 Draw Line: A monochrome screen is stored as a single array of bytes, allowing eight consecutive
        // pixels to be stored in one byte. The screen has width w, where w is divisible by 8 (that is, no byte will
        // be split across rows). The height of the screen, of course, can be derived from the length of the array
        // and the width. Implement a function that draws a horizontal line from ( xl, y) to ( x2, y).
        // The method signature should look something like:
        // drawline(byte[] screen, int width, int xl, int x2, int y) 
        public static void DrawLine(byte[] screen, int width, int x1, int x2, int y)
        {
            int startOffset = x1 % 8;
            int firstFullByte = x1 / 8 + (startOffset != 0 ? 1 : 0);

            int endOffset = x2 % 8;
            int lastFullByte = x2 / 8 + (endOffset != 7 ? -1 : 0);

            for (int b = firstFullByte; b <= lastFullByte; b++)
                screen[(width / 8) * y + b] = 0xff;

            byte startMask = (byte) (0xff >> startOffset);
            byte endMask = (byte) ~(0xff >> (endOffset + 1));

            if ((x1 / 8) == (x2 / 8))
            {
                byte mask = (byte) (startMask & endMask);
                screen[(width / 8) * y + (x1 / 8)] |= mask;
            }
            else
            {
                if (startOffset != 0)
                {
                    int byteNumber = (width / 8) * y + firstFullByte - 1;
                    screen[byteNumber] |= startMask;
                }

                if (endOffset != 7)
                {
                    int byteNumber = (width / 8) * y + lastFullByte + 1;
                    screen[byteNumber] |= endMask;
                }
            }
        }
    }
}