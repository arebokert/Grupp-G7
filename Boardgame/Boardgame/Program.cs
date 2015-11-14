using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgame
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerWhite pw = new PlayerWhite();
            PlayerBlack bp = new PlayerBlack();
          //
            pw.Colour = 1;
            bp.Colour = 2;
            pw.printColour();
            bp.printColour();
            Console.ReadLine();
            
        }
    }
}
