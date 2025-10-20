namespace ConsoleApp1
{
    internal class Program
    {
        static List<Place> places { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
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
