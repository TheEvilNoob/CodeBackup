using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150PS5_5
{
    class Program
    {
        public class Person
        {
            string name;
            List<Person> E = new List<Person>();

            public Person(string n)
            {
                name = n;
            }
            public void AddPath(Person p)
            {
                E.Add(p);
                SortFriends();
            }
            public override string ToString()
            {
                return name;
            }
            public List<Person> FriendsOf()
            {
                return E;
            }
            public void SortFriends()
            {
                List<string> friends = new List<string>();
                foreach (Person p in E)
                {
                    friends.Add(p.ToString());
                }
                friends.Sort();
                Person[] temp = new Person[E.Count];
                E.CopyTo(temp);
                for (int i = 0; i < friends.Count; i++)
                {
                    foreach (Person p in temp)
                    {
                        if (friends[i] == p.ToString())
                        {
                            Swap(i, E.IndexOf(p));
                        }
                    }
                }
            }

            public void Swap(int i, int j)
            {
                if (i == j)
                {
                    return;
                }
                Person temp = E[i];
                E[i] = E[j];
                E[j] = temp;
            }
        }

        public static Dictionary<string, Person> V = new Dictionary<string, Person>();

        static void Main(string[] args)
        {
            int n, f, r;
            List<string> students = new List<string>();
            string input = Console.ReadLine();
            n = int.Parse(input);

            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                students.Add(input);
            }
            students.Sort();
            foreach (string s in students)
            {
                V.Add(s, new Person(s));
            }

            input = Console.ReadLine();
            f = int.Parse(input);
            for (int i = 0; i < f; i++)
            {
                input = Console.ReadLine();
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                V[splitString[0]].AddPath(V[splitString[1]]);
                V[splitString[1]].AddPath(V[splitString[0]]);
            }
            input = Console.ReadLine();
            r = int.Parse(input);
            List<string> rumors = new List<string>();
            for (int i = 0; i < r; i++)
            {
                input = Console.ReadLine();
                rumors.Add(input);
            }
            foreach (string rumor in rumors)
            {
                Queue<Person> result = BreadthFirstSearch(V[rumor]);
                foreach (string s in students)
                {
                    if (!result.Contains(V[s]))
                    {
                        result.Enqueue(V[s]);
                    }
                }
                while (result.Count > 0)
                {
                    Console.Write(result.Dequeue().ToString());
                    if (result.Count != 0)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        static Queue<Person> BreadthFirstSearch(Person start)
        {
            Queue<Person> order = new Queue<Person>();
            HashSet<Person> visited = new HashSet<Person>();

            Queue<Person> q = new Queue<Person>();
            visited.Add(start);
            q.Enqueue(start);
            order.Enqueue(start);

            while (q.Count > 0)
            {
                List<string> newPeople = new List<string>();
                Queue<Person> nextPerson = new Queue<Person>();
                foreach (Person curP in q)
                {
                    foreach (Person p in curP.FriendsOf())
                    {
                        if (!visited.Contains(p))
                        {
                            visited.Add(p);
                            nextPerson.Enqueue(p);
                            newPeople.Add(p.ToString());
                        }
                    }
                }
                newPeople.Sort();
                newPeople.ForEach(o => order.Enqueue(V[o]));
                q = nextPerson;
            }
            return order;
        }
    }
}
