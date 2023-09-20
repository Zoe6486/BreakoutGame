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
    public partial class Game : Form
    {
        private Manager manager;

        public Game()
        {
            InitializeComponent();
            manager = new Manager(this);
            this.CenterToScreen();
            this.DoubleBuffered = true;
        }
    }
}
