using System.Collections;
using System.Globalization;

namespace ConsoleApp1
{
    internal class Program
    {
        static List<Place> places { get; set; }

        static void Main(string[] args)
        {
            places = new List<Place>();
            places.Add(new Place("City", 15,10));
            places.Add(new Place("Prison", 8,8));

            MainLoop();

        }

        static void TransferToPlace(Person p, string destination, string origin)
        {
            Place start = places.FirstOrDefault(place => place.Name == origin);
            Place end = places.FirstOrDefault(place => place.Name == destination);

            start.People.Remove(p);
            end.People.Add(p);
        }

        static void MainLoop()
        {
            foreach (var item in places)
            {
                Console.WriteLine("Place: {0} vilken är {1}x{2}", item.Name, item.SizeX, item.SizeY);
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

                WriteOutCheck(places[0]); // calling writeoutcheck

            }
        }

        static void WriteOut(Person person1, Person person2)
        {
            if(person1 is Police && person2 is Police || person1 is Police && person2 is Police)
            {
                Console.WriteLine($"The police officer {person1.Name} greets his colleague {person2.Name}               ");

            }
            else if (person1 is Police && person2 is Citizen || (person1 is Citizen && person2 is Police))
            {
                
                Console.WriteLine($"{person1.Name} greets {person2.Name}                                               ");
            }
            else if (person1 is Thief && person2 is Citizen)
            {
                
                Console.WriteLine($"The thief {person1.Name} steals a valuable item from {person2.Name}!                ");
                person1.TransferBetweenInventory(person1, person2);
            }

            else if(person1 is Citizen && person2 is Thief)
            {
                Console.WriteLine($"The thief {person2.Name} steals an item from {person1.Name}!                     ");
                person2.TransferBetweenInventory(person1, person2);
            }

            if(person1 is Police && person2 is Thief)
            {
                Thief thief = (Thief)person2;
                if (thief.Inventory.Count <= 0) return;
                SendToPrisson(thief);
                person1.TransferBetweenInventory(person1, person2);

                Console.WriteLine($"The police {person1.Name} captures {person2.Name} and sends them to prison!     ");
            }
            if(person1 is Thief && person2 is Police)
            {
                Thief thief = (Thief)person1;
                if (thief.Inventory.Count <= 0) return;
                SendToPrisson(thief);
                person2.TransferBetweenInventory(person1, person2);

                Console.WriteLine($"The police {person2.Name} captures {person1.Name} and sends them to prison!     ");
            }
        }
        

        static void WriteOutCheck(Place place) // compares all the people in place
        {
            Console.SetCursorPosition(0,22);
            Console.WriteLine("----News----");

            int min = 0;
            int row = 0;
            foreach(Person p in place.CollidedPeople) {
                for (int i = min; i < place.CollidedPeople.Count - 1; i++)
                {
                    Console.SetCursorPosition(0, 23 + row);
                    if (place.CheckCollision(place.CollidedPeople[i], p) == true) 
                    { 
                        WriteOut(place.CollidedPeople[i], p);
                        if (row >= 2)
                        {
                            row = 0;
                        }
                        else
                        {
                            row++;
                        }
                        Thread.Sleep(5);
                    }
                }
                min++;
            }
        }

        static void SendToPrisson(Thief thief)
        {

            places[1].People.Add(thief);
            places[0].People.Remove(thief);

            thief.TimeInPrison = thief.Inventory.Count * 10;
            thief.PositionX = 1;
            thief.PositionY = 14;
            thief.RandomizeDirection();
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


    }
}
