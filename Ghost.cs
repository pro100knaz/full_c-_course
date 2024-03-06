using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_PackMan
{
    public class Ghost : Player
    {

        ConsoleColor _color;
        
        public ConsoleColor Color { get => _color; set => _color = value; }

        public Ghost() { PreviousState = '.'; }
        public Ghost(int x, int y, ConsoleColor c) : base(x, y)
        {
            PreviousState = '.';
            _color = c;
        }

    }
}
