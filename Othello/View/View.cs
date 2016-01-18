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
using System.Threading;

namespace Othello
{
    public partial class View : Form
    {
        Image green = Image.FromFile(@"Images\\NoMarker.png");
        Image white = Image.FromFile(@"Images\\whiteMarker.png");
        Image black = Image.FromFile(@"Images\\BlackMarker.png");
        string[] lines = System.IO.File.ReadAllLines(@"mapInitial.txt");
        private PictureBox[,] boardPictureBox;
        private int yourTurn;
        private string currentScore;
        Rectangle rect = new Rectangle(0, 0, 600, 600);
        Rectangle small = new Rectangle(0, 0, 66, 66);
        GameRules gameRules;
        GameLogic gameLogic;
        Board board;

        public View(GameRules gr, Board b, GameLogic gl)
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
            gameLogic.PlayerTurnInt = 1;
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
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    boardPictureBox[x, y] = (PictureBox)Controls.Find(lines[y % 8 + x * 8], true)[0];

                }
            }
        }

        private void tile_MouseClick(object sender, MouseEventArgs e)
        {
            if (gameLogic.PlayerTurnInt == yourTurn)
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

        private void resetButton(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {

            initialLoad();
            gameLogic.Counter = 0;
            gameLogic.changeTurn(gameLogic.Counter);
            restoreGame.Show();
            currentScore = gameLogic.calculateCurrentScore();
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
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (board.BoardArray[row, column] == board.GreenMarker)
                    {
                        Mutex mutexSave = new Mutex(false, "Images/NoMarker.png");
                        try
                        {
                            mutexSave.WaitOne();
                            boardPictureBox[row, column].Image = Image.FromFile(@"Images\\NoMarker.png");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Picture white already in use.");
                        }
                        finally
                        {
                            mutexSave.ReleaseMutex();
                        }
                    }
                   else if (board.BoardArray[row, column] == board.WhiteMarker)
                    {
                        Mutex mutexSave = new Mutex(false, "Images/whiteMarker.png");
                        try
                        {
                            mutexSave.WaitOne();
                            boardPictureBox[row, column].Image = Image.FromFile(@"Images\\whiteMarker.png");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Picture white already in use.");
                        }
                        finally
                        {
                            mutexSave.ReleaseMutex();
                        }
                    }
                    else if (board.BoardArray[row, column] == board.BlackMarker)
                    {
                        Mutex mutexSave = new Mutex(false, "Images/BlackMarker.png");
                        try
                        {
                            mutexSave.WaitOne();
                            boardPictureBox[row, column].Image = Image.FromFile(@"Images\\BlackMarker.png");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Picture black already in use.");
                        }
                        finally
                        {
                            mutexSave.ReleaseMutex();
                        }
                    }
                }
            }
        }

        public void initialLoad()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (boardPictureBox[row, column].Name.Equals("d4") || boardPictureBox[row, column].Name.Equals("e5"))
                    {
                        boardPictureBox[row, column].Image = white;
                        boardPictureBox[row, column].Tag = "white";
                        board.BoardArray[row, column] = board.WhiteMarker;
                    }
                    else if (boardPictureBox[row, column].Name.Equals("d5") || boardPictureBox[row, column].Name.Equals("e4"))
                    {
                        boardPictureBox[row, column].Image = black;
                        boardPictureBox[row, column].Tag = "black";
                        board.BoardArray[row, column] = board.BlackMarker;
                    }
                    else
                    {
                        boardPictureBox[row, column].Image = green;
                        boardPictureBox[row, column].Tag = "green";
                        board.BoardArray[row, column] = board.GreenMarker;
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

        public void scoreChanged(string newScore)
        {
            score.Text = newScore;
        }

        public void turnChanged(int turn)
        {
            label1.Text =  "Player "+gameLogic.PlayerTurn+"'s turn";
        }

        public void gameOverChanged(Boolean gameOver)
        {
            if (gameOver)
            {
                MessageBox.Show(gameLogic.winner(), "Game Over!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                reset();
                newGame();
            }
        }

        private void pokeOpponent(object sender, EventArgs e)
        {
            gameLogic.Switcher++;
        }
    }
}
