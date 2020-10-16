using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150PS1
{
    class Program
    {
        public static HashSet<string> solutions = new HashSet<string>();
        public static HashSet<string> rejected = new HashSet<string>();
        static void Main(string[] args)
        {
            string input;
            List<string> inputList = new List<string>();
            List<string> dictionary = new List<string>();
            int n, k;
            while ((input = Console.ReadLine()) != null && input != "")
            {
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                foreach (string s in splitString)
                {
                    inputList.Add(s);
                }
            }

            for (int i = 2; i < inputList.Count; i++)
            {
                dictionary.Add(inputList[i]);
            }
            n = Convert.ToInt32(inputList[0]);
            k = Convert.ToInt32(inputList[1]);
            SortWords(dictionary);
            Console.WriteLine(solutions.Count);
        }

        static void SortWords(List<string> dictionary)
        {


            string sortedWord = "";
            
            dictionary.Sort();
            foreach (string w in dictionary)
            {
                char[] c = w.ToCharArray();
                Array.Sort<char>(c);
                sortedWord = new string(c);
                if (solutions.Contains(sortedWord))
                {
                    solutions.Remove(sortedWord);
                    rejected.Add(sortedWord);
                }
                else if (!rejected.Contains(sortedWord))
                {
                    solutions.Add(sortedWord);
                }
            }
        }
    }
}
