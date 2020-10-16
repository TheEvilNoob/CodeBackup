using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4150CartesiaPrime //Resubmitted version with comments and because I forgot I built in the Compare function into my Point class and was doing it manually.
{
    class Point
    {
        public int X, Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Compare(Point P) //compare the points to see if they're the same.
        {
            if (P.X == X && P.Y == Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class Program
    {
        static bool foundAnswer = false;
        static int a, b, c, d;
        static Point[] borgs;
        static void Main(string[] args)
        {
            Point start;
            int x, y, m, n;
            string input = Console.ReadLine();
            string[] splitstring = input.Split(' ');
            x = int.Parse(splitstring[0]);
            y = int.Parse(splitstring[1]);
            start = new Point(x, y);

            input = Console.ReadLine();
            splitstring = input.Split(' ');
            a = int.Parse(splitstring[0]);
            b = int.Parse(splitstring[1]);
            c = int.Parse(splitstring[2]);
            d = int.Parse(splitstring[3]);

            input = Console.ReadLine();
            m = int.Parse(input);

            input = Console.ReadLine();
            n = int.Parse(input);
            borgs = new Point[n];
            for (int i = 0; i < n; i++) //put all the borg locations into the borgs list.
            {
                input = Console.ReadLine();
                splitstring = input.Split(' ');
                borgs[i] = new Point(int.Parse(splitstring[0]), int.Parse(splitstring[1]));
            }

            int transporter = 1;
            Escape(start, m, transporter);

            if(foundAnswer == false)
            {
                Console.WriteLine("You will be assimilated! Resistance is futile!");
            }
        }

        static void Escape(Point start, int minutes, int transporter)
        {
            if(minutes == 0 || foundAnswer == true)
            {
                return;
            }

            Point location1, location2, location3, location4;
            int modifierX = deltaX(transporter);
            int modifierY = deltaY(transporter);

            location1 = new Point(start.X + modifierX, start.Y + modifierY); //precompute the four outcomes of the transporter so I only have to do it once.
            location2 = new Point(start.X - modifierX, start.Y + modifierY);
            location3 = new Point(start.X + modifierX, start.Y - modifierY);
            location4 = new Point(start.X - modifierX, start.Y - modifierY);

            if(location1.X == 0 && location1.Y == 0) //If I reach 0,0 then return and make sure we don't keep searching for a path.
            {
                Console.WriteLine("I had " + (minutes - 1) + " to spare! Beam me up Scotty!");
                foundAnswer = true;
                return;
            }
            if (location2.X == 0 && location2.Y == 0)
            {
                Console.WriteLine("I had " + (minutes - 1) + " to spare! Beam me up Scotty!");
                foundAnswer = true;
                return;
            }
            if (location3.X == 0 && location3.Y == 0)
            {
                Console.WriteLine("I had " + (minutes - 1) + " to spare! Beam me up Scotty!");
                foundAnswer = true;
                return;
            }
            if (location4.X == 0 && location4.Y == 0)
            {
                Console.WriteLine("I had " + (minutes - 1) + " to spare! Beam me up Scotty!");
                foundAnswer = true;
                return;
            }

            if (ContainsBorgs(location1) == false) //If you can move to the location, then move there. If there's a borg then skip it.
            {
                Escape(location1, minutes - 1, transporter+1);
            }
            if (ContainsBorgs(location2) == false)
            {
                Escape(location2, minutes - 1, transporter + 1);
            }
            if (ContainsBorgs(location3) == false)
            {
                Escape(location3, minutes - 1, transporter + 1);
            }
            if (ContainsBorgs(location4) == false)
            {
                Escape(location4, minutes - 1, transporter + 1);
            }

            /*int transporter = 1;
            int modifierX;
            int modifierY;

            while (minutes > 0)
            {
                modifierX = deltaX(transporter);
                modifierY = deltaY(transporter);
                transporter++;

                CheckBorgs(start, modifierX, modifierY);
                if (CheckBorgs(start, modifierX, modifierY) == false)
                {
                    start.X -= modifierX;
                }
                else if (CheckBorgs(start, -modifierX, modifierY) == false)
                {
                    start.X += modifierX;
                }
                else if (CheckBorgs(start, modifierX, -modifierY) == false)
                {
                    start.Y -= modifierY;
                }
                else if (CheckBorgs(start, -modifierX, -modifierY) == false)
                {
                    start.Y += modifierY;
                }
                minutes--;
                if (start.X == 0 && start.Y == 0)
                {
                    Console.WriteLine("I had " + minutes + " to spare! Beam me up Scotty!");
                    return;
                }
            }
            Console.WriteLine("You will be assimilated! Resistance is futile!");
            return;*/
        }

        static bool ContainsBorgs(Point P)
        {

            foreach (Point borg in borgs) //check to make sure there aren't any borgs in the location you want to move to.
            {
                if (borg.Compare(P))
                {
                    return true;
                }
            }
            //if (borgs.Contains(new Point(P.X, P.Y)))
            //{
                //return true;
            //}
            return false;
        }

        /*static bool CheckBorgs(Point P, int modX, int modY)
        {
            int AddX, AddY;

            AddX = P.X + modX;
            AddY = P.Y + modY;
            if (borgs.Contains(new Point(AddX, AddY)))
            {
                P.X = AddX;
                P.Y = AddY;
                return true;
            }
            return false;
        }*/

        static int deltaX(int t) //calculate deltaX
        {
            return (a * t) % b;
        }

        static int deltaY(int t) //calculate deltaY
        {
            return (c * t) % d;
        }
    }
}
