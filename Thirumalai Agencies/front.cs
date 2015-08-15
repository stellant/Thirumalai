using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Thirumalai_Agencies
{
    public partial class front : Form
    {
        int i = 0;
        public front()
        {
            InitializeComponent();
        }

        private void front_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            pictureBox1.Left = ((pictureBox1.Parent.Width - pictureBox1.Width) / 2);
            label1.Left = ((label1.Parent.Width - label1.Width) / 2+65);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i == 0)
            {
                label3.ForeColor = Color.Purple;
            }
            else if (i == 1)
            {
                label3.ForeColor = Color.Maroon;
            }
            else if (i == 2)
            {
                label3.ForeColor = Color.Black;
            }
            else if (i == 3)
            {
                label3.ForeColor = Color.Green;
            }
            else if (i == 4)
            {
                label3.ForeColor = Color.DarkMagenta;
            }
            else if (i == 5)
            {
                label3.ForeColor = Color.DarkTurquoise;
                i = 0;
            }
            i++;
        }
    }
}
