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
    public partial class Welcome : Form
    {
        private Start frmStart;
        private Instructions frmInstructions;
        public Welcome()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (frmStart == null)
            {
                frmStart = new Start();
            }
            frmStart.ShowDialog();
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            if (frmInstructions == null)
            {
                frmInstructions = new Instructions();
            }
            frmInstructions.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
