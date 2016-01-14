using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Othello.Linq
{
    class SaveBoard
    {
        private XDocument doc;
        private int counter;

        public int Counter
        {
            get
            {
                return counter;
            }

            set
            {
                counter = value;
            }
        }
        public SaveBoard()
        {
            doc = XDocument.Load(@Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Linq\\savedBoard.xml");

        }

        public void storeBoard(int[,] boardArray, int counter)
        {
            doc.Element("board").RemoveAll();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (boardArray[x, y] == 1)
                    {
                        doc.Element("board").Add(new XElement("white", new XAttribute("xCoord", x), new XAttribute("yCoord", y)));
                    }
                    else if (boardArray[x, y] == 2)
                    {
                        doc.Element("board").Add(new XElement("black", new XAttribute("xCoord", x), new XAttribute("yCoord", y)));
                    }
                }
            }

            doc.Element("board").Add(new XElement("counter", new XAttribute("counter", counter)));
            doc.Save(@Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Linq\\savedBoard.xml");
        }

        public int[,] restoreSavedGame()
        {
            int[,] savedBoard = new int[8, 8];

            IEnumerable<XElement> blacks = from el in doc.Elements("black") select el;
            IEnumerable<XElement> whites = from el in doc.Elements("white") select el;

            foreach(XElement el in blacks)
            {
                int xCoord = Int32.Parse(el.Attribute("xCoord").Value);
                int yCoord = Int32.Parse(el.Attribute("yCoord").Value);
                savedBoard[xCoord, yCoord] = 2;
            }

            foreach (XElement el in whites)
            {
                int xCoord = Int32.Parse(el.Attribute("xCoord").Value);
                int yCoord = Int32.Parse(el.Attribute("yCoord").Value);
                savedBoard[xCoord, yCoord] = 1;
            }

            counter = Int32.Parse(doc.Element("counter").Value);

            /*
            foreach (XElement element in doc.Elements("board").DescendantNodes())
            {
                listOfXElements.Add(element);
            }
            

            for (int i = 0; i < listOfXElements.Count; i++)
            {      
                if ((listOfXElements[i].Name) == "white")
                {
                    savedBoard[Int32.Parse(listOfXElements[i].FirstAttribute.Value), Int32.Parse(listOfXElements[i].LastAttribute.Value)] = 1;
                }
                else if ((listOfXElements[i].Name) == "black")
                {
                    savedBoard[Int32.Parse(listOfXElements[i].FirstAttribute.Value), Int32.Parse(listOfXElements[i].LastAttribute.Value)] = 2;
                }
                else if(listOfXElements[i].Name == "counter")
                {
                    Counter = Int32.Parse(listOfXElements[i].FirstAttribute.Value);
                }
            }
            */

            return savedBoard;
        }
    }
}
