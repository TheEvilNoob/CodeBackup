using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150GridGobble
{
    class Cell
    {
        //Cell directNeighbor;
        int value;
        int max;
        //Cell maxNeighbor;
        List<Cell> neighbors = new List<Cell>();

        public Cell(int v)
        {
            value = v;
            max = 0;
        }

        /*public void AddNeighbors(Cell left, Cell middle, Cell right)
        {
            neighbors.Add(left);
            neighbors.Add(right);
            directNeighbor = middle;
        }*/

        public void AddNeighbors(Cell neighbor)
        {
            neighbors.Add(neighbor);
        }

        public int GetValue()
        {
            return value;
        }

        public void SetMax(int i)
        {
            max = i;
        }

        /*public int FindMaxNeighbor()
        {
            if(neighbors.Count == 0)
            {
                return 0;
            }
            if(maxNeighbor != null && maxNeighbor.neighbors.Count == 0)
            {
                return maxNeighbor.value;
            }
            int result1, result2, result3;

            result1 = (value - neighbors[0].value) + neighbors[0].FindMaxNeighbor();
            result2 = (value - neighbors[1].value) + neighbors[1].FindMaxNeighbor();
            result3 = (value + neighbors[2].value) + neighbors[2].FindMaxNeighbor();

            if(result1 > result3 && result1 > result2)
            {
                maxNeighbor = neighbors[0];
                middleValue = false;
                return maxNeighbor.value;
            }
            else if (result2 > result3 && result2 > result1)
            {
                maxNeighbor = neighbors[1];
                middleValue = false;
                return maxNeighbor.value;
            }
            else
            {
                maxNeighbor = neighbors[2];
                middleValue = true;
                return maxNeighbor.value;
            }
        }*/

        public int CalculateMax()
        {
            if (neighbors.Count == 0)
            {
                return max;
            }
            if (max != 0)
            {
                return max;
            }
            int result1, result2, result3;

            result1 = (max - neighbors[0].value) + neighbors[0].CalculateMax();
            result2 = (max - neighbors[1].value) + neighbors[1].CalculateMax();
            result3 = (max + neighbors[2].value) + neighbors[2].CalculateMax();

            if (result1 > result3 && result1 > result2)
            {
                //maxNeighbor = neighbors[0];
                max = result1;
            }
            else if (result2 > result3 && result2 > result1)
            {
                //maxNeighbor = neighbors[1];
                max = result2;
            }
            else
            {
                //maxNeighbor = neighbors[2];
                max = result3;
            }
            return max;
        }

        //public Cell GetMaxNeighbor()
        //{
            //return maxNeighbor;
        //}

        public int CalculatePathValue()
        {
            return value + max;
        }

    }
    class Program
    {
        static int r, c;
        static Cell[] cells;
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] splitstring = input.Split(' ');
            r = int.Parse(splitstring[0]);
            c = int.Parse(splitstring[1]);

            cells = new Cell[r * c];
            int[] max = new int[r * c];

            input = Console.ReadLine();
            splitstring = input.Split(' ');

            for (int i = 0; i < c; i++)
            {
                cells[i] = new Cell(int.Parse(splitstring[i]));
                cells[i].SetMax(0);
            }

            int counter = c;
            for (int i = 1; i < r; i++)
            {
                input = Console.ReadLine();
                splitstring = input.Split(' ');
                for (int j = 0; j < c; j++)
                {
                    cells[counter] = new Cell(int.Parse(splitstring[j]));

                    if (j == 0)
                    {
                        cells[counter].AddNeighbors(cells[counter - 1]);
                        cells[counter].AddNeighbors(cells[counter - (c - 1)]);
                    }
                    else if(j == (c - 1))
                    {
                        cells[counter].AddNeighbors(cells[counter - (c + 1)]);
                        cells[counter].AddNeighbors(cells[counter - ((2 * c) - 1)]);
                    }
                    else
                    {
                        cells[counter].AddNeighbors(cells[counter - (c + 1)]);
                        cells[counter].AddNeighbors(cells[counter - (c - 1)]);
                    }
                    cells[counter].AddNeighbors(cells[counter - c]);
                    cells[counter].CalculateMax();
                    counter++;
                }
            }
            Console.WriteLine(FindLargestScore());
        }

        static int FindLargestScore()
        {
            int start = cells.Length - c;
            int result = 0, temp;
            for (int i = start; i < cells.Length; i++)
            {
                temp = cells[i].CalculatePathValue();
                if (temp > result)
                {
                    result = temp;
                }
            }
            return result;
        }
    }
}
