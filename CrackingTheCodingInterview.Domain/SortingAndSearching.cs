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
    }
}