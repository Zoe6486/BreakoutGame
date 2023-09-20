using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;


namespace Ziying_s_Assignment2
{
    class Manager
    {
        private Game frmGame;

        // Game objects
        private PictureBox picBall;
        private PictureBox picPaddle;
        private Bricks bricks;
        private Ball ball;
        private Paddle paddle;

        // Pre-game UI elements
        private Panel pnlChooseLevel;
        private Label lblChoose;
        private Button btnEasy;
        private Button btnNormal;
        private Button btnDifficult;

        // In-game UI elements
        private Button btnPauseGame;
        private Button btnContinueGame;
        private Button btnRestart;
        private Button btnReturn;
        private bool isGameStarted;
        private bool isGamePaused;

        // Game settings and variables
        private int verticalSpeed;
        private int horizontalSpeed;
        private int r = 2;
        private int bricksRows;
        private int bricksCols;
        private int bricksCounts;
        private int paddleSpeed;

        private int score;
        private Label lblScore;
        private Label lblCountdown;

        // Game ending UI elements
        private Panel pnlEnding;
        private Label lblEnding;
        private Button btnReplay;

        // Sound players
        public SoundPlayer hit_brick, hit_paddle, game_won, game_over;

        /// <summary>
        /// Constructor for the manager class
        /// </summary>
        public Manager(Game frmGame)
        {
            frmGame.Width = 1100; 
            frmGame.Height = 800;
            this.frmGame = frmGame;

            InitialisePreGame();
            InitialiseGameContents();
            InitialiseGameControls();
            InitialiseGameEnding();
            frmGame.timer.Tick += new System.EventHandler(this.timer_Tick);

            hit_brick = new SoundPlayer();
            hit_brick.SoundLocation = @"..\..\audio\hit_brick.wav";
            hit_paddle = new SoundPlayer();
            hit_paddle.SoundLocation = @"..\..\audio\hit_paddle.wav";
            game_won = new SoundPlayer();
            game_won.SoundLocation = @"..\..\audio\game_won.wav";
            game_over = new SoundPlayer();
            game_over.SoundLocation = @"..\..\audio\game_over.wav";
        }


