using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] B = new int[] { 2, 5, 7, 10, 22 };
            int[] A = new int[] { 7, 8, 9, 13, 14 };
            int k = 9;
            Console.WriteLine(select(A, B, k));
        }

        static int select(int[] A, int[] B, int k)
        {
            return select(A, 0, A.Count() - 1, B, 0, B.Count() - 1, k);
        }

        static int select(int[] A, int loA, int hiA, int[] B, int loB, int hiB, int k)
        {
            // A and B are each sorted into ascending order, and 0 <= k < |A|+|B|
            // Returns the element that would be stored at index k if A and B were
            // combined into a single array that was sorted into ascending order.
            // A[loA..hiA] is empty
            if (hiA < loA)
                return B[k - loA];
            // B[loB..hiB] is empty
            if (hiB < loB)
                return A[k - loB];
            // Get the midpoints of A[loA..hiA] and B[loB..hiB]
            int i = (loA + hiA) / 2;
            int j = (loB + hiB) / 2;
            // Figure out which one of four cases apply
            if (A[i] > B[j])
                if (k <= i + j)
                    return select(A, loA, i-1, B, loB, j, k);
                else
                    return select(A, i, hiA, B, j+1, hiB, k);
            else
                if (k <= i + j)
                    return select(A, loA, i, B, loB, j-1, k);
                else
                    return select(A, i+1, hiA, B, j, hiB, k);
        }
    }
}
