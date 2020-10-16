using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150PS7_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            long result;
            while ((input = Console.ReadLine()) != null && input != "")
            {
                string[] splitString = input.Split(' ');
                switch (splitString[0])
                {
                    case "gcd":
                        Console.WriteLine(GreatestCommonDivisor(Convert.ToInt64(splitString[1]), Convert.ToInt64(splitString[2])));
                        break;
                    case "exp":
                        Console.WriteLine(Exponentiation(Convert.ToInt64(splitString[1]), Convert.ToInt64(splitString[2]), Convert.ToInt64(splitString[3])));
                        break;
                    case "inverse":
                        result = Inverse(Convert.ToInt64(splitString[1]), Convert.ToInt64(splitString[2]));
                        if (result <= 0) {
                            Console.WriteLine("none");
                        }
                        else
                        {
                            Console.WriteLine(result);
                        }
                        break;
                    case "isprime":
                        Console.WriteLine(IsPrime(Convert.ToInt64(splitString[1])));
                        break;
                    case "key":
                        KeyGeneration(Convert.ToInt64(splitString[1]), Convert.ToInt64(splitString[2]));
                        break;
                    case "testinverse":
                        TestInverse(Convert.ToInt64(splitString[1]), Convert.ToInt64(splitString[2]));
                        break;
                }
            }
        }

        static long GreatestCommonDivisor(long i, long j)
        {
            if(j == 0)
            {
                return i;
            }

            long d = i % j;

            return GreatestCommonDivisor(j, d);
        }

        static long Exponentiation(long a, long b, long N)
        {
            long x = 1, y = a;

            while (b > 0)
            {
                if (b % 2 == 1)
                {
                    x *= y;
                    x %= N;
                }
                y *= y;
                y %= N;
                b /= 2;
            }
            return x;

            /*x %= N;

            long result = 1;
            while (y > 0)
            {
                if((y & 1) == 1)
                {
                    result = (result * x) % N;
                }
                y /= 2;
                x = (x * x) % N;
            }
            return result;*/
        }

        static long Inverse(long a, long N)
        {
            if (GreatestCommonDivisor(a, N) != 1) //If it doesn't have an inverse return and print none.
            {
                return -1;
            }

            long x = 0, y = 1, z = N, previousX = 1, previousY = 0, previousZ = a;

            while (z > 0)
            {
                long divideResult = previousZ / z;
                long temp = z;
                z = previousZ - divideResult * temp;
                previousZ = temp;
                temp = x;
                x = previousX - divideResult * temp;
                previousX = temp;
                temp = y;
                y = previousY - divideResult * temp;
                previousY = temp;
            }

            if (previousX < 0) //If the number is negative make it the positive equivalent
            {
                previousX += N;
            }
            return previousX;

            /*
            if (GreatestCommonDivisor(a, N) != 1) //If it doesn't have an inverse return and print none.
            {
                return -1;
            }

            //long x = 1, y = 0, previousX = 0, previousY = 1, N0 = N;
            //long divideResult, newA, newX, newY;

            while (a > 0)
            {
                divideResult = N0 / a;
                newA = N0 % a;
                newX = previousX - divideResult * x;
                newY = previousY - divideResult * y;
                previousX = x;
                previousY = y;
                x = newX;
                y = newY;
                N0 = a;
                a = newA;
            }

            if (previousX < 0) //If the answer is negative, make it the positive equivalent.
            {
                previousX += N;
            }
            return previousX;*/

            /*long N0 = N, result = 0, m = 1, x, y;

            while (a > 0)
            {
                x = N0 / a; 
                y = a; 
                a = N0 % a; 
                N0 = y; 
                y = m;
                m = result - x * y;
                result = y;
            }

            result %= N;

            if (result < 0) 
            {
                result += N;
            }

            return result;*/
        }

        static string IsPrime(long n)
        {
            if (Exponentiation(2, n - 1, n) != 1)
            {
                return "no";
            }
            if (Exponentiation(3, n - 1, n) != 1)
            {
                return "no";
            }
            if (Exponentiation(5, n - 1, n) != 1)
            {
                return "no";
            }
            return "yes";
        }

        static void KeyGeneration(long p, long q)
        {
            long e = 0, d;
            long n = p * q;
            long limit = (p - 1) * (q - 1);

            for(int i = 2; i < limit; i++)
            {
                long result = GreatestCommonDivisor(i, limit);
                if (result == 1)
                {
                    e = i;
                    break;
                }
            }

            d = Inverse(e, limit);

            Console.WriteLine(n + " " + e + " " + d);
        }

        static void TestInverse(long i, long j)
        {
            long result, counter = 0;
            for (long k = i; k < j; k++)
            {
                result = Inverse(k, j);

                if (result != -1) {
                    counter++;
                }
            }
            Console.WriteLine(counter);
        }
    }
}
