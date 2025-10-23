using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Transport
    {
        public List<Person> persons {  get; set; }
        public string target { get; set; }
        public string origin { get; set; }

        public Transport(List<Person> p, string tar, string place)
        {
            persons = p;
            target = tar;
            origin = place;
        }
        public void AddToTransport(Person p)
        {
            if (persons != null)
            {
                persons.Add(p);
            }
            else
            {
                persons = new List<Person>();
                persons.Add(p);
            }
        }
    }
}
