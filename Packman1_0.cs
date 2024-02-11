using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

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
    class Ghost : Player
    {

        ConsoleColor _color;

        public ConsoleColor Color {get => _color; set => _color = value; }

        public Ghost(int x, int y, ConsoleColor c) : base( x,  y)
        {
            _color = c;
        }


    }

    



    internal class Packman1_0
    {
        private static bool player_loose = false;
        static void FullStop()
        {
            //какая-то логика полонй остановки
        }

        static void GhostDFS(Player player, Ghost ghost, ref List<List<char>> map)
        {
            if (player.xPosition == ghost.xPosition && player.yPosition == ghost.yPosition)
            {
                player_loose = true;

            }
            int[] dx = new int[] { 1, 0, -1, 0 };
            int[] dy = new int[] { 0, -1, 0, 1 };

            for (int i = 0; i < 4; i++)
            {

                int tx = ghost.xPosition + dx[i];
                int ty = ghost.yPosition + dy[i];

                if (map[ty][tx] != '#')
                {
                    map[ghost.yPosition][ghost.xPosition] = ' ';
                    ghost.xPosition = tx;
                    ghost.yPosition = ty;
                    map[ghost.yPosition][ghost.xPosition] = '@';
                    GhostDFS(player, ghost,ref map);
                }
                    
            }

        }


        static void printnmap(List<List<char>> map, int score,ref List<Ghost> ghosts)
        {
            for (int i = 0; i < map.Count; i++)
            {
                for(int j = 0; j < map[i].Count; j++)
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
                    else if (map[i][j] == '@')
                    {
                        //значит тут призрак и хотелось бы цвет призрака
                        //можно проверить какому призраку соответсвует и нарисовать его цвет
                        foreach(var g in ghosts)
                        {
                            if (g.yPosition == i && g.xPosition == j)
                                Console.ForegroundColor = g.Color;


                        }
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


        static void changePlayerPosition(Player player,ref List<List<char>> map, char simvol)
        {
            map[player.yPosition][player.xPosition] = simvol;
        }

        static void changePlayerDirection(Player player, ConsoleKeyInfo pressedKey,ref List<List<char>> map, ref int score)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:

                    if (map[player.yPosition -1][player.xPosition] != '#')
                    {
                        changePlayerPosition(player, ref map, ' ');
                        player.yPosition--;
                        if (hasDot(player, map))
                            score++;
                        changePlayerPosition(player, ref map, 'a');
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (map[player.yPosition +1][player.xPosition] != '#')
                    {
                        changePlayerPosition(player, ref map, ' ');
                        player.yPosition++;
                        if (hasDot(player, map))
                            score++;

                        changePlayerPosition(player, ref map, 'a');
                    }         
                    break;
                case ConsoleKey.LeftArrow:
                    if (map[player.yPosition ][player.xPosition-1] != '#')
                    {
                        changePlayerPosition(player, ref map, ' ');
                        player.xPosition--;
                        if (hasDot(player, map))
                            score++;

                        changePlayerPosition(player, ref map, 'a');
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (map[player.yPosition][player.xPosition +1] != '#')
                    {
                       
                        changePlayerPosition(player, ref map, ' ');
                        player.xPosition++;
                        if (hasDot(player, map))
                            score++;
                        changePlayerPosition(player, ref map, 'a');
                    }
                    break;

            }
        }

        static bool hasDot(Player  player, List<List<char>> map)
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


            List<List<char>> map = new List<List<char>>(mapFromFile.Length);
            for (int i = 0; i < mapFromFile.Length; i++)
            {
                map.Add(new List<char>());
                for (int j = 0; j < mapFromFile[0].Length; j++)
                {
                    map[i].Add(mapFromFile[i][j]);
                }
            }


            Console.CursorVisible = false;

            Player player = new Player(1, 1);
            changePlayerPosition(player, ref map, 'a');
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


            changePlayerPosition(ghost1, ref map, '@');
            changePlayerPosition(ghost2, ref map, '@');

            List<Ghost> ghosts = new List<Ghost>();
            ghosts.Add(ghost1);
            ghosts.Add(ghost2);

            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        foreach(var g in ghosts)
            //        {
            //            GhostDFS(player, g, ref map);
            //        }
            //    }
            //});



            int score = 0;
            while (true)
            {                     
                Thread.Sleep(150);
                Console.Clear();
                printnmap(map, score, ref ghosts);
               changePlayerDirection(player, pressedKey, ref map,ref score);

            }
     


        }
    }
}
