using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace full_c__course
{
    public static class FindShortestWayForGhost
    {
        private const int INF = 1000000;
        public static (int, int) BFS(Map map, Ghost curGhost)
        {
            int finishX = map.player.xPosition;
            int finishY = map.player.yPosition;

            int width = map.MapWidth;
            int height = map.MapHeight;

            List<List<int>> dist = new List<List<int>>(height);

            for (int i = 0; i < height; i++)
            {
                List<int> innerList = new List<int>(width);
                innerList.AddRange(Enumerable.Repeat(INF, width));
                dist.Add(innerList);
            }

            Queue<(int, int)> q = new Queue<(int, int)>();
            q.Enqueue((curGhost.yPosition, curGhost.xPosition));



            List<int> dy = new List<int> { -1, 0, 1, 0 };
            List<int> dx = new List<int> { 0, 1, 0, -1 };



                dist[curGhost.yPosition][curGhost.xPosition] = 1;
            while (q.Count > 0)
            {
                (int y, int x) Coordinates = q.Dequeue();

                bool flag = false;

                for (int i = 0; i < dy.Count; i++)
                {
                    int ty = Coordinates.y + dy[i];
                    int tx = Coordinates.x + dx[i];


                    if (0 <= ty && ty < height && 0 <= tx && tx < width &&
                map.map[ty][tx] != '#' && dist[ty][tx] > dist[Coordinates.y][Coordinates.x] + 1)
                    {
                        dist[ty][tx] = dist[Coordinates.y][Coordinates.x] + 1;
                        q.Enqueue((ty, tx));
                    }
                    if(ty == finishY && tx == finishX)
                    {
                        flag = true;
                        break;
                    }

                }
                if (flag)
                    break;
            }



            //for (int i = 0;i < dist.Count;i++)
            //{
            //    for (int j = 0; j < dist[0].Count; j++)
            //    {
            //        Console.Write(dist[i][j]);
            //    }

            //    Console.WriteLine();

            //}




            var result = getPath(curGhost.xPosition,curGhost.yPosition,finishX,finishY,dist);

            return result;
        }

        static (int, int) getPath(int startX, int StartY, int FinishX, int FinishY, List<List<int>> dist)
        {
            var result = (FinishX, FinishY);
            int currDist = dist[FinishY][FinishX];
            while (currDist != 1)
            {
                if (currDist == 2)
                    return result;

                List<int> dy = new List<int> { -1, 0, 1, 0 };
                List<int> dx = new List<int>() { 0, 1, 0, -1 };


                for (int i = 0; i < dy.Count; i++)
                {
                    int ty = result.FinishY + dy[i];
                    int tx = result.FinishX + dx[i];
                    if (0 <= ty && ty < dist.Count && 0 <= tx && tx < dist[0].Count &&
                        dist[ty][tx] == (currDist - 1))
                    {
                        result.FinishX = tx;
                        result.FinishY = ty;
                        currDist--;
                        break;
                    }
                }

            }
                
            return result;

        }
    }
}

