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

            // Temporary thing
            /*
            People.Add(new Police("Bengt", 2,2 ));
            People.Add(new Person("Bengti", 1,1));
            People.Add(new Person("Bengtil", 1, 5));
            People.Add(new Thief("Tjive", 2, 3));
            People.Add(new Person("Bosse", size / 2 - 1, size / 2 + 1));
            People.Add(new Thief("Tjuve", size / 2 + 1, size / 2));
            */
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
                        Console.Write("-");
                    }
                    else
                    {
                        if (GetAtLocation(x,y) != null)
                        {
                            Thread.Sleep(2);
                            Person person = GetAtLocation(x, y);
                            if (person is Thief)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("T");
                            }
                            else if (person is Police)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("P");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("C");
                            }
                            Console.ResetColor();
                        }
                        else
                        {
                            Thread.Sleep(1);
                            Console.Write(" ");
                        }
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
        public bool CheckCollision(Person a, Person b)
        {
            if (a.PositionX == b.PositionX && a.PositionY == b.PositionY)
            {
                return true;
            }

            return false;
        }
        // Temp method until it is established at other point
        private Person GetAtLocation(int x, int y)
        {
            x -= 1;
            y -= 1;
            foreach (Person person in People)
            {
                if (person.PositionX == x && person.PositionY == y)
                {
                    return person;
                }
            }
            return null;
        }
    }
}
