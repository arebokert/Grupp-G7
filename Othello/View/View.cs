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
        private int yourTurn;
        private string currentScore;
        Rectangle rect = new Rectangle(0,0,600,600);
        Rectangle small = new Rectangle(0, 0, 66, 66);
        GameRules gameRules;
        GameLogic gameLogic;
        Board board;

        public View(GameRules gr, Board b,GameLogic gl)
        {
            boardPictureBox = new PictureBox[8, 8];
            board = b;
            gameRules = gr;
            gameLogic = gl;
            InitializeComponent();
        }

        private void newGame()
        {
            populatePicArray(Controls);
            initialLoad();
            lines.Reverse();
        }
        public void boardArrayChanged(int[,] newArray)
        {
            board.BoardArray = newArray;
            updateBoardArray();
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
            if(gameLogic.PlayerTurnInt == yourTurn)
            {
               
                PictureBox p = (PictureBox)sender;
                gameRules.makeMove(extractValues(p.Name.First(), p.Name.Last()));
                updateBoardArray();
            }
            else
            {
                Console.WriteLine("Not your turn, please wait");
            }
        }

        private void resetGame(object sender, EventArgs e)
        {
            initialLoad();
            gameLogic.Counter = 0;
            gameLogic.changeTurn(gameLogic.Counter);
            restoreGame.Show();
            currentScore = gameLogic.currentScore();
            score.Text = currentScore;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gameLogic.restoreSavedGame();
            updateBoardArray();
            restoreGame.Hide();
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
                        Image green = Image.FromFile(@"..\\..\\Resources\\Images\\NoMarker.png");
                        boardPictureBox[x, y].Image = green;
                    }
                    else if (board.BoardArray[x, y] == board.WhiteMarker)
                    {
                        Image white = Image.FromFile(@"..\\..\\Resources\\Images\\whiteMarker.png");
                        boardPictureBox[x, y].Image = white;
                    }
                    else if (board.BoardArray[x, y] == board.BlackMarker)
                    {
                        Image black = Image.FromFile(@"..\\..\\Resources\\Images\\BlackMarker.png");
                        boardPictureBox[x, y].Image = black;
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
                    if (boardPictureBox[x, y].Name.Equals("d4") || boardPictureBox[x, y].Name.Equals("e5"))
                    {
                        boardPictureBox[x, y].Image = board.White;
                        boardPictureBox[x, y].Tag = "white";
                        board.BoardArray[x,y] = 1;
                    }
                    else if (boardPictureBox[x, y].Name.Equals("d5") || boardPictureBox[x, y].Name.Equals("e4"))
                    {
                        boardPictureBox[x, y].Image = board.Black;
                        boardPictureBox[x, y].Tag = "black";
                        board.BoardArray[x, y] = 2;
                    }
                    else
                    {
                        boardPictureBox[x, y].Image = board.Green;
                        boardPictureBox[x, y].Tag = "green";
                        board.BoardArray[x, y] = 0;
                    }
                }
            }
        }

        private void WhiteButton_Click(object sender, EventArgs e)
        {
            yourTurn = board.WhiteMarker;
            gameLogic.YourTurn = board.BlackMarker;
            buttonPanel.Hide();
            newGame();
        }

        private void BlackButton_Click(object sender, EventArgs e)
        {
            yourTurn = board.BlackMarker;
            gameLogic.YourTurn = board.WhiteMarker;
            buttonPanel.Hide();
            newGame();
        }

        private void updateScore_Click(object sender, EventArgs e)
        {
            currentScore = gameLogic.currentScore();
            score.Text = currentScore;
        }
    }
}
