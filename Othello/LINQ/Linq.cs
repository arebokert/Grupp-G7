using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Othello.LINQ
{
    class Linq
    {
       public Linq()
        {
            XDocument doc = XDocument.Load("savedBoard.xml");
        }
        public void storeBoard(int[,] boardArray)
        {

        }
    }
}
