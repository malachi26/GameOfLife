using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace GameOfLife_CR
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath =@"c:\users\lyle.hart\documents\visual studio 2015\Projects\GameOfLife-CR\GameOfLife-CR\Grid.txt";

            // We use ReadAllLines because Windows adds \r\n and this gets rid of \r
            string gridText = string.Join("\n", File.ReadAllLines(filePath));

            int interval = 500;
            int generation = 0;

            Game game = new Game(gridText);
            
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Generation: {generation++}");
                Console.WriteLine(game.ShowGrid());
                game.NextGeneration();

                // Console.ReadKey()
                Thread.Sleep(interval);
            }

            // End
        }
    }


}
