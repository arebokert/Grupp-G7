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
            initialLoad();
            //updateBoardArray();
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
                gameRules.makeMove(extractValues(p.Name.First(), p.Name.Last()));
                updateBoardArray();
            }
            else
            {
                Console.WriteLine("Not your turn, please wait");
            }
            textBox1.Text = gameScore;
        }

        private void resetGame(object sender, EventArgs e)
        {
          //  gameLogic.initialLoad();
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

        private int[] extractValues(char f, char l)
        {
            //Index at 0 is row, index at 1 is column
            int[] clickedTile = new int[2];
            clickedTile[0] = (int)Char.GetNumericValue(l);
            clickedTile[0]--;
            switch (f)
            {
                case 'a':
                    clickedTile[1] = 0;
                    break;
                case 'b':
                    clickedTile[1] = 1;
                    break;
                case 'c':
                    clickedTile[1] = 2;
                    break;
                case 'd':
                    clickedTile[1] = 3;
                    break;
                case 'e':
                    clickedTile[1] = 4;
                    break;
                case 'f':
                    clickedTile[1] = 5;
                    break;
                case 'g':
                    clickedTile[1] = 6;
                    break;
                case 'h':
                    clickedTile[1] = 7;
                    break;
            }
            return clickedTile;
        }

        private void updateBoardArray()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.BoardArray[x, y] == board.GreenMarker)
                    {
                        board.BoardPictureBox[x, y].Tag.Equals("green");
                        board.BoardPictureBox[x, y].Image = board.Green;
                    }
                    else if (board.BoardArray[x, y] == board.WhiteMarker)
                    {
                        board.BoardPictureBox[x, y].Tag.Equals("white");
                        board.BoardPictureBox[x, y].Image = board.White;
                    }
                    else if (board.BoardArray[x, y] == board.BlackMarker)
                    {
                        board.BoardPictureBox[x, y].Tag.Equals("black");
                        board.BoardPictureBox[x, y].Image = board.Black;

                    }
                }
            }
        }

        public void initialLoad()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.BoardPictureBox[x, y].Name.Equals("d4") || board.BoardPictureBox[x, y].Name.Equals("e5"))
                    {
                        board.BoardPictureBox[x, y].Image = board.White;
                        board.BoardPictureBox[x, y].Tag = "white";
                        board.BoardArray[x,y] = 1;
                    }
                    else if (board.BoardPictureBox[x, y].Name.Equals("d5") || board.BoardPictureBox[x, y].Name.Equals("e4"))
                    {
                        board.BoardPictureBox[x, y].Image = board.Black;
                        board.BoardPictureBox[x, y].Tag = "black";
                        board.BoardArray[x, y] = 2;
                    }
                    else
                    {
                        board.BoardPictureBox[x, y].Image = board.Green;
                        board.BoardPictureBox[x, y].Tag = "green";
                        board.BoardArray[x, y] = 0;
                    }
                }
            }
            // updateBoardArray();
        }
        
    }
}
