using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellman_Ford
{
    class Graph
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
        public class Path
        {
            Dictionary<string, City> V;
            List<Highway> E;
            Dictionary<string, List<Highway>> D;
            IEnumerable<string> order;
        }

        public Path p = new Path();
        static void Main(string[] args)
        {
            int n, h, t;
            string input = Console.ReadLine();
            List<int> costs = new List<int>();
            Dictionary<string, double> cities = new Dictionary<string, double>();
            n = int.Parse(input);
            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                cities.Add(splitString[0], splitString[1]);
                V.Add(splitString[0], new City());
            }

            input = Console.ReadLine();
            h = int.Parse(input);
            for (int i = 0; i < h; i++)
            {
                input = Console.ReadLine();
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                E.Add(new Highway(splitString[0], splitString[1], cities[splitString[1]].Value));
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
            //Graph g = SetupGraph(costs, highways);
            for (int i = 0; i < trips.Count; i+=2)
            {
                Console.WriteLine(Bellman_Ford(trips[i], trips[i+1]));
            }
        }

        double BellmanFord(string start, string destination)
        {
            //int edge = g.edge;
            //int vertex = g.vertex;
            //int[] distance = new int[vertex];
            int s, d, w;

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
                foreach (Highway h in ED[city])
                {
                    UpdatePath(h.start, h.destination, h.weight);
                }
            }
            return V[destination].distance;
        }

        void UpdatePath(string s, string d, double w)
        {
            if(V[d].distance > V[s].distance + w)
            {
                V[d].distance = V[s].distance + w;
                V[d].previous = s;
            }
        }

        IEnumerable<string> FindOrder(List<Highway> E)
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

        void TopologicalSort(string start, HashSet<string> visited, Stack<string> s, List<Highway> E)
        {
            visited.Add(start);

            foreach (Highway h in E)
            {
                if (h.start == start)
                {
                    if (!visited.Contains(h.destination))
                    {
                        TopologicalSort(h.destination, visited, s, E);
                    }
                }
            }
            s.Push(start);
        }
        
    }
    
}
