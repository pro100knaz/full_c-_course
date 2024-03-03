using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace full_c__course
{
    internal class Packman1_0
    {
        static void Main(string[] args)
        {
            Map map = new Map("map.txt");
            map.player = new Player(1, 1);
            map.SetPlayerPosition();

            Console.CursorVisible = false;
           
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo();

            Task.Run(() => 
            {
                        while(true)
                        {
                             pressedKey = Console.ReadKey();
                        }
            });

            Ghost ghost1 = new Ghost(8, 6, ConsoleColor.White);
            Ghost ghost2 = new Ghost(5, 3, ConsoleColor.Green);

            map.Ghosts = new List<Ghost> { ghost1, ghost2 };
            map.SetGhostPosition();

            while (true)
            {                     
                Thread.Sleep(150);
                Console.Clear();
                map.printnmap();
                map.changePlayerDirection(pressedKey);

            }
        }
    }
}
