﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Othello.Linq
{
    public class SaveBoard
    {
        private XDocument doc;
        private int counter;
        private int[,] boardArray;
        private FileSystemWatcher watcher;
        private static Object locker = new Object();

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
            watcher = new FileSystemWatcher(@Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Linq");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "savedBoard.xml";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
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
            bool failed = true;
            while (failed)
            {
                try
                {
                    doc = XDocument.Load(@Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Linq\\savedBoard.xml");
                    failed = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Savefile already in use, skipping load.");
                }
            }
            int[,] savedBoard = new int[8, 8];

            IEnumerable<XElement> board = from el in doc.Elements("board") select el;
            IEnumerable<XElement> blacks = from el in board.Elements("black") select el;
            IEnumerable<XElement> whites = from el in board.Elements("white") select el;

            foreach (XElement el in blacks)
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
            counter = Int32.Parse(doc.Element("board").Element("counter").Attribute("counter").Value);
            return savedBoard;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                watcher.EnableRaisingEvents = false;
                BoardArray = restoreSavedGame();
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        public bool ContentEquals(int[,] arr, int[,] other)
        {
            if (arr != null && other != null) {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (arr[i, j] != other[i, j])
                        {
                            return false;
                        }
                    }
                }
            } else {
                return false;
            }
            return true;
        }

        public Action<int[,]> onBoardChange
        {
            get;
            set;
        }

        public int[,] BoardArray
        {
            get
            {
                return boardArray;
            }

            set
            {
                if (value != boardArray)
                {
                    boardArray = value;
                    Action<int[,]> localOnChange = onBoardChange;
                    if (localOnChange != null)
                    {
                        localOnChange(value);
                    }
                }
            }
        }
    }
}