        private void InitialisePreGame()
        {
            // Add Choosing-Level panel
            pnlChooseLevel = new Panel();
            pnlChooseLevel.BackColor = System.Drawing.Color.Black;
            pnlChooseLevel.BorderStyle = BorderStyle.FixedSingle;
            pnlChooseLevel.Size = new Size(500, 400);
            pnlChooseLevel.Location = new Point(300, 200);
            pnlChooseLevel.TabIndex = 3;
            frmGame.Controls.Add(pnlChooseLevel);

            //Add some texts in a label
            lblChoose = new Label();
            lblChoose.AutoSize = true;
            lblChoose.Font = new System.Drawing.Font("Mistral", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblChoose.ForeColor = System.Drawing.Color.White;
            lblChoose.Text = "CHOOSE A LEVEL TO START!";
            lblChoose.Size = new Size(300, 80);
            lblChoose.Location = new Point(20, 50);
            pnlChooseLevel.Controls.Add(lblChoose);

            //Add Easy-Level button
            btnEasy = new Button();
            btnEasy.Font = new System.Drawing.Font("Mistral", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnEasy.BackColor = System.Drawing.Color.White;
            btnEasy.Text = "EASY";
            btnEasy.Size = new Size(250, 50);
            btnEasy.Location = new Point(125, 125);
            pnlChooseLevel.Controls.Add(btnEasy);

            //Add Normal-Level button
            btnNormal = new Button();
            btnNormal.Font = new System.Drawing.Font("Mistral", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnNormal.BackColor = System.Drawing.Color.White;
            btnNormal.Text = "NORMAL";
            btnNormal.Size = new Size(250, 50);
            btnNormal.Location = new Point(125, 200);
            pnlChooseLevel.Controls.Add(btnNormal);

            //Add Difficult-Level button
            btnDifficult = new Button();
            btnDifficult.Font = new System.Drawing.Font("Mistral", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnDifficult.BackColor = System.Drawing.Color.White;
            btnDifficult.Text = "DIFFICULT";
            btnDifficult.Size = new Size(250, 50);
            btnDifficult.Location = new Point(125, 275);
            pnlChooseLevel.Controls.Add(btnDifficult);

            // Bind Event Handlers
            btnEasy.Click += new System.EventHandler(this.btnEasy_Click);
            btnNormal.Click += new System.EventHandler(this.btnNormal_Click);
            btnDifficult.Click += new System.EventHandler(this.btnDifficult_Click);
        }


        private void InitialiseGameContents()
        {
            //Add a Countdown label
            lblCountdown = new Label();
            lblCountdown.Size = new Size(300, 40);
            lblCountdown.Location = new Point(500, 350);
            lblCountdown.Text = "3";
            lblCountdown.AutoSize = true;
            lblCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblCountdown.ForeColor = System.Drawing.Color.White;
            frmGame.Controls.Add(lblCountdown);

            // Set the ball speed and the rows and cols of the bricks
            verticalSpeed = 25;
            horizontalSpeed = 25;
            bricksRows = r;
            bricksCols = 5;
            paddleSpeed = 5;

            // Add the paddle
            picPaddle = new PictureBox();
            picPaddle.Width = 200;
            picPaddle.Height = 25;
            picPaddle.Left = 450;
            picPaddle.Top = 650;
            picPaddle.BackColor = Color.Gray;
            paddle = new Paddle(picPaddle, paddleSpeed);
            frmGame.Controls.Add(paddle.SelectedPaddle);

            //Add the bricks
            bricks = new Bricks(bricksRows, bricksCols);
            bricksCounts = bricks.rows * bricks.cols;
            for (int row = 0; row < bricks.rows; row++)
            {
                for (int col = 0; col < bricks.cols; col++)
                {
                    frmGame.Controls.Add(bricks.SelectedBrick(row, col));
                }
            }

            // Add the ball
            picBall = new PictureBox();
            picBall.Image = Ziying_s_Assignment2.Properties.Resources.ImgBall;
            picBall.SizeMode = PictureBoxSizeMode.StretchImage;
            picBall.Size = new Size(25, 25);
            picBall.Location = new Point(535, 500);
            picBall.BackColor = Color.Transparent;
            ball = new Ball(picBall, verticalSpeed, horizontalSpeed);
            frmGame.Controls.Add(ball.SelectedBall);
        }

        private void InitialiseGameControls()
        {
            // Add the score label to record the score that the player win 
            lblScore = new Label();
            lblScore.Size = new Size(300, 40);
            lblScore.Location = new Point(25, 705);
            lblScore.Text = "Score: ";
            lblScore.AutoSize = true;
            lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblScore.ForeColor = System.Drawing.Color.White;
            frmGame.Controls.Add(lblScore);

            //Add Pause button
            btnPauseGame = new Button();
            btnPauseGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnPauseGame.BackColor = System.Drawing.Color.White;
            btnPauseGame.Text = "Pause";
            btnPauseGame.Size = new Size(80, 30);
            btnPauseGame.Location = new Point(700, 700);
            frmGame.Controls.Add(btnPauseGame);

            //Add Continue button
            btnContinueGame = new Button();
            btnContinueGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnContinueGame.BackColor = System.Drawing.Color.White;
            btnContinueGame.Text = "Continue";
            btnContinueGame.Size = new Size(80, 30);
            btnContinueGame.Location = new Point(800, 700);
            frmGame.Controls.Add(btnContinueGame);

            //Add Restart button
            btnRestart = new Button();
            btnRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnRestart.BackColor = System.Drawing.Color.White;
            btnRestart.Text = "Restart";
            btnRestart.Size = new Size(80, 30);
            btnRestart.Location = new Point(900, 700);
            frmGame.Controls.Add(btnRestart);

            //Add Return button
            btnReturn = new Button();
            btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnReturn.BackColor = System.Drawing.Color.White;
            btnReturn.Text = "Return";
            btnReturn.Size = new Size(80, 30);
            btnReturn.Location = new Point(1000, 700);
            frmGame.Controls.Add(btnReturn);


            //Initialise game settings
            score = 0;
            isGameStarted = false;
            isGamePaused = false;

            //Initialise Event Handlers
            frmGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Game_MouseMove);
            btnPauseGame.Click += new System.EventHandler(this.btnPauseGame_Click);
            btnContinueGame.Click += new System.EventHandler(this.btnContinueGame_Click);
            btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
        }

        private void InitialiseGameEnding()
        {
            // Add an ending panel when game ends 
            pnlEnding = new Panel();
            pnlEnding.BackColor = System.Drawing.Color.Black;
            pnlEnding.BorderStyle = BorderStyle.FixedSingle;
            pnlEnding.Size = new Size(500, 300);
            pnlEnding.Location = new Point(300, 300);
            pnlEnding.TabIndex = 3;
            frmGame.Controls.Add(pnlEnding);

            // Add a label to display some text
            lblEnding = new Label();
            lblEnding.Size = new Size(300, 50);
            lblEnding.Location = new Point(125, 80);
            lblEnding.AutoSize = true;
            lblEnding.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblEnding.ForeColor = System.Drawing.Color.White;
            pnlEnding.Controls.Add(lblEnding);

            // Add a replay button on the ending panel to replay the game
            btnReplay = new Button();
            btnReplay.Font = new System.Drawing.Font("Mistral", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnReplay.BackColor = System.Drawing.Color.White;
            btnReplay.Text = "REPLAY";
            btnReplay.Size = new Size(250, 50);
            btnReplay.Location = new Point(125, 200);
            pnlEnding.Controls.Add(btnReplay);

            // Make the ending panel hid when the game starts
            pnlEnding.Visible = false;

            btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
        }

        /// <summary>
        /// Create a method to start the game
        /// </summary>
        public void StartGame()
        {
            pnlChooseLevel.Visible = false;
            // Show countdown and start game timer after 3 seconds
            for (int i = 3; i > 0; --i)
            {
                lblCountdown.Text = i.ToString();
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
            }
            isGameStarted = true;
            isGamePaused = false;
            frmGame.timer.Start();
            lblCountdown.Text = "";
        }

        /// <summary>
        /// Create a method to pause the game
        /// </summary>
        public void PauseGame()
        {
            isGameStarted = true;
            isGamePaused = true;
            frmGame.timer.Stop();
        }

        /// <summary>
        /// Create a method to continue the game
        /// </summary>
        public void ContinueGame()
        {
            isGameStarted= true;
            isGamePaused = false;
            frmGame.timer.Start();
        }

        /// <summary>
        /// Create a method to retart the game
        /// </summary>
        private void RestartGame()
        {
            frmGame.Controls.Clear();
            InitialisePreGame();
            InitialiseGameContents();
            InitialiseGameControls();
            InitialiseGameEnding();
            frmGame.timer.Start();
        }

        /// <summary>
        /// Create a method to control the controls when timer starts
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (isGameStarted && !isGamePaused)
            {
                ball.BallMove();
                CheckCollision();
            }
        }

        /// <summary>
        /// Create a method to check collison
        /// </summary>
        public void CheckCollision()
        {
            // Check collision between the ball and the form boundary
            if (ball.SelectedBall.Left <= 0 || ball.SelectedBall.Right >= frmGame.Width)
            {
                hit_paddle.Play();
                ball.HorizontalSpeed *= -1;
            }

            if (ball.SelectedBall.Top <= 0)
            {
                hit_paddle.Play();
                ball.VerticalSpeed *= -1;
            }

            if (ball.SelectedBall.Bottom >= frmGame.Height)
            {
                GameOver(); // If the ball collided with the form bottom, the game fails
            }

            // Check collision between the ball and the paddle
            if (paddle.SelectedPaddle.Bounds.IntersectsWith(ball.SelectedBall.Bounds))
            {
                hit_paddle.Play();
                ball.VerticalSpeed *= -1;
            }

            // Check collision between the ball and the bricks
            for (int row = 0; row < bricks.rows; row++)
            {

                for (int col = 0; col < bricks.cols; col++)
                {
                    PictureBox brickPictureBox = bricks.SelectedBrick(row, col);
                    if (brickPictureBox != null && brickPictureBox.Visible && brickPictureBox.Bounds.IntersectsWith(ball.SelectedBall.Bounds))
                    {
                        if (bricks.firmness[row,col] >= 1)
                        {
                            hit_paddle.Play();
                        }
                        bricks.ReduceFirmness(row, col);
                        score += 10; // Once the brick is hit, the score increases by 10
                        lblScore.Text = "Score: " + score.ToString();
                        if (bricks.IsBrickDestroyed(row, col))
                        { 
                            hit_brick.Play();
                            brickPictureBox.Visible = false;
                            bricksCounts--; //Recoding the existing bricks counts
                            score += 5; // Once the brick is destroyed, the score increases by 5
                            lblScore.Text = "Score: " + score.ToString();
                        }
                        
                        else if(bricks.isBombBrickHit(row, col))
                        {
                            hit_brick.Play();
                            brickPictureBox.Visible = false;
                            bricksCounts = 0; 
                            score += 50; // Once the bomb brick is hit, the score increases by 50
                            lblScore.Text = "Score: " + score.ToString();
                            GamePassed();
                        }
                        ball.VerticalSpeed *= -1;
                        break;
                    }
                }
            }

            if (bricksCounts == 0)
            {
                GamePassed(); // When all bricks are destroyed, this game level is passed 
            }
        }

        /// <summary>
        /// Create a method to end the game when failed
        /// </summary>
        private void GameOver()
        {
            game_over.Play();
            paddle.SelectedPaddle.Visible = false;
            ball.SelectedBall.Visible = false;
            frmGame.timer.Stop();
            lblEnding.Text = "Sorry, Game Over!  \nYour Score is: " + score + "\n\n Replay it?";
            pnlEnding.Visible = true;
        }

        /// <summary>
        /// Create a method to end the game when passed the level
        /// </summary>
        private void GamePassed()
        {
            for (int row = 0; row < bricks.rows; row++)
            {
                for (int col = 0; col < bricks.cols; col++)
                {
                    frmGame.Controls.Remove(bricks.SelectedBrick(row, col));
                }
            }
            paddle.SelectedPaddle.Visible = false;
            ball.SelectedBall.Visible = false;
            frmGame.timer.Stop();
            lblEnding.Text = "Wow! You pass this level!  \nYour Score is: " + score + "\n\n Replay it?";
            pnlEnding.Visible = true;
            game_won.Play();
        }


        /// <summary>
        /// Bind the mouse move with paddle move
        /// </summary>
        private void Game_MouseMove(object sender, MouseEventArgs e)
        {
            if (isGameStarted && !isGamePaused)
            {
                int mouseX = e.X;
                int paddleX = mouseX - paddle.SelectedPaddle.Width / 2;
                paddle.PaddleMove(paddleX - paddle.SelectedPaddle.Left, frmGame.Width);
            }
        }

        /// <summary>
        /// Set the Easy level with 2-rows bricks
        /// </summary>
        private void btnEasy_Click(object sender, EventArgs e)
        {
            frmGame.Controls.Clear(); 
            r = 2;
            InitialiseGameContents();
            InitialiseGameControls();
            InitialiseGameEnding();
            StartGame();
        }

        /// <summary>
        /// Set the Normal level with 4-rows bricks
        /// </summary>
        private void btnNormal_Click(object sender, EventArgs e)
        {
            frmGame.Controls.Clear();
            r = 4;
            InitialiseGameContents();
            InitialiseGameControls();
            InitialiseGameEnding();
            StartGame();
        }

        /// <summary>
        /// Set the Difficult level with 5-rows bricks
        /// </summary>
        private void btnDifficult_Click(object sender, EventArgs e)
        {
            frmGame.Controls.Clear();
            r = 5;
            InitialiseGameContents();
            InitialiseGameControls();
            InitialiseGameEnding();
            StartGame();
        }

        private void btnPauseGame_Click(object sender, EventArgs e)
        {
            if(isGameStarted)
            PauseGame();
        }

        private void btnContinueGame_Click(object sender, EventArgs e)
        {
            if (isGameStarted)
            ContinueGame();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            PauseGame(); 
            frmGame.Close();
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
