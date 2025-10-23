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


        public Thief(string name, int positionX, int positionY) : base(name, positionX, positionY)
        {
            Name = name;
            PositionX = positionX;
            PositionY = positionY;
            RandomizeDirection();

            Inventory = SetUpInventory();
        }

        public override void TransferBetweenInventory(Person person1, Person person2)
        {
            base.TransferBetweenInventory(person1, person2);
        }
    }
}
