using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello.Model
{
    public class GameRules
    {
        private Boolean allowed;
        private int letterValue;
        private int tileMultiple;
        private int tileValue;
        private int previousTileValue;
        private List<PictureBox> changePictureBoxColor = new List<PictureBox>();
      
        public GameRules()
        {

            Allowed = false;
        }



        public void checkIfAllowed(List<PictureBox> boxes, PictureBox p)
        {
            extractValues(p.Name.First(), p.Name.Last());
            checkHorisontal(boxes, p);
            

            
            if (checkVertical(boxes,p))
            {

            }
            if (checkDiagonal(boxes,p))
            {

            }

        }

        private Boolean checkDiagonal(List<PictureBox> boxes, PictureBox p)
        {

            //(8*y)-(8-x)-10 LEFT UP
            //(8*y)-(8-x)-6 RIGHT UP
            //(8*y)-(8-x)+10 RIGHT DOWN
            //(8*y)-(8-x)+6 LEFT DOWN

            return true;
        }

        private void extractValues(char f, char l)
        {
           tileMultiple = (int)Char.GetNumericValue(l);
            switch (f)
            {
                case 'a':
                    letterValue = 1;
                    break;
                case 'b':
                    letterValue = 2;
                    break;
                case 'c':
                    letterValue = 3;
                    break;
                case 'd':
                    letterValue = 4;
                    break;
                case 'e':
                    letterValue = 5;
                    break;
                case 'f':
                    letterValue = 6;
                    break;
                case 'g':
                    letterValue = 7;
                    break;
                case 'h':
                    letterValue = 8;
                    break;
            }

            tileValue = (8 * tileMultiple) - (8 - letterValue);
            previousTileValue = tileValue - 1;
        }

        private Boolean checkHorisontal(List<PictureBox> boxes, PictureBox p)
        {

           List<PictureBox> temp = new List<PictureBox>();
           
            while(boxes.ElementAt(tileValue).Tag.Equals("green")&& (tileValue > previousTileValue - letterValue)){
                tileValue = tileValue - 1;
                temp.Add(boxes.ElementAt(tileValue));

            }
            if (boxes.ElementAt(tileValue).Tag.Equals("white"))
            {
                ChangePictureBoxColor = temp;
            }
              

            
                //(8*y)-(8-x)-2 LEFT
          
                //(8*y)-(8-x) RIGHT
           
                return false;
            

        }

        private Boolean checkVertical(List<PictureBox> boxes, PictureBox p)
        {

            //(8*y)-(8-x)-8 UP

            //(8*y)-(8-x)+8 DOWN
            return true;

        }






        public bool Allowed
        {
            get
            {
                return allowed;
            }

            set
            {
                allowed = value;
            }
        }

        public List<PictureBox> ChangePictureBoxColor
        {
            get
            {
                return changePictureBoxColor;
            }

            set
            {
                changePictureBoxColor = value;
            }
        }
    }
}
