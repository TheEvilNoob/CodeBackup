using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS3_3
{
    class Program
    {
        static int[] binary = new int[10] {0, 1, 10, 11, 100, 101, 110, 111, 1000, 1001};

        static int fastmultiply(int x, int y)
        {
            int n = Convert.ToInt32(Math.Floor(Math.Log10(x) + 1));
            if (n == 1)
            {
                return x * y;
            }
            else
            {
                int xl = x / 100;
                int xr = x % 100;
                int yl = y / 100;
                int yr = y % 100;

                int p1 = xl * yl;
                int p2 = xr * yr;
                int p3 = (xl + xr) * (yl + yr);
                return p1 * (2 ^ n) + (p3 - p1 - p2) * (2 ^ (n / 2)) + p2;
            }
        }

        static int pwr2bin(int n)
        {
            if (n == 1)
            {
                return 10;
            }
            else
            {
                int z = pwr2bin(n / 2);
                return fastmultiply(z, z);
            }
        }

        static int dec2bin(int x)
        {
            int n = Convert.ToInt32(Math.Floor(Math.Log10(x) + 1));
            if (n == 1)
            {
                return binary[x];
            }
            else
            {
                int xl = x / 100;
                int xr = x % 100;
                return fastmultiply(pwr2bin(n / 2), dec2bin(xl)) + dec2bin(xr);
            }
        }
        static void Main(string[] args)
        {
            dec2bin(1257);
        }
    }
}
