using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello.Model
{
  public  class AI
    {
        //   private GameRules gameRules;
        private List<PictureBox> greenTiles;
        private List<List<PictureBox>> compare;
        private int score;
        private Boolean wait = true;
        private PictureBox aiPressedTile;

        public PictureBox AiPressedTile
        {
            get
            {
                return aiPressedTile;
            }

            set
            {
                aiPressedTile = value;
            }
        }

        public AI()
        {
            //       score = 0;
            //    gameRules = g;
            //       greenTiles = new List<PictureBox>();
            //       compare = new List<List<PictureBox>>();
            //       AiPressedTile = new PictureBox();
        }


        /*      public void aiTurn()
              {
                //  wait = true; 
                  getAllGreenTiles();
                  tryPlaceMarker();
                  compareResults();
               //   gameRules.AiIsCalculating = true;
                //  gameRules.makeMove(AiPressedTile);
              }

              public void getAllGreenTiles()
              {
                  foreach (PictureBox pb in gameRules.Board)
                  {

                      if (pb.Tag.Equals("green"))
                      {
                          greenTiles.Add(pb);
                        //  Console.WriteLine(pb.Tag);
                      }
                  }
              }
              public void tryPlaceMarker()
              {
                  foreach (PictureBox pb in greenTiles)
                  {
                      gameRules.checkAllDirections(pb);
                     // Console.WriteLine(pb.Tag);
                    //  Console.WriteLine(pb.Name);
                    //  Console.WriteLine("HEJ");
                      if (gameRules.PaintList.Any())
                      {
                          compare.Add(gameRules.PaintList);
                      }
                  }
              }
              public void compareResults()
              {
                  foreach (List<PictureBox> listPb in compare)
                  {
                      if (listPb.Count > score)
                      {
                          score = listPb.Count;
                          AiPressedTile = listPb.Last();
                      }
                  }

              }
          }
          */
    }
}
