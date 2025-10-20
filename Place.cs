using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Place
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public List<Person> People { get; set; }

        public Place(string name, int size)
        {
            Name = name;
            Size = size;
            People = new List<Person>();
        }
        public void Draw()
        {
            int x = 0;
            int y = 0;

            while (y < Size + 1)
            {
                if (x < Size + 1)
                {
                    if (x == 0 || x == Size)
                    {
                        Console.Write("|");
                    }
                    else if (y == 0 || y == Size)
                    {
                        Console.Write("---");
                    }
                    else
                    {
                        Console.Write(" X ");
                    }
                    x++;
                }
                else
                {
                    Console.WriteLine("");
                    x = 0;
                    y++;
                }
            }
        }
        public bool CheckCollision()
        {
            return false;
        }
    }
}
