using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Othello.Objects;
namespace Othello.Model
{
    public class GameRules
    {
        Board board;
        GameLogic gameLogic;
        public GameRules(GameLogic g, Board b)
        {
            board = b;
            gameLogic = g;
        }


        public void makeMove(int[] tileClicked)
        {
            checkAllDirections(tileClicked[0], tileClicked[1]);
            gameLogic.doLogic(tileClicked);

        }

        public Boolean checkAllDirections(int column, int row)
        {
            gameLogic.LegalMoveCounter = 0;
            gameLogic.changeTurn(gameLogic.Counter);
            checkMove(column, row, -1, 0, "left");
            checkMove(column, row, 1, 0, "right"); 
            checkMove(column, row, 0, -1, "up"); 
            checkMove(column, row, 0, 1, "down"); 
            checkMove(column, row, -1, -1, "upLeft"); 
            checkMove(column, row, 1, -1, "upRight");
            checkMove(column, row, 1, 1, "downRight");
            checkMove(column, row, -1, 1, "downLeft");

            if (gameLogic.LegalMoveCounter > 0)
            {
                return true;
            }
            return false;
        }
        private Boolean condition(string direction, int column, int row)
        {
            switch (direction)
            {
                case "up":
                    if (column > 0)
                    {
                        return true;
                    }
                    break;
                case "down":
                    if (column < 7)
                    {
                        return true;
                    }
                    break;

                case "left":
                    if (row > 0)
                    {
                        return true;
                    }
                    break;

                case "right":
                    if (row < 7)
                    {
                        return true;
                    }
                    break;

                case "upRight":
                    if (row < 7 && column > 0)
                    {
                        return true;
                    }
                    break;

                case "upLeft":
                    if (row > 0 && column > 0)
                    {
                        return true;
                    }
                    break;

                case "downLeft":
                    if (row > 0 && column < 7)
                    {
                        return true;
                    }
                    break;

                case "downRight":
                    if (row < 7 && column < 7)
                    {
                        return true;
                    }
                    break;
            }

            return false;
            //row > 0 && row < 7 && column > 0 && column < 7 && board.BoardArray[column, row]
        }
        private void checkMove(int column, int row, int rowDirection, int columnDirection, string direction)
        {
            int c = 0;
            if (board.BoardArray[column, row] == board.GreenMarker)
            {
                row += rowDirection;
                column += columnDirection;
                while (condition(direction, column, row) && board.BoardArray[column, row] == gameLogic.NotPlayerTurnInt)
                {
                    gameLogic.PaintArrayTemp[column, row] = 1;

                    if (board.BoardArray[column + columnDirection, row + rowDirection] == gameLogic.PlayerTurnInt)
                    {
                        c++;
                        if (c == 2)
                        {
                            break;
                        }
                        else
                        {
                            gameLogic.PaintArrayTemp[column + columnDirection, row + rowDirection] = 2;
                        }
                    }
                    row += rowDirection;
                    column += columnDirection;
                }
                gameLogic.legalMove(column, row);
            }
        }
    }
}
