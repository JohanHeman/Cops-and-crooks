namespace ConsoleApp1
{
    internal class Program
    {
        static List<Place> places { get; set; }

        static void Main(string[] args)
        {
            places = new List<Place>();
            places.Add(new Place("City", 10));
            places.Add(new Place("Prison", 8));
            foreach (var item in places)
            {
                Console.WriteLine("Place: {0} vilken är {1}x{1}", item.Name, item.Size);
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
                    t.PositionX = rnd.Next(prison.Size);
                    t.PositionY = rnd.Next(prison.Size);
                }
            }
        }

        static void MainLoop()
        {
            Console.WriteLine("Main Loop Called");
        }
        static void TransferToPlace(Person p)
        {
            Console.WriteLine("Transfering {0}", p.Name);
        }
        static void WriteOut()
        {
            Console.WriteLine("Write Out Method");
        }
    }
}
