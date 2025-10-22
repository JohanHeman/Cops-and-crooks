using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Police : Person
    {



        public Police(string name, int positionX, int positionY, int directionX, int directionY) : base(name, positionX, positionY, directionX, directionY)
        {
            Name = name;
            PositionX = positionX;
            PositionY = positionY;
            DirectionX = directionX;
            DirectionY = directionY;

            Inventory = SetUpInventory();
        }

        public override void TransferBetweenInventory(Person person1, Person person2)
        {
            base.TransferBetweenInventory(person1, person2);
        }

        // greet function in Person.cs
    }
}
