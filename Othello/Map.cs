using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class Map
    {

        private Image green;
        private int[,] board = new int[8, 8];
        private PlayerWhite pw;
        private PlayerBlack pb ;

        public Map(PlayerBlack p, PlayerWhite w)
        {
            green = Image.FromFile(@"..\\..\\Resources\\Images\\NoMarker.png");
            pb = p;
            pw = w;
            initializeBoard();
        }

        public void initializeBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((j == 3 && i == 4) || (j == 4 && i == 3))
                    {
                        board[i, j] = pw.Colour;    
                    }
                    else if ((j == 3 && i == 3) || (j == 4 && i == 4))
                    {
                        board[i, j] = pb.Colour;                   
                    }
                    else
                    {
                        board[i, j] = 0;             
                    }
                }
            }
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

        public Image Green
        {
            get
            {
                return green;
            }

            set
            {
                green = value;
            }
        }

    }
}