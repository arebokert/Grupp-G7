﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgame
{
   abstract class Player
    {
      public  int colour;
      public Player()
        {
    
        }
        public void printColour()
        {
            Console.WriteLine(Colour);
           
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