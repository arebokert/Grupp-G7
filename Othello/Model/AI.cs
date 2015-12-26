using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello.Model
{
    public class AI
    {
        //   private GameRules gameRules;
        private int[,] greenTiles;

        private List<int> compareList;

        private int score;
        private int[,] boardArray;
        private int[] aiPressedTile;
        private PictureBox aiTile;
        private GameRules gameRules;


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

        public AI(GameRules g)
        {
            greenTiles = new int[8, 8];
            aiPressedTile = new int[2];
            score = 0;
            gameRules = g;
        }


        public void aiTurn()
        {
            getAllGreenTiles();
            compare();
            gameRules.makeMove(aiTile);
        }

        public void getAllGreenTiles()
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (gameRules.BoardArray[x, y] == 0)
                    {
                        gameRules.checkAllDirections(x, y);
                        compareList.Add(x);
                        compareList.Add(y);
                        compareList.Add(gameRules.MoveScore);
                    }
                }
            }
        }

        private void compare()
        {
            for (int i = 2; i < compareList.Count - 3; i += 3)
            {
                if (score <= compareList.ElementAt(i))
                {
                    score = compareList.ElementAt(i);
                    aiPressedTile[0] = compareList.ElementAt(i - 2);
                    aiPressedTile[1] = compareList.ElementAt(i - 1);
                }
            }
            aiTile = gameRules.Board[aiPressedTile[0], aiPressedTile[1]];
        }
        

    }
}
