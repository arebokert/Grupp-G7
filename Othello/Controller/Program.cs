using Othello.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            GameRules g = new GameRules();
            View v = new View(g);
            PlayerWhite pw = new PlayerWhite();
            PlayerBlack pb = new PlayerBlack();
            Map mp = new Map(pb,pw);
            pw.Colour = 1;
            pb.Colour = 2;
            v.paint(mp.Green,pw.WhiteMarker, pb.BlackMarker);
            
    
            Application.Run(v);
        }
    }
}
