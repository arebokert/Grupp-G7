using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello.Objects
{
    public class Board
    {
        private int blackMarker;
        private int whiteMarker;
        private int greenMarker;
        private Image green;
        private Image white;
        private Image black;
        private PictureBox[,] boardPictureBox;
        private int[,] boardArray;
        public Board()
        {
            Green = Image.FromFile(@"..\\..\\Resources\\Images\\NoMarker.png");
            White = Image.FromFile(@"..\\..\\Resources\\Images\\whiteMarker.png");
            Black = Image.FromFile(@"..\\..\\Resources\\Images\\BlackMarker.png");
            BlackMarker = 2;
            WhiteMarker = 1;
            GreenMarker = 0;
            BoardPictureBox = new PictureBox[8, 8];
            BoardArray = new int[8, 8];
        }

        public PictureBox[,] BoardPictureBox
        {
            get
            {
                return boardPictureBox;
            }

            set
            {
                boardPictureBox = value;
            }
        }

        public int[,] BoardArray
        {
            get
            {
                return boardArray;
            }

            set
            {
                boardArray = value;
            }
        }

        public int GreenMarker
        {
            get
            {
                return greenMarker;
            }

            set
            {
                greenMarker = value;
            }
        }

        public int WhiteMarker
        {
            get
            {
                return whiteMarker;
            }

            set
            {
                whiteMarker = value;
            }
        }

        public int BlackMarker
        {
            get
            {
                return blackMarker;
            }

            set
            {
                blackMarker = value;
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

        public Image White
        {
            get
            {
                return white;
            }

            set
            {
                white = value;
            }
        }

        public Image Black
        {
            get
            {
                return black;
            }

            set
            {
                black = value;
            }
        }
    }
}
