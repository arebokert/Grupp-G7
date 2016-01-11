using Othello.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello
{
    public partial class View : Form
    {
        private GameRules gameRule;
        string[] lines = System.IO.File.ReadAllLines(@"..\\..\\Resources\\mapInitial.txt");
        private PictureBox[,] board = new PictureBox[8, 8];
        Rectangle rect = new Rectangle(0,0,600,600);
        Rectangle small = new Rectangle(0, 0, 66, 66);
        GameRules gameRules;

        public View(GameRules g)
        { 
            InitializeComponent();
            populatePicArray(Controls);
            gameRules = g;
            gameRules.Board = board;
            gameRules.initialLoad();
            lines.Reverse();
            board = gameRules.Board;

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), rect);
        }

 
        internal GameRules GameRule
        {
            get
            {
                return gameRule;
            }

            set
            {
                gameRule = value;
            }
        }

        private void a3_Click(object sender, EventArgs e)
        {

        }

        private void a7_Click(object sender, EventArgs e)
        {

        }

        private void h8_Click(object sender, EventArgs e)
        {

        }

        private void g1_Click(object sender, EventArgs e)
        {

        }

        //Adds all PicturBoxes to the List boxes by matching Tags with content of the stringArray Lines
        private void populatePicArray(Control.ControlCollection controls)
        {
          
            for(int x =0; x<8; x++)
            {
                for(int y = 0; y<8; y++)
                {
                    board[x,y] = (PictureBox)Controls.Find(lines[y%8+x*8], true)[0];

                }
            }
        }

        private void tile_MouseClick(object sender, MouseEventArgs e)
        {
            if(gameRules.PlayerTurnInt == gameRules.WhiteMarker)
            {
                PictureBox p = (PictureBox)sender;
                gameRules.makeMove(p);
            }
            else
            {
                Console.WriteLine("Not your turn, please wait");
            }

        }

        private void resetGame(object sender, EventArgs e)
        {
            gameRules.initialLoad();
        }
    }
}
