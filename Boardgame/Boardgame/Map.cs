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



        private void placePlayerInitial(int i,int j)
        {
            setBoard[3][3] = i;
            setBoard[4][4] = i;
            setBoard[3][4] = j;
            setBoard[4][3] = j;
        }
    }
}
