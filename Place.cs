using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Place
    {
        public string Name { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public List<Person> People { get; set; }

        public List<Person> CollidedPeople { get; set; } = new List<Person>();

        public Place(string name, int sizex, int sizey)
        {
            Name = name;
            SizeX = sizex;
            SizeY = sizey;
            People = new List<Person>();

            // Temporary thing

            if (name != "Prison")
            {
                People.Add(new Police("Bengt", 1, 1, new List<Item>(), 0, 1));
                People.Add(new Police("Bengte", 5, 3, new List<Item>(), 1, 1));
                People.Add(new Citizen("Bengtson", 1, 1, new List<Item>(), -1, 1));
                People.Add(new Citizen("James", 3, 5, new List<Item>(), 1, -1));
                People.Add(new Citizen("Brown", 4, 4, new List<Item>(), 0, -1));
                People.Add(new Thief("Speedster", 3, 4, new List<Item>(), 1, 2));
                People.Add(new Thief("Speedster", 1, 1, new List<Item>(), 1, 2));
                People.Add(new Thief("Speedster", 1, 1, new List<Item>(), -1, 1));
                People.Add(new Citizen("Maya", 1, 3, new List<Item>(), 1, 0));
                People.Add(new Citizen("Georgia", SizeX / 2, SizeY / 3 + 1, new List<Item>(), 1, 0));
                People.Add(new Citizen("Bob", 7, SizeY / 2 - 1, new List<Item>(), 0, 1));
                People.Add(new Thief("Walker", 2, 2, new List<Item>(), 1, 1));
                People.Add(new Police("Watkins", 7, 4, new List<Item>(), 1, 1));
                People.Add(new Police("Watkins", 2, 1, new List<Item>(), 2, 2));
                People.Add(new Police("Watkins", 1, 8, new List<Item>(), 3, 3));
                People.Add(new Police("Watkins", 1, 8, new List<Item>(), 0, 0));
                People.Add(new Police("Watkins", 7, 8, new List<Item>(), 0, 0));
                People.Add(new Thief("Walker", 8, 8, new List<Item>(), -1, 0));
                People.Add(new Thief("Walker", 4, 8, new List<Item>(), -1, 0));
                People.Add(new Thief("Walker", 7, 5, new List<Item>(), 1, 1));
                People.Add(new Thief("Walker", 5, 2, new List<Item>(), 0, 1));
                People.Add(new Thief("Walker", 1, 2, new List<Item>(), 1, 1));
                People.Add(new Thief("Walker", 8, 2, new List<Item>(), 0, 0));

            }
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

            CollidedPeople.Clear();
            CollidedPeople = new List<Person>();

            while (y < SizeY + 1)
            {
                if (x < SizeX + 1)
                {
                    if (x == 0 || x == SizeX)
                    {
                        Console.Write("| |");
                    }
                    else if (y == 0 || y == SizeY)
                    {
                        Console.Write(" - ");
                    }
                    else
                    {
                        if (GetAtLocation(x, y).Count > 1)
                        {
                            Person person = GetAtLocation(x, y).FirstOrDefault(person => person is Thief || person is Police);
                            /*
                            foreach (var person in GetAtLocation(x,y))
                            {
                            */
                            if (person is Police)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(" P ");
                            }
                            else if (person is Thief)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" T ");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(" C ");
                            }
                            Console.ResetColor();
                            Thread.Sleep(1);
                            foreach (var item in GetAtLocation(x,y))
                            {
                                if (!CollidedPeople.Contains(item))
                                    CollidedPeople.Add(item);
                            }
                        }
                        else if (GetAtLocation(x,y).Count == 1)
                        {
                            Person person = GetAtLocation(x, y)[0];
                            /*
                            foreach (var person in GetAtLocation(x,y))
                            {
                            */
                                if (person is Police)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write(" P ");
                                }
                                else if (person is Thief)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(" T ");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write(" C ");
                                }
                                Console.ResetColor();
                                Thread.Sleep(1);
                            /*
                            }
                            */
                        }
                        else
                        {
                            Console.Write("   ");
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
            
            foreach (var item in People)
            {
                item.Move(SizeX, SizeY);
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
        private List<Person> GetAtLocation(int x, int y)
        {
            List<Person> persons = new List<Person>(); 
            x -= 1;
            y -= 1;
            foreach (Person person in People)
            {
                if (person.PositionX == x && person.PositionY == y)
                {
                    persons.Add(person);
                }
            }
            return persons;
        }
    }
}
