using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ziying_s_Assignment2
{

    public partial class Start : Form
    {
        private Game frmGame;
        private Manager manager;
        
        public Start()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            frmGame = new Game();
            frmGame.ShowDialog();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (frmGame == null)
            {
                frmGame = new Game();
            }
            frmGame.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
