using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellman_Ford
{
    public class MainClass
    {

        static void Main(string[] args)
        {
            int n, h, t;
            string input = Console.ReadLine();
            BF bf = new BF();
            Dictionary<string, double> cities = new Dictionary<string, double>();
            n = int.Parse(input);
            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                cities.Add(splitString[0], Convert.ToDouble(splitString[1]));
                bf.AddHighway(splitString[0], splitString[0], Convert.ToDouble(splitString[1]));
                //V.Add(splitString[0], new BF.City());
            }

            input = Console.ReadLine();
            h = int.Parse(input);
            for (int i = 0; i < h; i++)
            {
                input = Console.ReadLine();
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                bf.AddHighway(splitString[0], splitString[1], cities[splitString[1]]);
                //bf.E.Add(new Highway(splitString[0], splitString[1], cities[splitString[1]]));
                //bf.D.Add(splitString[0], E);
            }

            input = Console.ReadLine();
            t = int.Parse(input);
            List<string> trips = new List<string>();
            for (int i = 0; i < t; i++)
            {
                input = Console.ReadLine();
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                trips.Add(splitString[0]);
                trips.Add(splitString[1]);
            }

            double result;
            for (int i = 0; i < trips.Count; i += 2)
            {
                result = bf.BellmanFord(trips[i], trips[i + 1]);
                if (result == int.MaxValue)
                {
                    Console.WriteLine("NO");
                }
                else
                {
                    Console.WriteLine(result);
                }
            }
        }

        public class BF
        {
            public class Highway
            {
                public string start, destination;
                public double weight;
                public Highway(string s, string d, double w)
                {
                    start = s;
                    destination = d;
                    weight = w;
                }

            }
            public class City
            {
                public string previous;
                public double distance;
            }

            public static Dictionary<string, City> V = new Dictionary<string, City>();
            public static List<Highway> E = new List<Highway>();
            public static Dictionary<string, List<Highway>> D = new Dictionary<string, List<Highway>>();
            public static IEnumerable<string> order = null;


            public void AddHighway(string s, string d, double w)
            {
                Highway h = new Highway(s, d, w);
                E.Add(h);

                if (!V.ContainsKey(s))
                {
                    V.Add(s, new City());
                }
                if (!V.ContainsKey(d))
                {
                    V.Add(d, new City());
                }
                if (!D.ContainsKey(s))
                {
                    D.Add(s, new List<Highway>());
                }
                D[s].Add(h);
            }

            public double BellmanFord(string start, string destination)
            {
                if (start == destination)
                {
                    return 0;
                }

                foreach (string city in V.Keys)
                {
                    V[city].distance = int.MaxValue;
                    V[city].previous = null;
                }
                V[start].distance = 0;

                if (order == null)
                {
                    order = FindOrder(E);
                }

                foreach (string city in order)
                {
                    foreach (Highway h in D[city])
                    {
                        UpdatePath(h.start, h.destination, h.weight);
                    }
                }
                return V[destination].distance;
            }

            static void UpdatePath(string s, string d, double w)
            {
                if (V[d].distance > V[s].distance + w)
                {
                    V[d].distance = V[s].distance + w;
                    V[d].previous = s;
                }
            }

            static IEnumerable<string> FindOrder(List<Highway> E)
            {
                HashSet<string> visited = new HashSet<string>();
                Stack<string> s = new Stack<string>();

                for (int i = 0; i < E.Count; i++)
                {
                    if (!visited.Contains(E[i].start))
                    {
                        TopologicalSort(E[i].start, visited, s, E);
                    }
                }
                return s;
            }

            static void TopologicalSort(string start, HashSet<string> visited, Stack<string> s, List<Highway> E)
            {
                visited.Add(start);

                for (int i = 0; i < E.Count; i++)
                {
                    if (E[i].start == start)
                    {
                        if (!visited.Contains(E[i].destination))
                        {
                            TopologicalSort(E[i].destination, visited, s, E);
                        }
                    }
                }
                s.Push(start);
            }

        }
    }
}


