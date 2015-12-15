using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
   public class GameRules
    {
        private Boolean allowed;
        public GameRules()
        {
            Allowed = false;
        }

        public void checkIfAllowed()
        {
           
            if(checkDiagonal() && checkHorisontal() && checkVertical() == true)
            {
                Allowed = true;
            }
        }



        private Boolean checkDiagonal()
        {
            return true;
        }

        private Boolean checkHorisontal()
        {
            return true;
        }

        private Boolean checkVertical()
        {
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
    }
}
