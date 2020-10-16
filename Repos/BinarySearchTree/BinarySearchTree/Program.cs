using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class MainClass
    {
        static List<HashSet<int>> solutions;
        static void Main(string[] args)
        {
            solutions = new List<HashSet<int>>();

            string input;
            List<string> inputList = new List<string>();
            int n, k;
            while ((input = Console.ReadLine()) != null && input != "")
            {
                string[] splitString = input.Split(new char[] { ' ' }, StringSplitOptions.None);
                foreach (string s in splitString)
                {
                    inputList.Add(s);
                }
            }

            n = Convert.ToInt32(inputList[0]);
            k = Convert.ToInt32(inputList[1]);
            for (int i = 2; i < inputList.Count;)
            {
                BST test = new BST();
                for (int j = 0; j < k; j++,i++)
                {
                    test.Add(Convert.ToInt32(inputList[i]));
                }
                CheckSolutions(test.indexes);
            }
            Console.WriteLine(solutions.Count);
        }

        public static void CheckSolutions(HashSet<int> index)
        {
            foreach(HashSet<int> indexes in solutions)
            {
                if (indexes.SetEquals(index))
                {
                    return;
                }
            }
            solutions.Add(index);
        }
    }
    class Node
    {
        public Node left { get; set; }
        public Node right { get; set; }
        public int value { get; set; }
        public int index { get; set; }
    }
    class BST
    {
        public Node root { get; set; }
        public HashSet<int> indexes = new HashSet<int>();
        public int total = 0;
        public bool Add(int addValue)
        {
            Node parent = null, child = root;
            while (child != null)
            {
                parent = child;
                if (addValue < child.value)
                {
                    child = child.left;
                }
                else if (addValue > child.value)
                {
                    child = child.right;
                }
                else
                {
                    return false;
                }
            }
            Node newNode = new Node();
            newNode.value = addValue;

            if (root == null)
            {
                newNode.index = 1;
                indexes.Add(newNode.index);
                root = newNode;
            }
            else
            {
                if (addValue < parent.value)
                {
                    newNode.index = parent.index * 2;
                    indexes.Add(newNode.index);
                    parent.left = newNode;
                }
                else
                {
                    newNode.index = (parent.index * 2) + 1;
                    indexes.Add(newNode.index);
                    parent.right = newNode;
                }
            }
            return true;
        }
    }

}
