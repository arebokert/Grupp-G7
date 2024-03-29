﻿using Othello.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Othello.Objects;
using Othello.Linq;

namespace Othello
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Board b = new Board();
            SaveBoard s = new SaveBoard();
            GameLogic g = new GameLogic(b, s);            
            GameRules gr = new GameRules(g, b);
            View v = new View(gr, b, g);
            AI ai = new AI(gr, b,g);

            g.onSwitcherChange += ai.switcherChanged;
            s.onBoardChange += g.boardArrayChanged;
            s.onBoardChange += v.boardArrayChanged;
            g.onScoreChange += v.scoreChanged;
            g.onGameOverChange += v.gameOverChanged;
            g.onTurnChange += gr.playerTurnChanged;
            g.onTurnChange += v.turnChanged;

            Application.EnableVisualStyles();
            Application.Run(v);
        }
    }
}
