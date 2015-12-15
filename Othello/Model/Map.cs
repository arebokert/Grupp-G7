using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class Map
    {

        private Image green;
        private PlayerWhite pw;
        private PlayerBlack pb ;

        public Map(PlayerBlack p, PlayerWhite w)
        {
            green = Image.FromFile(@"..\\..\\Resources\\Images\\NoMarker.png");
            pb = p;
            pw = w; 
        }
        public Image Green
        {
            get
            {
                return green;
            }

            set
            {
                green = value;
            }
        }

    }
}