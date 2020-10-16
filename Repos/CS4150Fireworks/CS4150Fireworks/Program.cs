using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150Fireworks
{
    class Fireworks
    {
        static Dictionary<string, int> distances = new Dictionary<string, int>();
        static Dictionary<string, bool> results = new Dictionary<string, bool>();
        //static int[] distance;
        static int totalDistance;
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] splitstring = input.Split(' ');
            int n, k;
            n = int.Parse(splitstring[0]);
            k = int.Parse(splitstring[1]);
            int[] distance = new int[n - 1];
            for (int i = 0; i < n - 1; i++)
            {
                input = Console.ReadLine();
                int current = int.Parse(input);
                distance[i] = current;
                totalDistance += current;
            }
            Optimize(distance, k);
        }

        static void Optimize(int[] distance, int k)
        {
            string input;
            int[] launchersList = new int[k];
            for (int i = 0; i < k; i++)
            {
                input = Console.ReadLine();
                launchersList[i] = int.Parse(input);
            }
            int launchersToFire, result;
            for (int i = 0; i < launchersList.Length; i++)
            {
                launchersToFire = launchersList[i];
                if (launchersToFire == distance.Length + 1)
                {
                    Console.WriteLine(distance.Min());
                    return;
                }
                if (launchersToFire == 2)
                {
                    Console.WriteLine(totalDistance);
                    return;
                }
                result = Search(distance, launchersToFire, i);
                Console.WriteLine(result);
            }

            /*if (launchersToFire == distance.Length + 1)
            {
                Console.WriteLine(distance.Min());
                return;
            }
            if (launchersToFire == 2)
            {
                Console.WriteLine(totalDistance);
                return;
            }
            int result, min = 0;
            bool results;
            int currentDist = totalDistance - 1;
            //int[] tempDist = (int[])distance.Clone();
            //while(result == true && currentDist > 0)
            //{
            for (int i = 1; i < totalDistance; i++)
            {
                results = Search(distance, launchersToFire, i);

                if (results == true && i >= min)
                {
                    min = i;
                }
                //currentDist = i;
                //if (results == true) {
                    //min = results;
                //}
                //if (result == 0)
                //{
                    //break;
                //}
                //tempDist = distance;
            }
            Console.WriteLine(min);*/
        }

        static int Search(int[] distance, int launchersToFire, int d)
        {

            for(int i = 1; i < totalDistance; i++)
            {
                if(Decision(distance, launchersToFire, i) == true)
                {
                    results.Add(Convert.ToString(launchersToFire) + " " + Convert.ToString(i), true);
                }
                else
                {
                    return i-1;
                }
            }
            return 0;

            //return Decision(distance, launchersToFire, d);
            //if(Decision(distance, launchersToFire, d) == false)
            //{
                //return 0;
            //}
            //distance.tak
            //int split, min, result;
            //for(int i = 0;)

            /*bool result;
            int temp;
            int nonZeros = distance.Length;

            if (Decision(distance, launchersToFire, d) == false)
            {
                return false;
            }
            for (int i = 0; i < distance.Length; i++)
            {
                temp = distance[i];
                distance[i] = 0;
                nonZeros--;
                if(nonZeros < launchersToFire - 1)
                {
                    break;
                }
                result = Decision(distance, launchersToFire, d);
                if (result == false)
                {
                    nonZeros++;
                    distance[i] = temp;
                }
            }
            return true;*/
        }

        static bool Decision(int[] distance, int launchersToFire, int d)
        {
            bool result;
            if(results.TryGetValue(Convert.ToString(launchersToFire) + " " + Convert.ToString(d), out result))
            {
                return result;
            }
            int sum = 0;
            int counter = 0;

            foreach (int i in distance)
            {
                sum += i;
                if (sum >= d)
                {
                    sum = 0;
                    counter++;
                }
            }
            if (counter >= (launchersToFire - 2 + 1))
            {
                return true;
            }
            else
            {
                return false;
            }

            /*int sum1 = 0, sum2 = 0;
            int result;

            for (int i = 1; i < distance.Length; i++)
            {
                for(int j = 0; j < i; j++)
                {
                    sum1 += distance[j];
                }
                for(int k = i; k < distance.Length; k++)
                {
                    sum2 += distance[k];
                }
                if (sum1 < sum2)
                {
                    result = sum1;
                    if(result == d)
                    {
                        return true;
                    }
                }
                else
                {
                    result = sum2;
                    if(result == d)
                    {
                        return true;
                    }
                }
            }
            return false;*/
        }
    }
}
