﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading;

namespace full_c__course
{
    internal class Game
    {
        public static object ThreadLocker { get; private set; }

        static void Main(string[] args)
        {
            Map map = new Map("map.txt");
            map.player = new Player(30, 10);
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

            void DfsGhost(object? g)
            {
                Ghost currentGhost = (Ghost) g;

                    int[] dx = new int[] { 1, 0, -1, 0 };
                    int[] dy = new int[] { 0, -1, 0, 1 };

                    for (int i = 0; i < 4; i++)
                    {

                        int tx = currentGhost.xPosition + dx[i];
                        int ty = currentGhost.yPosition + dy[i];

                        if (map.map[ty][tx] != '#')
                        {
                            currentGhost.PreviuosXposition = currentGhost.xPosition;
                            currentGhost.PreviuosYposition = currentGhost.yPosition;

                            map.map[currentGhost.PreviuosYposition][currentGhost.PreviuosXposition] = currentGhost.PreviousState;
                            currentGhost.PreviousState = map.map[ty][tx];
                            currentGhost.xPosition = tx;
                            currentGhost.yPosition = ty;
                             break;
                        }

                    }

            }

            void GhostPursue(object? g)
            {
                Ghost currentGhost = (Ghost)g;

                // Поиск кратчайшего пути на основе принципов bfs с бесконечным преследованием
                var NewCoordinates = FindShortestWayForGhost.BFS(map, currentGhost);

                //Изменение предыдущих состояний, для того чтобы призраки правильно отрисовывались и не забирали точки на своём пути вместо игрока.
                currentGhost.PreviuosXposition = currentGhost.xPosition;
                currentGhost.PreviuosYposition = currentGhost.yPosition;



                map.map[currentGhost.PreviuosYposition][currentGhost.PreviuosXposition] = currentGhost.PreviousState;
                currentGhost.PreviousState = map.map[currentGhost.yPosition][currentGhost.xPosition];

                //изменение позиции самого призрака.
                currentGhost.xPosition = NewCoordinates.Item1;
                currentGhost.yPosition = NewCoordinates.Item2;
              

            }




            Ghost ghost1 = new Ghost(8, 6, ConsoleColor.White);
            Ghost ghost2 = new Ghost(5, 3, ConsoleColor.Green);

            map.Ghosts = new List<Ghost> { ghost1, ghost2 };
            map.SetGhostPosition();

            void ChangeGhostPosition ()
            {
                object lockObject = new object();
                lock (lockObject)
                {
                    foreach (Ghost g in map.Ghosts)
                    {
                        // ParameterizedThreadStart MYthreadStart = new ParameterizedThreadStart(DfsGhost);
                        ParameterizedThreadStart MYthreadStart = new ParameterizedThreadStart(GhostPursue);
                        Thread t = new Thread(MYthreadStart);
                        t.Start(g);
                        Console.WriteLine();
                    }
                }
            }


            void GameEnd()
            {
                while(true)
                {
                    Console.WriteLine("You are fucking looooseeeer" +
                        "    <-_->  Try another game");
                    Thread.Sleep(500);
                }
            }


            while (true)
            {                     

                //необходимо сделать движение призракам
                Thread.Sleep(200);
                Console.Clear();
                map.printnmap();
                map.changePlayerDirection(pressedKey);
                ChangeGhostPosition();
                if(map.Looser)
                {
                    GameEnd();
                    break;
                }

            }
        }
    }
}
