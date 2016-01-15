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
            checkLeft(column, row);
            checkRight(column, row);
            checkUp(column, row);
            checkDown(column, row);
            checkUpRight(column, row);
            checkUpLeft(column, row);
            checkDownRight(column, row);
            checkDownLeft(column, row);
            if (gameLogic.LegalMoveCounter > 0)
            {
                return true;
            }
            return false;
        }

        private void checkLeft(int column, int row)
        {
            int c = 0;
            if (board.BoardArray[column, row] == board.GreenMarker)
            {
                row--;
                {
                    while (row > 0 && board.BoardArray[column, row] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[column, row] = 1;
                        if (board.BoardArray[column, row - 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[column, row - 1] = 2;
                            }
                        }
                        row--;
                    }
                }
                gameLogic.legalMove(column, row);
            }
        }

        private void checkRight(int column, int row)
        {
            int c = 0;
            if (board.BoardArray[column, row] == board.GreenMarker)
            {
                row++;
                {
                    while (row < 7 && board.BoardArray[column, row] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[column, row] = 1;
                        if (board.BoardArray[column, row + 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[column, row + 1] = 2;
                            }
                        }
                        row++;
                    }
                }
                gameLogic.legalMove(column, row);
            }
        }

        private void checkUp(int column, int row)
        {
            int c = 0;
            if (board.BoardArray[column, row] == board.GreenMarker)
            {
                column--;
                {
                    while (column > 0 && board.BoardArray[column, row] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[column, row] = 1;
                        if (board.BoardArray[column - 1, row] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[column - 1, row] = 2;
                            }
                        }
                        column--;
                    }
                }
                gameLogic.legalMove(column, row);
            }
        }

        private void checkDown(int column, int row)
        {
            int c = 0;
            if (board.BoardArray[column, row] == board.GreenMarker)
            {
                column++;
                {
                    while (column < 7 && board.BoardArray[column, row] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[column, row] = 1;
                        if (board.BoardArray[column + 1, row] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[column + 1, row] = 2;
                            }
                        }
                        column++;
                    }
                }
                gameLogic.legalMove(column, row);
            }
        }

        private void checkUpRight(int column, int row)
        {
            int c = 0;
            if (board.BoardArray[column, row] == board.GreenMarker)
            {
                row++;
                column--;
                {
                    while (column > 0 && row < 7 && board.BoardArray[column, row] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[column, row] = 1;
                        if (board.BoardArray[column - 1, row + 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[column - 1, row + 1] = 2;
                            }
                        }
                        row++;
                        column--;
                    }
                }
                gameLogic.legalMove(column, row);

            }
        }

        private void checkUpLeft(int column, int row)
        {
            int c = 0;
            if (board.BoardArray[column, row] == board.GreenMarker)
            {
                row--;
                column--;
                {
                    while (column > 0 && row > 0 && board.BoardArray[column, row] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[column, row] = 1;
                        if (board.BoardArray[column - 1, row - 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[column - 1, row - 1] = 2;
                            }
                        }
                        row--;
                        column--;
                    }
                }
                gameLogic.legalMove(column, row);
            }
        }

        private void checkDownRight(int column, int row)
        {
            int c = 0;
            if (board.BoardArray[column, row] == board.GreenMarker)
            {
                row++;
                column++;
                {
                    while (column < 7 && row < 7 && board.BoardArray[column, row] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[column, row] = 1;
                        if (board.BoardArray[column + 1, row + 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[column + 1, row + 1] = 2;
                            }
                        }
                        row++;
                        column++;
                    }
                }
                gameLogic.legalMove(column, row);
            }
        }

        private void checkDownLeft(int column, int row)
        {
            int c = 0;
            if (board.BoardArray[column, row] == 0)
            {
                row--;
                column++;
                {
                    while (column < 7 && row > 0 && board.BoardArray[column, row] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[column, row] = 1;
                        if (board.BoardArray[column + 1, row - 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[column + 1, row - 1] = 2;
                            }
                        }
                        row--;
                        column++;
                    }
                }
                gameLogic.legalMove(column, row);
            }
        }
    }
}
