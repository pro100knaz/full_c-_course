using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_PackMan
{
    public class Player
    {

        private int _xPosition;
        private int _yPosition;


        public int PreviuosXposition { get; set; }
        public int PreviuosYposition { get; set; }
        public char PreviousState { get; set; } = ' ';

        public int xPosition { get => _xPosition; set => _xPosition = value; }
        public int yPosition { get => _yPosition; set => _yPosition = value; }

        public Player(int x, int y)
        {
            PreviuosXposition = x;
            PreviuosYposition = y;

            _xPosition = x;
            _yPosition = y;
        }
        public Player()
        {
            
        }

    }
}
