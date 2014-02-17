using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = UnicodeEncoding.UTF8;

            foreach (var culture in new[] { "en-us", "ja-jp", "ru-ru", "cs-cz" })
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                Console.WriteLine("Current Culture: " + Thread.CurrentThread.CurrentUICulture.EnglishName);
                Console.WriteLine(ClassLibrary.Strings.AlwaysEnglish);
                Console.WriteLine(ClassLibrary.Strings.Localizable);
                Console.WriteLine();
            }
        }
    }
}
