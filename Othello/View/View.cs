﻿using Othello.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello
{
    public partial class View : Form
    {
        private GameRules gameRule;
        string[] lines = System.IO.File.ReadAllLines(@"..\\..\\Resources\\mapInitial.txt");
        List<PictureBox> boxes = new List<PictureBox>();
        Rectangle rect = new Rectangle(0,0,600,600);
        Rectangle small = new Rectangle(0, 0, 66, 66);

   

        public View(GameRules g)
        { 
            InitializeComponent();
            populatePicArray(this.Controls);
            Console.WriteLine(boxes.Count);
            GameRule = g;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), rect);
        }

        public void paint(Image g, Image w, Image b)
        {
            for (int j = 0; j < boxes.Count; j++)
            {
                boxes[j].Image = g;
                if (boxes[j] == d4 || boxes[j] == e5)
                {
                    boxes[j].Image = w;
                }else if (boxes[j] == d5|| boxes[j]== e4)
                {
                    boxes[j].Image = b;

                }
            }
        }

        internal GameRules GameRule
        {
            get
            {
                return gameRule;
            }

            set
            {
                gameRule = value;
            }
        }

        private void a3_Click(object sender, EventArgs e)
        {

        }

        private void a7_Click(object sender, EventArgs e)
        {

        }

        private void h8_Click(object sender, EventArgs e)
        {

        }

        private void g1_Click(object sender, EventArgs e)
        {

        }

        //Adds all PicturBoxes to the List boxes by matching Tags with content of the stringArray Lines
        private void populatePicArray(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (lines.Contains(c.Tag))
                {
                    boxes.Add((PictureBox)c);
                }           
            }
        }

        private void tile_MouseClick(object sender, MouseEventArgs e)
        {
            GameRule.checkIfAllowed();
        }

    }
}