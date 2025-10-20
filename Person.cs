using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
