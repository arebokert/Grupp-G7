using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
   

 
        abstract class Player
        {
            public int colour;
            public Player()
            {

            }

            public int Colour
            {
                get
                {
                    return colour;
                }

                set
                {
                    colour = value;
                }
            }
        }
    }


