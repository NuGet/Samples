extern alias ExampleAlias;
using System;

namespace PackageReferenceAliasesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var version = ExampleAlias.NuGet.Versioning.NuGetVersion.Parse("5.0.0");
            Console.WriteLine($"Version : {version}");
        }
    }
}
