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
        public GameRules(GameLogic g,Board b)
        {
            board = b;
            gameLogic = g;
        }


        public void makeMove(PictureBox p)
        {
            Console.WriteLine(gameLogic.PlayerTurn);
            if (!p.Name.Equals(""))
            {
                gameLogic.extractValues(p.Name.First(), p.Name.Last());
                checkAllDirections(gameLogic.TileValueX, gameLogic.TileValueY);
                gameLogic.doLogic();
            }
        }
        public Boolean checkAllDirections(int tileValueX, int tileValueY)
        {
            gameLogic.LegalMoveCounter = 0;
            gameLogic.changeTurn(gameLogic.Counter);
            gameLogic.MoveScore = 0;
            checkLeft(tileValueX, tileValueY);
            checkRight(tileValueX, tileValueY);
            checkUp(tileValueX, tileValueY);
            checkDown(tileValueX, tileValueY);
            checkUpRight(tileValueX, tileValueY);
            checkUpLeft(tileValueX, tileValueY);
            checkDownRight(tileValueX, tileValueY);
            checkDownLeft(tileValueX, tileValueY);
            gameLogic.countScore();
            if (gameLogic.LegalMoveCounter > 0)
            {
                return true;
            }
            return false;
        }

        private void checkLeft(int xValue, int yValue)
        {
            int c = 0;
            if (board.BoardArray[xValue, yValue] == board.GreenMarker)
            {
                yValue--;
                {
                    while (yValue > 0 && board.BoardArray[xValue, yValue] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[xValue, yValue] = 1;
                        if (board.BoardArray[xValue, yValue - 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[xValue, yValue - 1] = 2;
                            }
                        }
                        yValue--;
                    }
                }
                gameLogic.legalMove(xValue, yValue);
            }
        }

        private void checkRight(int xValue, int yValue)
        {
            int c = 0;
            if (board.BoardArray[xValue, yValue] == board.GreenMarker)
            {
                yValue++;
                {
                    while (yValue < 7 && board.BoardArray[xValue, yValue] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[xValue, yValue] = 1;
                        if (board.BoardArray[xValue, yValue + 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[xValue, yValue + 1] = 2;
                            }
                        }
                        yValue++;
                    }
                }
                gameLogic.legalMove(xValue, yValue);
            }
        }

        private void checkUp(int xValue, int yValue)
        {
            int c = 0;
            if (board.BoardArray[xValue, yValue] == board.GreenMarker)
            {
                xValue--;
                {
                    while (xValue > 0 && board.BoardArray[xValue, yValue] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[xValue, yValue] = 1;
                        if (board.BoardArray[xValue - 1, yValue] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[xValue - 1, yValue] = 2;
                            }
                        }
                        xValue--;
                    }
                }
                gameLogic.legalMove(xValue, yValue);
            }
        }

        private void checkDown(int xValue, int yValue)
        {
            int c = 0;
            if (board.BoardArray[xValue, yValue] == board.GreenMarker)
            {
                xValue++;
                {
                    while (xValue < 7 && board.BoardArray[xValue, yValue] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[xValue, yValue] = 1;
                        if (board.BoardArray[xValue + 1, yValue] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[xValue + 1, yValue] = 2;
                            }
                        }
                        xValue++;
                    }
                }
                gameLogic.legalMove(xValue, yValue);
            }
        }

        private void checkUpRight(int xValue, int yValue)
        {
            int c = 0;
            if (board.BoardArray[xValue, yValue] == board.GreenMarker)
            {
                yValue++;
                xValue--;
                {
                    while (xValue > 0 && yValue < 7 && board.BoardArray[xValue, yValue] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[xValue, yValue] = 1;
                        if (board.BoardArray[xValue - 1, yValue + 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[xValue - 1, yValue + 1] = 2;
                            }
                        }
                        yValue++;
                        xValue--;
                    }
                }
                gameLogic.legalMove(xValue, yValue);

            }
        }

        private void checkUpLeft(int xValue, int yValue)
        {
            int c = 0;
            if (board.BoardArray[xValue, yValue] == board.GreenMarker)
            {
                yValue--;
                xValue--;
                {
                    while (xValue > 0 && yValue > 0 && board.BoardArray[xValue, yValue] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[xValue, yValue] = 1;
                        if (board.BoardArray[xValue - 1, yValue - 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[xValue - 1, yValue - 1] = 2;
                            }
                        }
                        yValue--;
                        xValue--;
                    }
                }
                gameLogic.legalMove(xValue, yValue);
            }
        }

        private void checkDownRight(int xValue, int yValue)
        {
            int c = 0;
            if (board.BoardArray[xValue, yValue] == board.GreenMarker)
            {
                yValue++;
                xValue++;
                {
                    while (xValue < 7 && yValue < 7 && board.BoardArray[xValue, yValue] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[xValue, yValue] = 1;
                        if (board.BoardArray[xValue + 1, yValue + 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[xValue + 1, yValue + 1] = 2;
                            }
                        }
                        yValue++;
                        xValue++;
                    }
                }
                gameLogic.legalMove(xValue, yValue);
            }
        }

        private void checkDownLeft(int xValue, int yValue)
        {
            int c = 0;
            if (board.BoardArray[xValue, yValue] == 0)
            {
                yValue--;
                xValue++;
                {
                    while (xValue < 7 && yValue > 0 && board.BoardArray[xValue, yValue] == gameLogic.NotPlayerTurnInt)
                    {
                        gameLogic.PaintArrayTemp[xValue, yValue] = 1;
                        if (board.BoardArray[xValue + 1, yValue - 1] == gameLogic.PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                gameLogic.PaintArrayTemp[xValue + 1, yValue - 1] = 2;
                            }
                        }
                        yValue--;
                        xValue++;
                    }
                }
                gameLogic.legalMove(xValue, yValue);
            }
        }
    }
}
