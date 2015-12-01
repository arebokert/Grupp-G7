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
            PlayerBlack pb = new PlayerBlack();

            Map mp = new Map();
            pw.Colour = 1;
            pb.Colour = 2;

            for (int i = 0; i < 8; i++)
            {

                for (int j = 0; j < 8; j++)
                {
                    if ((j == 3 && i == 4) || (j == 4 && i == 3))
                    {
                        mp.Board[i, j] = pw.Colour;
                        Console.Write(mp.Board[i, j]);

                    }
                    else if ((j == 3 && i == 3) || (j == 4 && i == 4))
                    {
                        mp.Board[i, j] = pb.Colour;
                        Console.Write(mp.Board[i, j]);


                    }
                    else
                    {
                        mp.Board[i, j] = 0;
                        Console.Write(mp.Board[i, j]);
                    }

                }
                Console.WriteLine("");

            }
            Console.ReadLine();
        }






    }
}
  

