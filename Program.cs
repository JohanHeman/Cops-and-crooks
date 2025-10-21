namespace ConsoleApp1
{
    internal class Program
    {
        static List<Place> places { get; set; }

        static void Main(string[] args)
        {
            places = new List<Place>();
            places.Add(new Place("City", 10,10));
            places.Add(new Place("Prison", 8,8));
            foreach (var item in places)
            {
                Console.WriteLine("Place: {0} vilken är {1}x{2}", item.Name, item.SizeX,item.SizeY);
                item.Draw();
            }

            MainLoop();
            WriteOut();




        }

        static void TransferToPlace(Thief t, Police p)
        {
            if (t.PositionX == p.PositionX && t.PositionY == p.PositionY)
            {
                Place prison = places.FirstOrDefault(place => place.Name == "Prison");
                if (prison != null)
                {
                    Random rnd = new Random();
                    t.PositionX = rnd.Next(prison.SizeX);
                    t.PositionY = rnd.Next(prison.SizeY);
                    Console.WriteLine("Transfering {0}", p.Name);
                }
            }


        }

        static void MainLoop()
        {
            Console.WriteLine("Main Loop Called");
        }

        static void WriteOut(Person person1, Person person2)
        {
            Console.WriteLine("----News----");

            if(person1 is Police && person2 is Police || person1 is Police && person2 is Police)
            {
                Console.WriteLine($"The police officer {person1.Name} greets his colleague");
            } else if (person1 is Police && person2 is Citizen || person1 is Citizen && person2 is Police)
            {
                Console.WriteLine($"The police officer {person1.Name} greets the citizen {person2.Name}");
            } else if (person1 is Thief && person2 is Citizen)
            {
                Console.WriteLine($"The thief {person1.Name} steals an item from {person2.Name}! ");
            }

            else if(person1 is Citizen && person2 is Thief)
            {
                Console.WriteLine($"The thief {person2.Name} steals an item from {person1.Name}! ");
            }

            else if(person1 is Police && person2 is Thief)
            {
                Console.WriteLine($"The police {person1.Name} captures {person2.Name} and sends them to prisson! "); 
            } else if(person1 is Thief && person2 is Police)
            {
                Console.WriteLine($"The police {person2.Name} captures {person1.Name} and sends them to prisson! ");
            }
            




        }



    }
}
