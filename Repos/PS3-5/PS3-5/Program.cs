using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS3_5
{
    class Program
    {
        
        static int d;
        static void Main(string[] args)
        {
            string input;
            List<Tuple<int,int>> stars = new List<Tuple<int,int>>();
            int n, count = 0;
            input = Console.ReadLine();
            string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None); ;
            d = int.Parse(splitString[0]);
            n = int.Parse(splitString[1]);
            //while ((input = Console.ReadLine()) != null && input != "")
            //{
                //splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);

                //stars.Add(new Tuple<int,int>(Convert.ToInt32(splitString[0]), Convert.ToInt32(splitString[1])));
            //}
            for(int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);

                stars.Add(new Tuple<int,int>(int.Parse(splitString[0]), int.Parse(splitString[1])));
            }
            Tuple<int, int> galaxy = FindMajority(stars.ToArray(), 0, stars.Count - 1);
            count = 0;
            if (galaxy == null)
            {
                Console.WriteLine("NO");
            }
            else
            {
                foreach (Tuple<int,int> star in stars)
                {
                    if(CheckRange(star, galaxy, d))
                    {
                        count++;
                    }
                }
                if(count > stars.Count / 2)
                {
                    Console.WriteLine(count);
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
        }

        static Tuple<int,int> FindMajority (Tuple<int,int>[] A, int startIndex, int endIndex)
        {
            int len = (endIndex - startIndex) + 1;
            int half = ((len - 1) / 2) + startIndex;
            if(len == 0)
            {
                return null;
            }
            else if (len == 1)
            {
                return A[startIndex];
            }
            else
            {
                Tuple<int,int> x = FindMajority(A, startIndex, half);
                Tuple<int,int> y = FindMajority(A, half+1, endIndex);
                if(x == null && y == null)
                {
                    return null;
                }
                else if (x == null)
                {
                    return CheckMajority(A, y, startIndex, endIndex);
                }
                else if (y == null)
                {
                    return CheckMajority(A, x, startIndex, endIndex);
                }
                else
                {
                    if(CheckMajority(A, x, startIndex, endIndex) != null)
                    {
                        return x;
                    }
                    if (CheckMajority(A, y, startIndex, endIndex) != null)
                    {
                        return y;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        static Tuple<int,int> CheckMajority(Tuple<int,int>[] A, Tuple<int,int> star, int startingIndex, int endingIndex)
        {
            int count = 0;
            int len = (endingIndex - startingIndex) + 1;
            for (int i = startingIndex; i <= endingIndex; i++)
            {
                if (CheckRange(A[i], star, d))
                {
                    count++;
                }
            }
            if (count > len / 2)
            {
                return star;
            }
            else
            {
                return null;
            }
        }

        static bool CheckRange(Tuple<int,int> star1, Tuple<int, int> star2, int d)
        {
            int dX, dY;
            dX = star2.Item1 - star1.Item1;
            dY = star2.Item2 - star1.Item2;
            return ((Math.Pow(dX, 2) + Math.Pow(dY, 2)) < Math.Pow(d, 2));
        }
    }
}
