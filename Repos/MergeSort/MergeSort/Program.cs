using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MergeClass {
    public static int counter = 0;
    static void Main(string[] args)
    {
        int[] arr = new int[8]
        {
        0, 2, 4, 6, 1, 3, 5, 7
        };

        MergeSort(arr, 0, arr.Length - 1);
        Console.WriteLine(counter);
        Console.WriteLine("Sorted Values:");
        for (int i = 0; i < arr.Length; i++)
            Console.WriteLine(arr[i]);
    }
 

private static void Merge(int[] input, int left, int middle, int right)
    {
        
        int[] leftArray = new int[middle - left + 1];
        int[] rightArray = new int[right - middle];

        Array.Copy(input, left, leftArray, 0, middle - left + 1);
        Array.Copy(input, middle + 1, rightArray, 0, right - middle);

        int i = 0;
        int j = 0;
        for (int k = left; k < right + 1; k++)
        {
            if (i == leftArray.Length)
            {
                input[k] = rightArray[j];
                j++;
            }
            else if (j == rightArray.Length)
            {
                input[k] = leftArray[i];
                i++;
            }
            else if (leftArray[i] <= rightArray[j])
            {
                counter++;
                input[k] = leftArray[i];
                i++;
            }
            else
            {
                counter++;
                input[k] = rightArray[j];
                j++;
            }
        }
    }

    private static void MergeSort(int[] input, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            MergeSort(input, left, middle);
            MergeSort(input, middle + 1, right);

            Merge(input, left, middle, right);
        }
    }
}
