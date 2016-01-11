using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    abstract class Marker
    {
        private int colour;
        public Marker()
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


