using System.Collections;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1
{
    internal class Main_Program
    {

        List<Place> places { get; set; }

        static int total_y { get; set; } = 2;


        static List<Person> robedCitizens = new List<Person>();

        public void Main(string[] args)
        {
            places = new List<Place>();
            places.Add(new Place("City", 15, 10));
            places.Add(new Place("Prison", 8, 8));

            foreach (var place in places)
            {
                total_y += place.SizeY;
            }

            MainLoop();

        }







        public void TransferToPlace(Person p, string destination, string origin)
        {
            Place start = places.FirstOrDefault(place => place.Name == origin);
            Place end = places.FirstOrDefault(place => place.Name == destination);

            start.People.Remove(p);
            end.People.Add(p);
            p.DirectionX = 1;
            p.Move(end.SizeX, end.SizeY);
        }

        static ConsoleKeyInfo key { get; set; }
        static bool second { get; set; } = false;

        public void MainLoop()
        {
            int t = 0;
            while (t < 1000)
            {
                Console.Clear();
                if (second)
                {
                    SecondaryLoop();
                }
                else
                {
                    PrimaryLoop();
                }
                t++;
            }
        }

        public void PrimaryLoop()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.SetCursorPosition(0, 0);
                foreach (var item in places)
                {
                    item.Draw();
                    if (item.People.Count > 0)
                    {
                        Thread.Sleep(1);
                    }
                }
                WriteOutCheck(places[0]); // calling writeoutcheck
                Thread.Sleep(100);
                foreach (var item in places[1].People)
                {
                    SendToCity(item as Thief);
                }
                foreach (var item in places)
                {
                    foreach (var transport in item.Transports)
                    {
                        foreach (var people in transport.persons)
                        {
                            TransferToPlace(people, transport.target, transport.origin);
                        }
                    }
                    item.Transports = new List<Transport>();
                }

                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.S:
                            second = true;
                            i = 100;
                            Console.Clear();
                            break;
                        default:
                            break;
                    }
                }


            }
        }

        public void SecondaryLoop()
        {
            Console.Clear();
            for (int i = 0; i < 200; i++)
            {
                Console.SetCursorPosition(0, 0);
                foreach (var item in places)
                {
                    item.PerformActions();
                }
                WriteOutList(places[0]); // calling writeoutcheck
                foreach (var item in places[1].People)
                {
                    SendToCity(item as Thief);
                }
                foreach (var item in places)
                {
                    foreach (var transport in item.Transports)
                    {
                        foreach (var people in transport.persons)
                        {
                            TransferToPlace(people, transport.target, transport.origin);
                        }
                    }
                    item.Transports = new List<Transport>();
                }

                Thread.Sleep(25);
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.S:
                            second = false;
                            i += 500;
                            Console.Clear();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public string WriteOut(Person person1, Person person2)
        {
            if (person1 == person2) return "";
            if (person1 is Police && person2 is Police || person1 is Police && person2 is Police)
            {
                return $"The police officer {person1.Name} greets his colleague {person2.Name}               ";

            }
            else if (person1 is Police && person2 is Citizen || (person1 is Citizen && person2 is Police))
            {
                person2.RandomizeDirection();
                return $"{person1.Name} greets {person2.Name}                                               ";
            }
            else if (person1 is Thief && person2 is Citizen)
            {
                if (!person1.CanStealFromInventory())
                {
                    return "";
                }
                person1.TransferBetweenInventory(person2, person1);

                if (!robedCitizens.Contains(person2))
                {
                    robedCitizens.Add(person2);
                }

                person1.RandomizeDirection();
                return $"The thief {person1.Name} steals a valuable item from {person2.Name}!                ";
            }

            else if (person1 is Citizen && person2 is Thief)
            {
                if (!person2.CanStealFromInventory())
                {
                    return "";
                }
                person2.TransferBetweenInventory(person1, person2);

                if (!robedCitizens.Contains(person1))
                {
                    robedCitizens.Add(person1);
                }
                person2.RandomizeDirection();
                return $"The thief {person2.Name} steals an item from {person1.Name}!                     ";

            }
            if (person1 is Thief && person2 is Police)
            {
                if (person1.CheckInventoryForTheft())
                {
                    SendToPrisson(person1);
                    Thief t = (Thief)person1;

                    if (person1.Inventory.Count <= 0)
                    {
                        t.TimeInPrison = 20;
                    }
                    else if (person1.Inventory.Count >= 1)
                    {
                        t.TimeInPrison = person1.Inventory.Count * 20;
                    }
                    person1 = t;

                    person2.TransferBetweenInventory(person1, person2);

                    old_news.Add($"The police {person2.Name} captures {person1.Name} and sends them to prison!");

                    return $"The police {person2.Name} captures {person1.Name} and sends them to prison!     ";
                }
            }
            else if (person1 is Police && person2 is Thief)
            {
                if (person2.CheckInventoryForTheft())
                {
                    SendToPrisson(person2);
                    Thief t = (Thief)person2;

                    if (person2.Inventory.Count <= 0)
                    {
                        t.TimeInPrison = 20;
                    }
                    else if (person2.Inventory.Count >= 1)
                    {
                        t.TimeInPrison = person2.Inventory.Count * 20;
                    }
                    person2 = t;

                    person1.TransferBetweenInventory(person2, person1);

                    old_news.Add($"The police {person1.Name} captures {person2.Name} and sends them to prison!");

                    return $"The police {person1.Name} captures {person2.Name} and sends them to prison!     ";
                }
            }
            return "";
        }


        public void WriteOutCheck(Place place) // compares all the people in place
        {
            Console.SetCursorPosition(0, total_y);
            Console.WriteLine("----News----");

            WriteNews(total_y + 1, 4, place);
            WriteStatus(total_y + 5);
        }

        static List<String> old_news { get; set; } = new List<String>();

        public void WriteOutList(Place place) // compares all the people in place
        {
            Console.SetCursorPosition(0, 0);
            List<Person> temp = new List<Person>();

            int height = Console.WindowHeight - 1;

            foreach (Person person in place.People)
            {
                if (!temp.Contains(person) && temp.Count < height)
                {
                    temp.Add(person);
                }
            }


            for (int i = 0; i < temp.Count; i++)
            {
                var person = temp[i];
                ClearCurrentConsoleLine();
                if (person is Thief)
                {
                    if (person.CheckInventoryForTheft())
                    {
                        Console.Write("Grand Thief: ");
                    }
                    else
                    {
                        Console.Write("Thief: ");
                    }
                }
                else if (person is Police)
                {
                    Console.Write("Police: ");
                }
                else
                {
                    Console.Write("Civilian: ");
                }
                Console.Write(person.Name);
                if (person.Inventory.Count > 0)
                {
                    Console.Write(": {0} ", person.GetInventory());
                }
                else
                {
                    Console.Write("");
                }
                Console.Write(": ({0},{1})", person.PositionX, person.PositionY);
                Console.WriteLine("");
                Thread.Sleep(1);
            }

            Console.SetCursorPosition(0, temp.Count);

            Console.WriteLine("----News----");

            WriteNews(temp.Count + 1, 2, place);

            WriteStatus(temp.Count + 3);
        }

        public void WriteNews(int pos, int max, Place place)
        {
            int min = 0;
            int row = 0;

            List<string> news = new List<string>();

            foreach (Person p in place.CollidedPeople)
            {
                min++;
                for (int i = min; i < place.CollidedPeople.Count - 1; i++)
                {
                    if (place.CheckCollision(place.CollidedPeople[i], p) == true)
                    {
                        if (WriteOut(place.CollidedPeople[i], p).Length > 1)
                        {
                            news.Add(WriteOut(place.CollidedPeople[i], p));
                        }
                    }
                }
            }

            List<string> old_news_rem = new List<string>();

            if (news.Count < max && old_news.Count > 0)
            {
                List<string> temp = new List<string>();
                foreach (string s in news)
                {
                    if (s.Length > 1 && temp.Count < max)
                    {
                        temp.Add(s);
                    }
                }
                foreach (string s in old_news)
                {
                    if (temp.Count < max && s.Length > 1)
                    {
                        temp.Add(s);
                        old_news_rem.Add(s);
                    }
                }
                news = temp;
            }
            foreach (var s in old_news_rem)
            {
                old_news.Remove(s);
            }

            foreach (var txt in news)
            {
                if (txt.Length > 1)
                {
                    Console.SetCursorPosition(0, pos + row);
                    ClearCurrentConsoleLine();
                    Console.WriteLine(txt);
                    row++;
                    Thread.Sleep(10);
                    if (!old_news.Contains(txt) && !old_news_rem.Contains(txt))
                    {
                        old_news.Add(txt);
                    }
                }
                if (row >= max)
                {
                    break;
                }
            }
        }

        public void SendToPrisson(Person thief)
        {

            places[1].CreateOrAddToTransport(thief, places[1].Name, places[1].Name);
        }

        public void SendToCity(Thief thief)
        {
            if (thief.TimeInPrison >= 0)
            {
                thief.TimeInPrison--;
                return;
            }

            places[1].CreateOrAddToTransport(thief, places[0].Name, places[1].Name);
            //places[1].People.Remove(thief);
            //places[0].People.Add(thief);
        }

        public void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }


        public void WriteStatus(int position)
        {
            Console.SetCursorPosition(0, position);
            Console.WriteLine("---Status---");
            Console.WriteLine("Number of arrested thieves: " + places[1].People.Count);
            //Console.WriteLine("Number of free thieves: " + places[0].GetThieves());
            Console.WriteLine("Amount of robbed citizens: " + robedCitizens.Count);
        }
    }
}