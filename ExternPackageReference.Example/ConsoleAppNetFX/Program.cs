extern alias ClassLib2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Callling into ClassLib" + new Library.Lib().ClassLibNumber());
            Console.WriteLine("Callling into ClassLib" + new ClassLib2.Library.Lib().ClassLibNumber());
            Console.WriteLine("Goodbye World!");
        }
    }
}
