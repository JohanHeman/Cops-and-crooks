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
            places.Add(new Place("City", 10,10));
            places.Add(new Place("Prison", 8,8));

            MainLoop();

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

                WriteOutCheck(places[0]); // calling writeoutcheck

            }
        }

        static void WriteOut(Person person1, Person person2)
        {
            Queue que = new();
           
            if(person1 is Police && person2 is Police || person1 is Police && person2 is Police)
            {
                que.Enqueue($"The police officer {person1.Name} greets his colleague            ");
                Console.WriteLine(que.Peek());
                que.Dequeue();

            }
            else if (person1 is Police && person2 is Citizen || (person1 is Citizen && person2 is Police))
            {
                
                que.Enqueue($"The police officer {person1.Name} greets the citizen {person2.Name}");
                
                Console.WriteLine(que.Peek());
                que.Dequeue();

            }
            else if (person1 is Thief && person2 is Citizen)
            {
                
                que.Enqueue($"The thief {person1.Name} steals an item from {person2.Name}! ");
                Console.WriteLine(que.Peek());
                que.Dequeue();

            }

            else if(person1 is Citizen && person2 is Thief)
            {
                que.Enqueue($"The thief {person2.Name} steals an item from {person1.Name}! ");
                Console.WriteLine(que.Peek());
                que.Dequeue();
                
            }

            if(person1 is Police && person2 is Thief)
            {

                Thief thief = (Thief)person2;
                SendToPrisson(thief);

                que.Enqueue($"The police {person1.Name} captures {person2.Name} and sends them to prisson! ");
                Console.WriteLine(que.Peek());
                Thread.Sleep(1000);
                que.Dequeue();
            }
            if(person1 is Thief && person2 is Police)
            {

                Thief thief = (Thief)person1;
                SendToPrisson(thief);

                que.Enqueue($"The police {person2.Name} captures {person1.Name} and sends them to prisson! ");
                Console.WriteLine(que.Peek());
                Thread.Sleep(1000);
                que.Dequeue();
            }
        }
        

        static void WriteOutCheck(Place place) // compares all the people in place
        {
            Console.SetCursorPosition(0,22);
            Console.WriteLine("----News----");

            int min = 0;
            foreach(Person p in place.CollidedPeople) {
                Console.SetCursorPosition(0, 23);
                for (int i = min; i < place.CollidedPeople.Count - 1; i++)
                {
                    if(place.CheckCollision(place.CollidedPeople[i], p) == true) 
                    { 
                        WriteOut(place.CollidedPeople[i], p);
                    }
                }
                Thread.Sleep(1);
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

        }

        static void SendToCity(Thief thief)
        {
            if(thief.TimeInPrison >= 0)
            {
                thief.TimeInPrison--;
            }

            places[1].People.Remove(thief);
            places[0].People.Add(thief);
        }


    }
}
