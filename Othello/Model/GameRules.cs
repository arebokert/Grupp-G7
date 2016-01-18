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

        public void playerTurnChanged(int turn)
        {
            if (checkIfAnyAvailableMove())
            {
                gameLogic.GameOverCheck[gameLogic.PlayerTurnInt - 1] = false;
                gameLogic.GameOverCheck[gameLogic.NotPlayerTurnInt - 1] = false;
            }
            else
            {
                gameLogic.GameOverCheck[gameLogic.PlayerTurnInt - 1] = true;
                if (gameLogic.GameOverCheck[gameLogic.PlayerTurnInt - 1] && gameLogic.GameOverCheck[gameLogic.NotPlayerTurnInt - 1])
                {
                    gameLogic.gameIsOver();
                }
                else
                {
                    gameLogic.Counter++;
                    gameLogic.changeTurn(gameLogic.Counter);
                }
            }
        }

        public void makeMove(int[] tileClicked)
        {
            checkAllDirections(tileClicked[0], tileClicked[1]);
            gameLogic.doLogic(tileClicked);
        }

        public Boolean checkAllDirections(int row, int column)
        {
            gameLogic.Switcher++;
            gameLogic.LegalMoveCounter = 0;
            gameLogic.changeTurn(gameLogic.Counter);
            checkMove(row, column, -1, 0, "left");
            checkMove(row, column, 1, 0, "right");
            checkMove(row, column, 0, -1, "up");
            checkMove(row, column, 0, 1, "down");
            checkMove(row, column, -1, -1, "upLeft");
            checkMove(row, column, 1, -1, "upRight");
            checkMove(row, column, 1, 1, "downRight");
            checkMove(row, column, -1, 1, "downLeft");
            if (gameLogic.LegalMoveCounter > 0)
            {
                return true;
            }
            return false;
        }
        private Boolean condition(string direction, int row, int column)
        {
            switch (direction)
            {
                case "up":
                    if (row > 0)
                    {
                        return true;
                    }
                    break;
                case "down":
                    if (row < 7)
                    {
                        return true;
                    }
                    break;

                case "left":
                    if (column > 0)
                    {
                        return true;
                    }
                    break;

                case "right":
                    if (column < 7)
                    {
                        return true;
                    }
                    break;

                case "upRight":
                    if (column < 7 && row > 0)
                    {
                        return true;
                    }
                    break;

                case "upLeft":
                    if (column > 0 && row > 0)
                    {
                        return true;
                    }
                    break;

                case "downLeft":
                    if (column > 0 && row < 7)
                    {
                        return true;
                    }
                    break;

                case "downRight":
                    if (column < 7 && row < 7)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private void checkMove(int row, int column, int columnDirection, int rowDirection, string direction)
        {
            int c = 0;
            if (board.BoardArray[row, column] == board.GreenMarker)
            {
                column += columnDirection;
                row += rowDirection;
                while (condition(direction, row, column) && board.BoardArray[row, column] == gameLogic.NotPlayerTurnInt)
                {
                    gameLogic.CurrentCheckTilesToChange[row, column] = 1;

                    if (board.BoardArray[row + rowDirection, column + columnDirection] == gameLogic.PlayerTurnInt)
                    {
                        c++;
                        if (c == 2)
                        {
                            break;
                        }
                        else
                        {
                            gameLogic.CurrentCheckTilesToChange[row + rowDirection, column + columnDirection] = 2;
                        }
                    }
                    column += columnDirection;
                    row += rowDirection;
                }
                gameLogic.legalMove(row, column);
            }
        }

        private Boolean checkIfAnyAvailableMove()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (board.BoardArray[row, column] == 0)
                    {
                        if (checkAllDirections(row, column))
                        {
                            gameLogic.CurrentCheckTilesToChange = new int[8, 8];
                            gameLogic.CurrentRoundTilesToChange = new int[8, 8];
                            return true;
                        }
                    }
                }
            }
            gameLogic.CurrentCheckTilesToChange = new int[8, 8];
            gameLogic.CurrentRoundTilesToChange = new int[8, 8];
            return false;
        }
    }
}
