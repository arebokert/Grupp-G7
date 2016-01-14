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
using Othello.Objects;

namespace Othello
{
    public partial class View : Form
    {
        string[] lines = System.IO.File.ReadAllLines(@"..\\..\\Resources\\mapInitial.txt");
        private PictureBox[,] boardPictureBox;
        Rectangle rect = new Rectangle(0,0,600,600);
        Rectangle small = new Rectangle(0, 0, 66, 66);
        GameRules gameRules;
        GameLogic gameLogic;
        Board board;
        private string gameScore;

        public View(GameRules gr, Board b,GameLogic gl)
        {
            boardPictureBox = new PictureBox[8, 8];
            board = b;
            gameRules = gr;
            gameLogic = gl;
            InitializeComponent();
            populatePicArray(Controls);
            board.BoardPictureBox = boardPictureBox;
            gameLogic.initialLoad();
            lines.Reverse();
            boardPictureBox = board.BoardPictureBox;
            textBox1.Text += gameLogic.GameScore;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), rect);
        }

        //Adds all PicturBoxes to the List boxes by matching Tags with content of the stringArray Lines
        private void populatePicArray(Control.ControlCollection controls)
        {
            for(int x =0; x<8; x++)
            {
                for(int y = 0; y<8; y++)
                {
                    boardPictureBox[x,y] = (PictureBox)Controls.Find(lines[y%8+x*8], true)[0];

                }
            }
        }

        private void tile_MouseClick(object sender, MouseEventArgs e)
        {
            if(gameLogic.PlayerTurnInt == board.WhiteMarker)
            {
                PictureBox p = (PictureBox)sender;
                gameRules.makeMove(p);
            }
            else
            {
                Console.WriteLine("Not your turn, please wait");
            }
            textBox1.Text = gameScore;
        }

        private void resetGame(object sender, EventArgs e)
        {
            gameLogic.initialLoad();
            restoreGame.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gameLogic.restoreSavedGame();
            restoreGame.Hide();
        }

        public void scoreChanged(String score)
        {
            textBox1.Text = score;
        }



    }
}
