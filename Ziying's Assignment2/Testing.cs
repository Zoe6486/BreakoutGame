using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziying_s_Assignment2
{
    internal class Testing
    {
        private Ball testingBall;
        private Bricks testingBricks;
        private Paddle testingPaddle;

        public Testing(Ball testingBall, Bricks testingBricks, Paddle testingPaddle)
        {
            this.testingBall = testingBall;
            this.testingBricks = testingBricks;
            this.testingPaddle = testingPaddle;
        }

        public void BallTest()
        {
            testingBall.HorizontalSpeed = 5;
            testingBall.VerticalSpeed = 5;
        }

        public void BricksTest()
        {
            testingBricks.rows = 7;
            testingBricks.cols = 8;
        }

        public void PaddleTest()
        {
            testingPaddle.PaddleMove(10, 1100);
        }

    }
}
