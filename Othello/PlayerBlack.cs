﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class PlayerBlack : Player
    {
       private Image blackMarker;
        public PlayerBlack()
        {
            BlackMarker = Image.FromFile("C:\\Users\\tomtom\\Pictures\\C\\BlackMarker.png");
        }

        public Image BlackMarker
        {
            get
            {
                return blackMarker;
            }

            set
            {
                blackMarker = value;
            }
        }
    }
}