﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        private string file = "savedBoard.xml";

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
            doc = XDocument.Load(file);
            watcher = new FileSystemWatcher(".");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = file;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        public void storeBoard(int[,] boardArray, int counter)
        {
            doc.Element("board").RemoveAll();
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (boardArray[row, column] == 1)
                    {
                        doc.Element("board").Add(new XElement("white", new XAttribute("xCoord", row), new XAttribute("yCoord", column)));
                    }
                    else if (boardArray[row, column] == 2)
                    {
                        doc.Element("board").Add(new XElement("black", new XAttribute("xCoord", row), new XAttribute("yCoord", column)));
                    }
                }
            }

            doc.Element("board").Add(new XElement("counter", new XAttribute("counter", counter)));
            Mutex mutexSave = new Mutex(false, file);
            try
            {
                mutexSave.WaitOne();
                doc.Save(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Savefile already in use, skipping save.");
            }
            finally
            {
                mutexSave.ReleaseMutex();
            }
        }

        public int[,] restoreSavedGame()
        {   
            bool failed = true;
            Mutex mutex = new Mutex(false, file);
            while (failed)
            {
                try
                {
                    mutex.WaitOne();
                    doc = XDocument.Load(file);
                    failed = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Savefile already in use, retrying load.");
                }
                finally
                {
                    mutex.ReleaseMutex();
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
