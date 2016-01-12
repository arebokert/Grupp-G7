using Othello.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Othello.Objects;

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
            GameLogic g = new GameLogic(b);
            GameRules gr = new GameRules(g, b);
            View v = new View(gr, b, g);
            AI ai = new AI(gr, b,g);

            Application.Run(v);
        }
    }
}
