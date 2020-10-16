using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150PS8_6
{
    class ReverseComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return -x.CompareTo(y);
        }
    }

    class DuplicateComparer<TKey> : IComparer<TKey> where TKey : IComparable
    {
        public int Compare(TKey x, TKey y)
        {
            //int result = x.CompareTo(y);
            int result = y.CompareTo(x);
            
            if (result == 0)
            {
                return 1;
            }
            else
            {
                return result;
            }
        }
        public int Compare(int x, int y)
        {
            return -x.CompareTo(y);
        }
    }


    class PriorityQueue
    {
        private SortedList<int, int> queue;

        public PriorityQueue()
        {
            queue = new SortedList<int, int>(new DuplicateComparer<int>());
        }

        public void insertOrChange(int name, int newWeight)
        {
            queue.Add(newWeight, name);
        }

        public KeyValuePair<int,int> deleteMax()
        {
            int value = queue.Values[0];
            int key = queue.Keys[0];
            queue.RemoveAt(0);
            return new KeyValuePair<int, int>(key, value);
        }

        public bool isEmpty()
        {
            return queue.Count == 0;
        }
    }

    class Bank
    {
        static void Main(string[] args)
        {
            PriorityQueue queue = new PriorityQueue();
            int totalMoneyMade = 0;
            string input = Console.ReadLine();
            string[] splitstring = input.Split(' ');
            int totalPeople = int.Parse(splitstring[0]);
            int minutesUntilClose = int.Parse(splitstring[1]);
            SortedDictionary<int, List<int>> peopleInQueue = new SortedDictionary<int, List<int>>(new ReverseComparer());


            for (int i = 0; i < totalPeople; i++) //Add everybody in the Queue based on the amount of minutes they have left before leaving.
            {
                input = Console.ReadLine();
                splitstring = input.Split(' ');

                int amountToDeposit = int.Parse(splitstring[0]);
                int minutesLeft = int.Parse(splitstring[1]);
                if (!peopleInQueue.ContainsKey(minutesLeft))
                {
                    peopleInQueue.Add(minutesLeft, new List<int>());
                    peopleInQueue[minutesLeft].Add(amountToDeposit);
                }
                else
                {
                    peopleInQueue[minutesLeft].Add(amountToDeposit);
                }
            }

            foreach (KeyValuePair<int, List<int>> pair in peopleInQueue) //sort the amount of money each person has to deposit for each key
            {
                pair.Value.Sort((a, b) => b.CompareTo(a));
                foreach (int money in pair.Value)
                {
                    queue.insertOrChange(pair.Key, money);
                }

                if(!queue.isEmpty())
                {
                    totalMoneyMade += queue.deleteMax().Key;
                }
            }

            Console.WriteLine(totalMoneyMade);
        }
    }
}
