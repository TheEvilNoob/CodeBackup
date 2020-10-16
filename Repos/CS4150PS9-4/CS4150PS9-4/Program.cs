using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150PS9_4
{
    class Program //cleaned up the code a little from the last submission and actually commented it.
    {
        static int[] hotels;
        static int[] smallestPenalties;
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int numberOfHotels = int.Parse(input); //get the number of hotels or stops I guess along the path.
            hotels = new int[numberOfHotels+1]; //create the hotel array.
            smallestPenalties = new int[numberOfHotels + 1]; //create an array where we can calculate the base smallest penalty so we can compare new penalties in our Penalty() function.

            for(int i = 0; i < numberOfHotels + 1; i++) //read everything in
            {
                input = Console.ReadLine();
                hotels[i] = (int.Parse(input));
            }

            Penalty(); //call penalty. It handles everything inside the function so we don't need to pass it anything or return anything.

            int result = smallestPenalties.Last(); //as we work our way through our hotels array in Penalty() we work our way towards the best path to take at the end of the list, so it will always be the last item.
            Console.WriteLine(result);
        }

        static void Penalty()
        {
            int penalty;
            for (int end = 1; end < hotels.Length; end++)
            {
                smallestPenalties[end] = Square(400 - hotels[end], 2); //find the worst case penalty of driving from 0 straight to that hotel (and later 1, 2, etc). Is actually (400 - (hotels[end] - 0))^2, but the first spot is both always 0 and not needed to get the answer.
                for (int start = 1; start < end; start++)
                {
                    penalty = Square(400 - (hotels[end] - hotels[start]), 2); //now start looking at the paths and comparing them to the worst case scenario. If driving straight from 0 to that hotel is the best penalty we keep it, otherwise we replace it with the better penalty.

                    if ((smallestPenalties[start] + penalty) < smallestPenalties[end])
                    {
                        smallestPenalties[end] = smallestPenalties[start] + penalty;
                    }
                }
            }
        }

        /*static int Penalty(int start)
        {
            SortedSet<int> penalties = new SortedSet<int>();
            //int smallestPenalty;
            if(start == hotels.Length - 1)
            {
                return 0;
            }
            int end = start + 1;
            int result, penalty;

            for (int i = end; i < hotels.Length; i++)
            {
                end = i;
                penalty = Square(400 - (hotels[end] - hotels[start]), 2) + Penalty(end);

                if (penalties.Count == 0)
                {
                    penalties.Add(penalty);
                }
                else if (penalty < penalties.Min())
                {
                    penalties.Add(penalty);
                }
            }

            result = penalties.Min();
            return result;

            if (start == hotels.Length - 1)
            {
                return 0;
            }
            int end = start + 1;
            int result = 0, penalty;

            for (int i = end; i < hotels.Length; i++)
            {
                end = i;
                penalty = Square(400 - (hotels[end] - hotels[start]), 2) + Penalty(end);

                if ((smallestPenalties[start] + penalty) < smallestPenalties[end])
                {
                    smallestPenalties[end] = smallestPenalties[start] + penalty;
                }
            }

            //result = penalties.Min();
            return result;
        }*/

        static int Square(int number, int exponent) //function that is hopefully a little faster than Math.Pow since I know exactly what I want it to do, what I'm passing it, etc. and it doesn't need to do unneeded checks.
        {
            int result = 1;
            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                {
                    result *= number;
                }
                exponent >>= 1;
                number *= number;
            }

            return result;
        }
    }
}
