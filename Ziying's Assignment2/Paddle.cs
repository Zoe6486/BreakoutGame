using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ziying_s_Assignment2
{
    class Paddle
    {
        private PictureBox picPaddle;
        private int paddleSpeed;
   
        /// <summary>
        /// Constructor for the Paddle class
        /// </summary>
        public Paddle( PictureBox picPaddle, int paddleSpeed)
        {
            
            this.picPaddle = picPaddle;
            this.paddleSpeed = paddleSpeed;
        }

        public PictureBox SelectedPaddle
        {
            get { return picPaddle; } // Property to get the PictureBox control representing the paddle
        }

        /// <summary>
        /// Move the paddle horizontally by a specified amount
        /// </summary>
        /// <param name="stepX"></param>
        /// <param name="formWidth"></param>
        public void PaddleMove(int stepX, int formWidth)
        {
            picPaddle.Left += stepX; // Move the paddle by the specified step

            if (picPaddle.Left < 0) // Check if the paddle has reached the left boundary of the form
            {
                picPaddle.Left = 0; // Set the paddle's left position to 0 to prevent it from going off the form
            }
            else if (picPaddle.Left > formWidth - picPaddle.Width) // Check if the paddle has reached the right boundary of the form
            {
                picPaddle.Left = formWidth - picPaddle.Width; // Set the paddle's left position to the maximum value to prevent it from going off the form
            }
        }
    }
}
