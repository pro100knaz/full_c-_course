using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace full_c__course
{
    public class Ghost : Player
    {

        ConsoleColor _color;

        public ConsoleColor Color { get => _color; set => _color = value; }

        public Ghost(int x, int y, ConsoleColor c) : base(x, y)
        {
            _color = c;
        }

        public void GhostDFS(Player player, Ghost ghost, ref List<List<char>> map)
        {
            if (player.xPosition == ghost.xPosition && player.yPosition == ghost.yPosition)
            {
               ////

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
                    GhostDFS(player, ghost, ref map);
                }

            }

        }
    }
}
