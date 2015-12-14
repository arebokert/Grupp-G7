using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class PlayerWhite : Player
    {
        private Image whiteMarker;
        public PlayerWhite()
        {
             WhiteMarker = Image.FromFile(@"..\\..\\Resources\\Images\\whiteMarker.png");
        }

        public Image WhiteMarker
        {
            get
            {
                return whiteMarker;
            }

            set
            {
                whiteMarker = value;
            }
        }
    }
}

