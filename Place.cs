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

        public List<Transport> Transports { get; set; } = new List<Transport>();




        public Place(string name, int sizex, int sizey)
        {
            Name = name;
            SizeX = sizex;
            SizeY = sizey;
            People = new List<Person>();

            // Temporary thing

            if (name != "Prison")
            {
                Generator gen = new Generator();
                int i = 0;
                while (i < sizex * 3)
                {
                    People.Add(gen.GeneratePerson(SizeX,SizeY));
                    i++;
                }
                People.Sort(CheckOrder);
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

        protected int CheckOrder(Person a, Person b)
        {
            int a_value = 0;
            int b_value = 0;

            if (a is Thief)
            {
                a_value = 2;
            }
            else if (a is Police)
            {
                a_value = 1;
            }
            if (b is Thief)
            {
                b_value = 2;
            }
            else if (b is Police)
            {
                b_value = 1;
            }

            if (a_value > b_value)
            {
                return -1;
            }
            else if (a_value < b_value)
            {
                return 1;
            }
            return 0;
        }

        public void CreateOrAddToTransport(Person p, string destination, string origin)
        {
            Transport tar = new Transport(new List<Person>(), "fake","fake");
            if (Transports.Count < 1)
            {
                Transports = new List<Transport>();
                tar = new Transport(new List<Person>(), destination, origin);
                tar.AddToTransport(p);
                Transports.Add(tar);
            }
            else
            {
                tar = Transports.FirstOrDefault(place => place.target == destination);
                if (tar == null)
                {
                    tar = new Transport(new List<Person>(), destination, origin);
                    tar.AddToTransport(p);
                    Transports.Add(tar);
                }
                else if (tar.target == destination)
                {
                    tar.AddToTransport(p);
                }
                else
                {
                    tar = new Transport(new List<Person>(), destination, origin);
                    tar.AddToTransport(p);
                    Transports.Add(tar);
                }
            }
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
                Console.ResetColor();
            }

            PerformActions();
        }

        public void PerformActions()
        {
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
