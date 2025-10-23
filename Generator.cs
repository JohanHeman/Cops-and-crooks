using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Generator
    {
        public Person GeneratePerson(int sizeX,int sizeY)
        {
            Random rnd = new Random();
            int i = rnd.Next(0,4);
            switch (i)
            {
                case 0:
                    Thief t = new Thief(
                        GenerateName(),
                        SetLocation(rnd.Next(0,sizeX),sizeX),
                        SetLocation(rnd.Next(0, sizeY), sizeY)
                        );
                    return t;
                case 1:
                    Police p = new Police(
                        GenerateName(),
                        SetLocation(rnd.Next(0, sizeX), sizeX),
                        SetLocation(rnd.Next(0, sizeY), sizeY)
                        );
                    return p;
                default:
                    Citizen c = new Citizen(
                        GenerateName(),
                        SetLocation(rnd.Next(0, sizeX), sizeX),
                        SetLocation(rnd.Next(0, sizeY), sizeY)
                        );
                    return c;
            }
            return null;
        }

        public int SetLocation(int attemptedLoc, int size)
        {
            if (attemptedLoc >= size - 1)
            {
                return size - 1;
            }
            else if (attemptedLoc <= 1)
            {
                return 1;
            }
            return attemptedLoc;
        }

        public string GenerateName()
        {
            string[] name =
            {
                "Tracie", "Hunt",
                "Deana", "Obrien",
                "Steven", "King",
                "Helena", "Fowler",
                "Rebekah", "Roth",
                "Juana", "Guerra",
                "Margie", "Mathis",
                "Ronald", "Rose",
                "Emery", "Mcintyre",
                "Rodger", "Douglas",
                "Andre", "Macias",
                "Morton", "Bowen",
                "Jessie", "Solis",
                "Marco", "Cain",
                "Thanh", "Gray",
                "Elijah", "Nichols",
                "Maryann", "Espinoza",
                "Lolita", "Santana",
                "Rod", "Cherry",
                "Mara", "Wells",
                "Lacey", "Swanson",
                "Edgardo", "Ayers",
                "Fabian", "Foster",
                "Roger", "Mckay",
                "Ashlee", "Spencer",
                "Agustin", "Gould",
                "Lenore", "Mitchell",
                "Scotty","Mccarty",
                "Arnoldo","Mcknight",
                "Lemuel","Vaughn"
            };
            Random rnd = new Random();
            return name[rnd.Next(name.Length - 1)] + " " + name[rnd.Next(name.Length - 1)];
        }
    }
}
