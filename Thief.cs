using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Thief : Person
    {
        public int TimeInPrison { get; set; }


        public Thief(int timeInPrison, string name, int positionX, int positionY, List<Item> inventory, int directionX, int directionY) : base(name, positionX, positionY, inventory, directionX, directionY)
        {
            TimeInPrison = timeInPrison;
        }

        public override TransferBetweenInventory()
        {
            
        }

        public void SendToPrison()
        {
            
        }
    }
}
