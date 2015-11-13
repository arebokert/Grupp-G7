using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgame
{
    class Map
    {

        private int[,] board = new int[8, 8];

        Map()
        {

        }

        public int[,] getBoard()
        {
            return this.board;
        }

        public void setBoard(int[,] board)
        {
            this.board = board;

        }
    }
}
