using CESI.BS.EasySave;
using System;
namespace CesiEasySave
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Environment.CurrentDirectory);
            new CesiEasySave.Controller.Controller();

        }
    }
}
