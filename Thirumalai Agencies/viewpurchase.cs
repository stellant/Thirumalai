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
    public partial class viewpurchase : Form
    {
        public viewpurchase()
        {
            InitializeComponent();
        }

        private void viewpurchase_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
