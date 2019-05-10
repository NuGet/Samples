extern alias ClassLib2;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Callling into ClassLib" + new Library.Lib().ClassLibNumber());
            Console.WriteLine("Callling into ClassLib" + new ClassLib2.Library.Lib().ClassLibNumber());
            Console.WriteLine("Goodbye World!");
        }
    }
}
