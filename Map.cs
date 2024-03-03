using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace full_c__course
{
    public class Map
    {
        public int score = 0;
        public string FileSource { get; set; }
        public Player player { get; set; }
        public List<Ghost> Ghosts { get; set; }

        public List<List<char>> map { get; set; }

        public Map() {}
        public Map(string fileSource)
        {
            
            FileSource = fileSource;
            ReadMap();
        }

       public void ReadMap()
        {
            string[] mapFromFile = File.ReadAllLines(FileSource);
            map = new List<List<char>>(mapFromFile.Length);
            for (int i = 0; i < mapFromFile.Length; i++)
            {
                map.Add(new List<char>());
                for (int j = 0; j < mapFromFile[0].Length; j++)
                {
                    map[i].Add(mapFromFile[i][j]);
                }
            }
        }

        public void SetPlayerPosition()
        {
            map[player.yPosition][player.xPosition] = 'a';
        }

        public void SetGhostPosition()
        {
            foreach (var g in Ghosts)
            {
                map[g.yPosition][g.xPosition] = '@';
            }
        }


        public void printnmap()
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
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
                        foreach (var g in Ghosts)
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
        private bool hasDot()
        {
            if (map[player.yPosition][player.xPosition] == '.')
            {
                return true;
            }
            return false;
        }
        private void changePlayerPosition(char simvol)
        {
            map[player.yPosition][player.xPosition] = simvol;
        }
        public void changePlayerDirection(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:

                    if (map[player.yPosition - 1][player.xPosition] != '#')
                    {
                        changePlayerPosition(' ');
                        player.yPosition--;
                        if (hasDot())
                            score++;
                        changePlayerPosition('a');
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (map[player.yPosition + 1][player.xPosition] != '#')
                    {
                        changePlayerPosition(' ');
                        player.yPosition++;
                        if (hasDot())
                            score++;

                        changePlayerPosition('a');
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (map[player.yPosition][player.xPosition - 1] != '#')
                    {
                        changePlayerPosition(' ');
                        player.xPosition--;
                        if (hasDot())
                            score++;

                        changePlayerPosition('a');
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (map[player.yPosition][player.xPosition + 1] != '#')
                    {

                        changePlayerPosition(' ');
                        player.xPosition++;
                        if (hasDot())
                            score++;
                        changePlayerPosition('a');
                    }
                    break;

            }
        }
    }
}
