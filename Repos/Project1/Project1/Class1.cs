using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Class1
    {
    static void Main()
    {
        for (int i = 0; i <= 15; i++)
        {
            string binary = Convert.ToString(i, 2);
            Console.WriteLine(binary);
        }
        Console.ReadLine();
    }
    }
