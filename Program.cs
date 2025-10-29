using System.Collections;
using System.Globalization;

namespace ConsoleApp1
{
    internal class Program
    {
        static List<Place> places { get; set; }

        static int total_y { get; set; } = 2;

        static void Main(string[] args)
        {
            places = new List<Place>();
            places.Add(new Place("City", 22,10));
            places.Add(new Place("Prison", 8,8));

            foreach (var place in places)
            {
                total_y += place.SizeY;
            }

            MainLoop();

        }

        static void TransferToPlace(Person p, string destination, string origin)
        {
            Place start = places.FirstOrDefault(place => place.Name == origin);
            Place end = places.FirstOrDefault(place => place.Name == destination);

            start.People.Remove(p);
            end.People.Add(p);
            p.DirectionX = 1;
            p.Move(end.SizeX,end.SizeY);
        }

        static ConsoleKeyInfo key {  get; set; }
        static bool second { get; set; } = false;

        static void MainLoop()
        {
            int t = 0;
            while (t < 1000)
            {
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

        static void PrimaryLoop()
        {
            foreach (var item in places)
            {
                item.Draw();
            }
            for (int i = 0; i < 100; i++)
            {
                Console.SetCursorPosition(0, 0);
                foreach (var item in places)
                {
                    item.Draw();
                    Thread.Sleep(1);
                }
                WriteOutCheck(places[0]); // calling writeoutcheck
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

        static void SecondaryLoop()
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

                Thread.Sleep(1000);
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

        static string WriteOut(Person person1, Person person2)
        {
            if (person1 == person2) return "";
            if(person1 is Police && person2 is Police || person1 is Police && person2 is Police)
            {
                return $"The police officer {person1.Name} greets his colleague {person2.Name}               ";

            }
            else if (person1 is Police && person2 is Citizen || (person1 is Citizen && person2 is Police))
            {
                
                return $"{person1.Name} greets {person2.Name}                                               ";
            }
            else if (person1 is Thief && person2 is Citizen)
            {

                person1.TransferBetweenInventory(person1, person2);
                person1.RandomizeDirection();
                return $"The thief {person1.Name} steals a valuable item from {person2.Name}!                ";
            }

            else if(person1 is Citizen && person2 is Thief)
            {
                person2.TransferBetweenInventory(person1, person2);
                person2.RandomizeDirection();
                return $"The thief {person2.Name} steals an item from {person1.Name}!                     ";
            }
            if (person1 is Thief && person2 is Police)
            {
                 SendToPrisson(person1);
                 person2.TransferBetweenInventory(person1, person2);
                 return $"The police {person2.Name} captures {person1.Name} and sends them to prison!     ";
            }
            else if(person1 is Police && person2 is Thief)
            {
                SendToPrisson(person2);
                person1.TransferBetweenInventory(person2, person1);
                return $"The police {person1.Name} captures {person2.Name} and sends them to prison!     ";
            }
            return "";
        }
        

        static void WriteOutCheck(Place place) // compares all the people in place
        {
            Console.SetCursorPosition(0, total_y);
            Console.WriteLine("----News----");

            WriteNews(total_y + 1, 4, place);
        }

        static List<String> old_news {  get; set; } = new List<String>();

        static void WriteOutList(Place place) // compares all the people in place
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("----News----");

            WriteNews(1, 10, place);

            Thread.Sleep(50);
        }

        static void WriteNews(int pos, int max, Place place)
        {
            int min = 0;
            int row = 0;
            List<string> news = new List<string>();
            foreach (Person p in place.CollidedPeople)
            {
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
                min++;
            }

            if (news.Count < max && old_news.Count > 0)
            {
                List<string> temp = new List<string>();
                List<string> old_news_rem = new List<string>();
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
                foreach (var s in old_news_rem)
                {
                    old_news.Remove(s);
                }
            }

            row = 0;
            foreach (var txt in news)
            {
                if (txt.Length > 1)
                {
                    Console.SetCursorPosition(0, pos + row);
                    ClearCurrentConsoleLine();
                    Console.WriteLine(txt);
                    row++;
                    Thread.Sleep(10);
                    if (!old_news.Contains(txt))
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

        static void SendToPrisson(Person thief)
        {
            Thief t = (Thief)thief;

            if (thief.Inventory.Count <= 0)
            {
                t.TimeInPrison = 10;
            }
            else
            {
                t.TimeInPrison = thief.Inventory.Count * 10;
            }
            thief = t;
            places[1].CreateOrAddToTransport(thief, places[1].Name, places[1].Name);
        }

        static void SendToCity(Thief thief)
        {
            if(thief.TimeInPrison >= 0)
            {
                thief.TimeInPrison--;
                return;
            }

            places[1].CreateOrAddToTransport(thief, places[0].Name, places[1].Name);
            //places[1].People.Remove(thief);
            //places[0].People.Add(thief);
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

    }
}
