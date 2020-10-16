using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150PS5_5
{
    class Program
    {
        public static Dictionary<string, List<string>> V = new Dictionary<string, List<string>>();
        List<string> E = new List<string>();

        static void Main(string[] args)
        {
            int n, f, r;
            List<string> students = new List<string>();
            n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                students.Add(Console.ReadLine());
            }
            students.Sort();
            foreach (string s in students)
            {
                V.Add(s, new List<string>());
            }

            f = int.Parse(Console.ReadLine());
            for (int i = 0; i < f; i++)
            {
                string[] splitString = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.None);
                V[splitString[0]].Add(splitString[1]);
                V[splitString[1]].Add(splitString[0]);
            }

            r = int.Parse(Console.ReadLine());
            List<string> rumors = new List<string>();
            for (int i = 0; i < r; i++)
            {
                string input = Console.ReadLine();
                rumors.Add(input);
                BreadthFirstSearch(input);
            }
        }

        static void BreadthFirstSearch(string start)
        {
            Queue<string> order = new Queue<string>();
            HashSet<string> visited = new HashSet<string>();

            Queue<string> students = new Queue<string>();
            visited.Add(start);
            students.Enqueue(start);
            order.Enqueue(start);

            while (students.Count > 0)
            {
                List<string> newPeople = new List<string>();
                Queue<string> nextPerson = new Queue<string>();
                foreach (string student in students)
                {
                    foreach (string friend in V[student])
                    {
                        if (!visited.Contains(friend))
                        {
                            visited.Add(friend);
                            nextPerson.Enqueue(friend);
                            newPeople.Add(friend);
                        }
                    }
                }
                newPeople.Sort();
                newPeople.ForEach(o => order.Enqueue(o));
                students = nextPerson;
            }
            List<string> remaining = new List<string>();
            foreach (string s in V.Keys)
            {
                if (!visited.Contains(s))
                {
                    remaining.Add(s);
                }
            }
            remaining.Sort();
            remaining.ForEach(o => order.Enqueue(o));
            while (order.Count > 0)
            {
                Console.Write(order.Dequeue().ToString());
                if (order.Count != 0)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}
