using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Othello.Objects;

namespace Othello.Model
{
    public class AI
    {
        //   private GameRules gameRules;
        Board board;
        GameLogic gameLogic;
        private int[,] greenTiles;
        private List<int[]> coordinates;
        private List<int[,]> paintArrays;
        private int score;
        private int[,] boardArray;
        private int[] aiPressedTile;
        private PictureBox aiTile;
        private GameRules gameRules;
        private int[,] paintArrayTemp;
        private Timer timer1;
        private int xCoord;
        private int yCoord;

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

        public AI(GameRules gr,Board b,GameLogic g)
        {
            InitTimer();
            board = b;
            greenTiles = new int[8, 8];
            aiPressedTile = new int[2];
            paintArrayTemp = new int[8,8];
            paintArrays = new List<int[,]>();
            coordinates = new List<int[]>();
            score = 0;
            gameRules = gr;
            gameLogic = g;
        }

        public void aiTurn()
        {
            score = 0;
            getAllGreenTiles();
            gameLogic.resetPaintArray(gameLogic.PaintArray);
            gameRules.makeMove(aiTile);
        }

        public void getAllGreenTiles()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.BoardArray[x, y] == 0)
                    {
                        if (gameRules.checkAllDirections(x, y))
                        {
                            int[] tempCoords = new int[2];
                            tempCoords[0] = x;
                            tempCoords[1] = y;
                            coordinates.Add(tempCoords);
                            copyArray();
                            storePaintArray();
                        }
                    }
                }
            }
            compareScore();
        }
        
        private void copyArray()
        {
            for(int x = 0; x < 8; x++)
            {
                for(int y = 0; y<8; y++)
                {
                    paintArrayTemp[x, y] = gameLogic.PaintArrayTemp[x, y];
                }
            }
        }

        public void storePaintArray()
        {
            int[,] temp = new int[8, 8];
            for(int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    if(paintArrayTemp[x,y] == 1)
                    {
                        temp[x, y] = 1;
                    }
                    else
                    {
                        temp[x, y] = 0;
                    }
                }
            }
            paintArrays.Add(temp);

        }

        public void compareScore()
        {
            int tempScore = 0;
            int[,] tempArray = new int[8, 8];
            int[] tempCoordinates = new int[2];
            int tempX = 0;
            int tempY = 0;
            for (int i = 0; i < paintArrays.Count; i++)
            {
                tempCoordinates = coordinates[i];
                tempArray = paintArrays[i];
                for(int x = 0; x<8; x++)
                {
                    for(int y = 0; y < 8; y++)
                    {

                        if (tempArray[x,y] == 1)
                        {
                            tempScore++;
                        }
                    }
                }
                if (tempScore >= score)
                {
                    score = tempScore;
                    tempX = tempCoordinates[0];
                    tempY = tempCoordinates[1];
                }
            }
            if(score == 0)
            {

               // gameRules.Counter = gameRules.Counter + 1;
            }
            xCoord = tempX;
            yCoord = tempY;
            aiTile = board.BoardPictureBox[xCoord, yCoord];
        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1500;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gameLogic.PlayerTurnInt == board.BlackMarker)
          {
                aiTurn();
          }
        }
    }
}
