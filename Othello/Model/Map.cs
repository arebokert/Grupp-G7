using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello
{
    class Map
    {

        private Image green;
        private Image white;
        private Image black;
        
        private List<PictureBox> board = new List<PictureBox>();

        public Map(PlayerBlack p, PlayerWhite w)
        {
            green = Image.FromFile(@"..\\..\\Resources\\Images\\NoMarker.png");
            White = Image.FromFile(@"..\\..\\Resources\\Images\\whiteMarker.png");
            Black = Image.FromFile(@"..\\..\\Resources\\Images\\BlackMarker.png");
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

        public Image Black
        {
            get
            {
                return black;
            }

            set
            {
                black = value;
            }
        }

        public Image White
        {
            get
            {
                return white;
            }

            set
            {
                white = value;
            }
        }
    }
}