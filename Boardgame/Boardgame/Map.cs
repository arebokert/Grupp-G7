using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgame
{
    class Map
    {

        public int[,] board  = new int[8,8];

        public   Map()
        {

        }

        public int[,] Board
        {
            get
            {
                return board;
            }

            set
            {
                board = value;
            }
        }

        public void placePlayerInitial(int i, int j)
        {
           
            
        }
    }
}