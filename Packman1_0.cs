using System;
using System.Collections.Generic;
using System.Text;

namespace full_c__course
{
    class Player
    {

        private int _xPosition;
        private int _yPosition;

        public int xPosition {  get => _xPosition; set => _xPosition = value; }
        public int yPosition { get => _yPosition; set => _yPosition = value; }

        public Player(int x, int y)
        {
            _xPosition = x;
            _yPosition = y;
        }
        public Player()
        {
            
        }

    }


    internal class Packman1_0
    {

        static void printnmap(List<string> map, int score)
        {
            for (int i = 0; i < map.Count; i++)
            {
                for(int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(map[i][j]);
                       
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                   else if (map[i][j] == 'a')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(map[i][j]);

                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                   else if (map[i][j] == '.')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(map[i][j]);

                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else 
                    {
                        Console.Write(map[i][j]);
                    }

                }
                Console.WriteLine();
            }

            Console.WriteLine(score);
        }


        static void changePlayerPosition(Player player, List<string> map, char simvol)
        {

            StringBuilder row = new StringBuilder(map[player.yPosition]);
            row[player.xPosition] = simvol;
            map[player.yPosition] = row.ToString();
        }

        static void changePlayerDirection(Player player, ConsoleKeyInfo pressedKey, List<string> map,ref int score)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:

                    if (map[player.yPosition -1][player.xPosition] != '#')
                    {
                        changePlayerPosition(player, map, ' ');
                        player.yPosition--;
                        if (hasDot(player, map))
                            score++;

                        changePlayerPosition(player, map, 'a');
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (map[player.yPosition +1][player.xPosition] != '#')
                    {
                        changePlayerPosition(player, map, ' ');
                        player.yPosition++;
                        if (hasDot(player, map))
                            score++;

                        changePlayerPosition(player, map, 'a');
                    }         
                    break;
                case ConsoleKey.LeftArrow:
                    if (map[player.yPosition ][player.xPosition-1] != '#')
                    {
                        changePlayerPosition(player, map, ' ');
                        player.xPosition--;
                        if (hasDot(player, map))
                            score++;

                        changePlayerPosition(player, map, 'a');
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (map[player.yPosition][player.xPosition +1] != '#')
                    {
                       
                        changePlayerPosition(player, map, ' ');
                        player.xPosition++;
                        if (hasDot(player, map))
                            score++;
                        changePlayerPosition(player, map, 'a');
                    }
                    break;

            }
        }

        static bool hasDot(Player  player, List<string> map)
        {
            if (map[player.yPosition][player.xPosition] == '.')
            {
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            string[] mapFromFile = File.ReadAllLines("map.txt");
            List<string> map = new List<string>();

            for (int i = 0; i < mapFromFile.Length; i++)
            {
                map.Add(mapFromFile[i]);
            }

            Console.CursorVisible = false;

            Player player = new Player(1, 1);
            changePlayerPosition(player, map, 'a');


            int score = 0;
            while (true)
            {                     
                Thread.Sleep(10);
                Console.Clear();
                printnmap(map, score);
                ConsoleKeyInfo pressedKey = Console.ReadKey();    
               changePlayerDirection(player, pressedKey, map,ref score);

            }
     


        }
    }
}
