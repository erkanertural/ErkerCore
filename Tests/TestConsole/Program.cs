

using ErkerCore.DataAccessLayer;
using ErkerCore.Entities;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Feature item= new MsSqlDataManager<MainDbContext>("").Get<Feature>(1);
            Console.WriteLine(item.Name);
            Console.ReadLine();
         
        }
    }
}
