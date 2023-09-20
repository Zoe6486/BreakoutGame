using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ziying_s_Assignment2.Properties;

namespace Ziying_s_Assignment2
{
    internal class Bricks
    {

        private PictureBox[,] bricks;// 2D array to store the PictureBox controls representing the bricks
        public int rows;
        public int cols;
        public int[,] firmness;  // 2D array to store the firmness values of the bricks

        /// <summary>
        /// Constructor for the Bricksclass
        /// </summary>
        public Bricks(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            bricks = new PictureBox[rows, cols];
            firmness = new int[rows, cols];

            GenerateBircks(); // Generate the bricks with random firmness and colors
            AddBombBricks(); // Add bomb bricks to the game
        }

        /// <summary>
        /// Generates the bricks with random firmness and colors
        /// </summary>
        private void GenerateBircks()
        {
            Random random = new Random();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int brickFirmness = random.Next(1, 5); // Randomly generate a firmness value between 1 and 4
                    Color brickColor = SetBrickColor(brickFirmness); // Set the color of the brick based on the firmness value

                    PictureBox brick = new PictureBox();
                    brick.BackColor = brickColor;
                    brick.Width = 200;
                    brick.Height = 50;
                    int brickSpacing = 10;
                    int DistanceToWall = 20;
                    // Calculate the position of the brick on the form
                    brick.Left = col * (brick.Width + brickSpacing) + DistanceToWall;
                    brick.Top = row * (brick.Height + brickSpacing) + DistanceToWall;

                    bricks[row, col] = brick; // Store the brick control in the bricks array
                    firmness[row, col] = brickFirmness; // Store the firmness value of the brick
                }
            }
        }

        /// <summary>
        /// Set the color of the brick based on its firmness value
        /// </summary>
        /// <param name="firmness"></param>
        /// <returns></returns>
        public Color SetBrickColor(int firmness)
        {
            switch (firmness)
            {
                case 1:
                    return Color.Yellow;
                case 2:
                    return Color.LawnGreen;
                case 3:
                    return Color.Cyan;
                case 4:
                    return Color.MediumSlateBlue;
                default:
                    return Color.White;
            }
        }

        /// <summary>
        /// Add bomb bricks to the game
        /// </summary>
        private void AddBombBricks()
        {
            Random random = new Random();

            // Add the first bomb brick
            int bombBrick1Row = random.Next(0, rows);
            int bombBrick1Col = random.Next(0, cols);
            firmness[bombBrick1Row, bombBrick1Col] = -1; // Set the firmness value of the bomb brick to -1
            bricks[bombBrick1Row, bombBrick1Col].BackColor = Color.Tomato;
            bricks[bombBrick1Row, bombBrick1Col].Image = Ziying_s_Assignment2.Properties.Resources.bomb;
            
            // Add the second bomb and ensure their locations are different
            int bombBrick2Row, bombBrick2Col;
            do
            {
                bombBrick2Row = random.Next(0, rows);
                bombBrick2Col = random.Next(0, cols);
            } while (bombBrick2Row == bombBrick1Row && bombBrick2Col == bombBrick1Col);
            firmness[bombBrick2Row, bombBrick2Col] = -1;
            bricks[bombBrick2Row, bombBrick2Col].BackColor = Color.Tomato;
            bricks[bombBrick2Row, bombBrick2Col].Image = Ziying_s_Assignment2.Properties.Resources.bomb;
        }

        
        public PictureBox SelectedBrick(int row, int col)
        {
            return bricks[row, col];
        }

        /// <summary>
        /// Reduce the firmness of the brick at the specified position
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void ReduceFirmness(int row, int col)
        {
            if (row >= 0 && row < rows && col >= 0 && col < cols)
            {
                int brickFirmness = firmness[row, col];
                if (brickFirmness > 0)
                {
                    brickFirmness--;
                    firmness[row, col] = brickFirmness;
                    bricks[row, col].BackColor = SetBrickColor(brickFirmness); // Update the color of the brick based on the reduced firmness
                }
            }
        }

        /// <summary>
        /// Check if the brick at the specified position is destroyed
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool IsBrickDestroyed(int row, int col)
        {
            if (row >= 0 && row < rows && col >= 0 && col < cols)
            {
                return firmness[row, col] == 0; // Return true if the firmness value of the brick is 0, indicating it is destroyed
            }
            return false;
        }

        /// <summary>
        /// Check if the brick at the specified position is a bomb brick
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool isBombBrickHit(int row, int col)
        {
            if(row >= 0 && row < rows && col >= 0 && col < cols)
            {
                return firmness[row, col] == -1; // Return true if the firmness value of the brick is -1, indicating it is a bomb brick
            }
            return false; 
        }
    }
}
