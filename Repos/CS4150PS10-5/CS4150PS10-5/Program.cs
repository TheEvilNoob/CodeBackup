using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150PS10_5
{
    class ArtGallery //Commented working version.
    {
        static int[] firstColumn; //using two arrays instead of a int[,] because according to the internet this is faster.
        static int[] secondColumn;
        static int rows; //Value of N. Just putting it as a global so we can use it in the maxValue function to calculate k = N - r.
        static Dictionary<string, int> values = new Dictionary<string, int>(); //Dictionary of the values we've calculated so far. Used to cut down on the number of times we need to calculate the path. Key is the current combination of the current row, the room we can't close, and the rooms left to close.

        static void Main(string[] args)
        {
            int roomsToBeClosed;
            string input = Console.ReadLine();
            string[] splitString = input.Split(' '); //grab the values of N and k.
            rows = int.Parse(splitString[0]);
            roomsToBeClosed = int.Parse(splitString[1]);

            firstColumn = new int[rows];
            secondColumn = new int[rows];

            for (int i = 0; i < rows; i++) //grab the art gallery values and put them into the two arrays.
            {
                input = Console.ReadLine();
                splitString = input.Split(' ');

                firstColumn[i] = int.Parse(splitString[0]);
                secondColumn[i] = int.Parse(splitString[1]);
            }

            Console.WriteLine(maxValue(0, -1, roomsToBeClosed)); //compute our answer. We start by passing it 0 (because we start at the first row), -1 because either room can be closed, and k.
        }

        static int maxValue(int row, int uncloseableRoom, int roomsToBeClosed)
        {
            int value = default(int);
            if (values.TryGetValue(row + " " + uncloseableRoom + " " + roomsToBeClosed, out value)) //if we've already done the work then don't do it again.
            {
                return value;
            }

            if (row >= rows) //since we blindly just add 1 to row we need to check if the index for the arrays will be out of range.
            {
                return 0;
            }

            int result = 0;
            if (roomsToBeClosed == rows - row) //if k = N - r then we have to close rooms for the rest of our rows.
            {

                if (uncloseableRoom == 0) //if we can't close 0 then close 1.
                {
                    return values[row + " " + uncloseableRoom + " " + roomsToBeClosed] = firstColumn[row] + maxValue(row + 1, 0, roomsToBeClosed - 1);
                }

                else if (uncloseableRoom == 1) //if we can't close 1 then close 0.
                {
                    return values[row + " " + uncloseableRoom + " " + roomsToBeClosed] = secondColumn[row] + maxValue(row + 1, 1, roomsToBeClosed - 1);
                }

                else //if we can close either then compare the value of the two choices and close the smaller one.
                {
                    result = firstColumn[row] + maxValue(row + 1, 0, roomsToBeClosed - 1);
                    int result2 = secondColumn[row] + maxValue(row + 1, 1, roomsToBeClosed - 1);
                    if (result < result2)
                    {
                        result = result2;
                    }
                    return values[row + " " + uncloseableRoom + " " + roomsToBeClosed] = result;
                }
            }
            else //else we have the option of closing rooms or not closing either, depending on whether or not it would give us more value overall.
            {
                if (uncloseableRoom == 0) //if we can't close 0 then either close 1 or neither.
                {
                    result = firstColumn[row] + maxValue(row + 1, 0, roomsToBeClosed - 1);
                    int result2 = firstColumn[row] + secondColumn[row] + maxValue(row + 1, -1, roomsToBeClosed);
                    if (result < result2)
                    {
                        result = result2;
                    }
                    return values[row + " " + uncloseableRoom + " " + roomsToBeClosed] = result;
                }

                else if (uncloseableRoom == 1) //if we can't close 1 then either close 0 or neither.
                {
                    result = secondColumn[row] + maxValue(row + 1, 1, roomsToBeClosed - 1);
                    int result2 = firstColumn[row] + secondColumn[row] + maxValue(row + 1, -1, roomsToBeClosed);
                    if (result < result2)
                    {
                        result = result2;
                    }
                    return values[row + " " + uncloseableRoom + " " + roomsToBeClosed] = result;
                }

                else //if we can close either then close 0, 1, or neither depending on the overall value.
                {
                    result = firstColumn[row] + maxValue(row + 1, 0, roomsToBeClosed - 1);
                    int result2 = secondColumn[row] + maxValue(row + 1, 1, roomsToBeClosed - 1);
                    int result3 = firstColumn[row] + secondColumn[row] + maxValue(row + 1, -1, roomsToBeClosed);
                    if (result < result2) //compare closing 0 and closing 1
                    {
                        result = result2;
                    }

                    if (result < result3) //compare closing 0 or 1 (whichever was greater previously) and closing neither. 
                    {
                        result = result3;
                    }
                    return values[row + " " + uncloseableRoom + " " + roomsToBeClosed] = result;
                }
            }
        }
    }
}
