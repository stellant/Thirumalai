using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Thirumalai_Agencies
{
    public partial class Form2 : Form
    {
        int i = 1;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "")
                {
                    label3.ForeColor = Color.Red;
                    textBox2.Focus();
                    label3.Text = "Password Should Not Be Empty";
                }
                else
                {
                    SqlConnection con = Class1.connection();
                    SqlDataReader dr;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from login where username='" + textBox1.Text + "' and password='"+textBox2.Text+"'", con);
                    dr=cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if ((textBox1.Text == dr.GetString(0)) && (textBox2.Text == dr.GetString(1)))
                        {
                            label3.ForeColor = Color.Green;
                            timer1.Start();
                            label3.Text = "Login Successful. Wait " + i + " Seconds.";
                        }
                    }
                    else
                    {
                        label3.ForeColor = Color.Red;
                        label3.Text = "Password Mismatch";
                        textBox2.Text = "";
                        textBox2.Focus();
                    }
                    dr.Close();
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i = i - 1;
            label3.Text = "Login Successful. Wait " + i+" Seconds.";
            if (i == 0)
            {
                timer1.Stop();
                this.Hide();
                new MDIParent1().Show();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = "admin";
        }
    }
}
