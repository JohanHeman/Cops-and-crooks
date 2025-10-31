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

        public Person(string name, int positionX, int positionY)
        {
            Name = name;
            PositionX = positionX;
            PositionY = positionY;
            RandomizeDirection();

            Inventory = SetUpInventory();
        }

        public void RandomizeDirection()
        {
            Random rnd = new Random();
            DirectionX = rnd.Next(-1,2);
            DirectionY = rnd.Next(-1, 2);
            if (DirectionX == 0 && DirectionY == 0)
            {
                RandomizeDirection();
            }
        }

        protected List<Item> SetUpInventory()
        {
            List<Item> inventory = new List<Item>();
            
            Random rnd = new Random();

            int i = rnd.Next(1,3);
            int type = rnd.Next(5);

            Item item = new Item();
            while (i > 0)
            {
                type = rnd.Next(6);
                item = new Item();
                switch (type)
                {
                    case 1:
                        item.Name = "Klocka";
                        item.OriginPerson = this;
                        break;
                    case 2:
                        item.Name = "Sten";
                        item.OriginPerson = this;
                        break;
                    case 3:
                        item.Name = "Plånbok";
                        item.OriginPerson = this;
                        break;
                    case 4:
                        item.Name = "Burk";
                        item.OriginPerson = this;
                        break;
                    case 5:
                        item.Name = "Katt";
                        item.OriginPerson = this;
                        break;
                    default:
                        item.Name = "Pinne";
                        item.OriginPerson = this;
                        break;
                }
                inventory.Add(item);
                i--;
            }
            return inventory;
        }

        public string GetInventory()
        {
            string item_string = "";
            int i = 0;
            foreach (var item in Inventory)
            {
                item_string += item.Name;
                if (item.OriginPerson != this)
                {
                    item_string += " (s)";
                }
                if (Inventory.Count > 1 && i < Inventory.Count - 1)
                {
                    item_string += ", ";
                }
                i++;
            }
            return item_string;
        }

        public bool CheckInventoryForTheft()
        {
            foreach (var item in Inventory)
            {
                if (item.OriginPerson != this)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CanStealFromInventory()
        {
            return Inventory.Count < 4;
        }

        // Transfer from Person 1 to Person 2
        public virtual void TransferBetweenInventory(Person person1, Person person2)
        {
            if (person2.Inventory.Count > 4) return;
            if (person1.Inventory.Count <= 0) return;

            int num = Random.Shared.Next(0, person1.Inventory.Count - 1);

            person2.Inventory.Add(person1.Inventory[num]); // takes random item from citezen
            person1.Inventory.Remove(person1.Inventory[num]); // takes random item from citezen
        }

            
        
        public virtual void Move(int sizex, int sizey)
        {
            PositionX += DirectionX;
            PositionY -= DirectionY;
            if (PositionX >= sizex)
            {
                PositionX = 0;
            }
            else if (PositionX < 0)
            {
                PositionX = sizex - 2;
            }
            if (PositionY >= sizey)
            {
                PositionY = 0;
            }
            else if (PositionY < 0)
            {
                PositionY = sizey - 2;
            }
        }
    }

}
