using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Person
    {
        static string Name {  get; set; }
        static int PositionX { get; set; }
        static int PositionY { get; set; }

        static List<Item> Inventory { get; set; }
        
        static int DirectionX { get; set; }
        static int DirectionY { get; set; }
    }
}
