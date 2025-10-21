using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Person
    {
        public string Name {  get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public List<Item> Inventory { get; set; }
        
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }


        public Person(string name, int positionX, int positionY, List<Item> inventory, int directionX, int directionY)
        {
            Name = name;
            PositionX = positionX;
            PositionY = positionY;
            Inventory = inventory;
            DirectionX = directionX;
            DirectionY = directionY;
            
        }

        
        public virtual void TransferBetweenInventory(Person person1, Person person2)
        {
            if(person1 is Police && person2 is Thief)
            {
                foreach (Item i in person2.Inventory)
                {
                    person1.Inventory.Add(i); // takes all items to police from thief
                }
            } else if(person1 is Thief && person2 is Citizen )
            {
                int num = Random.Shared.Next(1, person2.Inventory.Count);

                person1.Inventory.Add(person2.Inventory[num]); // takes random item from citezen
            }

        }

            
        
        public virtual void Move(int sizex, int sizey)
        {
            PositionX += DirectionX;
            PositionY += DirectionY;
            if (PositionX >= sizex)
            {
                PositionX = 1;
            }
            else if (PositionX <= 0)
            {
                PositionX = sizex - 1;
            }
            if (PositionY >= sizey)
            {
                PositionY = 1;
            }
            else if (PositionY <= 0)
            {
                PositionY = sizey - 1;
            }
        }
    }

}
