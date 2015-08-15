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
    public partial class Form1 : Form
    {
        int i = 0;
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.AliceBlue;
            this.TransparencyKey = Color.AliceBlue;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i = i + 1;
            if (i > 5)
            {
                timer1.Stop();
                this.Hide();
                Form2 form2 = new Form2();
                form2.Show();
               // MDIParent1 m = new MDIParent1();
               // m.Show();
                
            }
        }

    
    }
}
