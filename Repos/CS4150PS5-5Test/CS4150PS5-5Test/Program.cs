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
            List<string> students = new List<string>();
            n = int.Parse(input);
            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                students.Add(splitString[0]);
                bf.AddPath(splitString[0], splitString[0]);
                //V.Add(splitString[0], new BF.City());
            }

            input = Console.ReadLine();
            h = int.Parse(input);
            for (int i = 0; i < h; i++)
            {
                input = Console.ReadLine();
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                bf.AddPath(splitString[0], splitString[1]);
                //bf.E.Add(new Path(splitString[0], splitString[1], students[splitString[1]]));
                //bf.D.Add(splitString[0], E);
            }

            input = Console.ReadLine();
            t = int.Parse(input);
            List<string> trips = new List<string>();
            for (int i = 0; i < t; i++)
            {
                input = Console.ReadLine();
                //string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                trips.Add(input);
                //trips.Add(splitString[0]);
                //trips.Add(splitString[1]);
            }

            double result;
            for (int i = 0; i < trips.Count; i++)
            {
                result = bf.BellmanFord(trips[i]);
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
            public class Path
            {
                public string start, destination;
                public Path(string s, string d)
                {
                    start = s;
                    destination = d;
                }

            }
            public class City
            {
                public string previous;
                public double distance;
            }

            public static Dictionary<string, City> V = new Dictionary<string, City>();
            public static List<Path> E = new List<Path>();
            public static Dictionary<string, List<Path>> D = new Dictionary<string, List<Path>>();
            public static IEnumerable<string> order = null;


            public void AddPath(string s, string d)
            {
                Path h = new Path(s, d);
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
                    D.Add(s, new List<Path>());
                }
                D[s].Add(h);
            }

            public double BellmanFord(string start)
            {
                //if (start == destination)
                //{
                    //return 0;
                //}

                foreach (string city in V.Keys)
                {
                    V[city].distance = int.MaxValue;
                    V[city].previous = null;
                }
                V[start].distance = 0;

                if (order == null)
                {
                    order = BreadthFirstSearch(E);
                }

                foreach (string city in order)
                {
                    foreach (Path h in D[city])
                    {
                        UpdatePath(h.start, h.destination);
                    }
                }
                return V[start].distance;
            }

            static void UpdatePath(string s, string d)
            {
                if (V[d].distance > V[s].distance)
                {
                    V[d].distance = V[s].distance;
                    V[d].previous = s;
                }
            }

            static IEnumerable<string> BreadthFirstSearch(List<Path> E)
            {
                Queue<string> order = new Queue<string>();
                HashSet<string> visited = new HashSet<string>();
                Queue<string> q = new Queue<string>();
                visited.Add(E[0].start);
                q.Enqueue(E[0].start);

                while (q.Count > 0)
                {
                    string s = q.Dequeue();
                    order.Enqueue(s);
                    for (int i = 1; i < E.Count; i++)
                    {
                        if (!visited.Contains(E[i].start))
                        {
                            visited.Add(E[i].start);
                            q.Enqueue(E[i].start);
                        }
                    }
                }

                while (order.Count > 0)
                {
                    string str = order.Dequeue();
                }
                return q;
            }

            /*static IEnumerable<string> FindOrder(List<Path> E)
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

            static void TopologicalSort(string start, HashSet<string> visited, Stack<string> s, List<Path> E)
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
            }*/

        }
    }
}


