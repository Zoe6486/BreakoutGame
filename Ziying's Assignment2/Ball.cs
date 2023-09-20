using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ziying_s_Assignment2
{
    internal class Ball
    {
        private PictureBox picBall;
        private int verticalSpeed, horizontalSpeed; // Speed of the ball in the vertical and horizontal directions

        /// <summary>
        /// Constructor for the Ball class
        /// </summary>
        public Ball(PictureBox picBall, int verticalSpeed, int horizontalSpeed)
        {
            this.picBall = picBall;
            this.verticalSpeed = verticalSpeed;
            this.horizontalSpeed = horizontalSpeed;
        }

        /// <summary>
        /// Property to get the selected ball PictureBox
        /// </summary>
        public PictureBox SelectedBall
        {
            get { return picBall; }
        }

        /// <summary>
        /// Property to get or set the vertical speed of the ball
        /// </summary>
        public int VerticalSpeed
        {
            get { return verticalSpeed; }
            set { this.verticalSpeed = value; }
        }

        /// <summary>
        /// Property to get or set the horizontal speed of the ball
        /// </summary>
        public int HorizontalSpeed
        {
            get { return horizontalSpeed; }
            set { this.horizontalSpeed = value; }
        }

        /// <summary>
        /// Method to move the ball based on its current speed
        /// </summary>
        public void BallMove()
        {
            //picBall.Top += verticalSpeed; 
            //picBall.Left += horizontalSpeed; 
            Point newLocation = new Point(picBall.Location.X + horizontalSpeed, picBall.Location.Y + verticalSpeed);
            picBall.Location = newLocation;
        }
    }
}
