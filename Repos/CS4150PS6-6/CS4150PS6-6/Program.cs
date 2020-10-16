using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150PS6_6
{
    class Program
    {
        class PriorityQueue
        {
            private SortedList<Tuple<double, int>, int> queue;

            public PriorityQueue()
            {
                queue = new SortedList<Tuple<double, int>, int>();
            }

            public void insertOrChange(int name, double newWeight, double oldWeight)
            {
                queue.Remove(new Tuple<double, int>(oldWeight, name));
                queue.Add(new Tuple<double, int>(newWeight, name), name);
            }

            public int deleteMin()
            {
                int name = queue.Values[0];
                queue.RemoveAt(0);
                return name;
            }

            public bool isEmpty()
            {
                return queue.Count == 0;
            }
        }

        //public static Dictionary<int, List<Tuple<int, double>>> E;
        //public static Dictionary<int, double> V;

        static void Main(string[] args)
        {
            int n, m, x, y;
            double f;
            Dictionary<int, List<Tuple<int, double>>> E;
            string input = Console.ReadLine();
            string[] splitString = input.Split(' ');
            n = int.Parse(splitString[0]);
            m = int.Parse(splitString[1]);
            while (n != 0 && m != 0)
            {
                //V = new Dictionary<int, double>();
                E = new Dictionary<int, List<Tuple<int, double>>>();
                for (int i = 0; i < m; i++)
                {
                    input = Console.ReadLine();
                    splitString = input.Split(' ');
                    x = int.Parse(splitString[0]);
                    y = int.Parse(splitString[1]);
                    f = double.Parse(splitString[2]);

                    if (!E.ContainsKey(x))
                    {
                        E.Add(x, new List<Tuple<int, double>>());
                    }
                    if (!E.ContainsKey(y))
                    {
                        E.Add(y, new List<Tuple<int, double>>());
                    }

                    E[x].Add(new Tuple<int, double>(y, f));
                    E[y].Add(new Tuple<int, double>(x, f));
                }
                Dijkstras(0, n, E);
                input = Console.ReadLine();
                splitString = input.Split(' ');
                n = int.Parse(splitString[0]);
                m = int.Parse(splitString[1]);
            }
        }

        static void Dijkstras(int start, int end, Dictionary<int, List<Tuple<int, double>>> E)
        {
            Dictionary<int, double> dist = new Dictionary<int, double>();

            for (int i = 0; i < end; i++)
            {
                if (!dist.ContainsKey(i))
                {
                    dist.Add(i, double.MaxValue);
                }
            }

            PriorityQueue queue = new PriorityQueue();
            queue.insertOrChange(start, 0, 0);
            dist[start] = 1;

            while (!queue.isEmpty())
            {
                int cur = queue.deleteMin();
                foreach (Tuple<int, double> corridor in E[cur])
                {
                    int v = corridor.Item1;
                    double w = corridor.Item2;
                    if (Delta(dist[v]) > Delta(dist[cur] * w))
                    {
                        double oldW = dist[v];
                        dist[v] = dist[cur] * w;
                        queue.insertOrChange(v, Delta(dist[v]), Delta(oldW));
                    }
                }
            }
            //Console.WriteLine("{0:0.0000}", dist[end - 1]);
            Console.WriteLine(string.Format("{0:N4}", dist[end - 1]));
        }

        static double Delta(double d)
        {
            return Math.Abs(d - 1);
        }
    }
}
