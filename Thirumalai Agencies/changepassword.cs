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
    public partial class changepassword : Form
    {
        public changepassword()
        {
            InitializeComponent();
        }

        private void changepassword_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            groupBox1.Left = (groupBox1.Parent.Width - groupBox1.Width) / 2;
            groupBox1.Top = (groupBox1.Parent.Height - groupBox1.Height) / 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox1.Text == "") && (textBox2.Text == ""))
                {
                    label3.ForeColor = Color.Red;
                    textBox1.Focus();
                    label3.Text = "Old and New Password Should Not Be Empty";
                }
                else if ((textBox1.Text == "") && (textBox2.Text != ""))
                {
                    label3.ForeColor = Color.Red;
                    textBox1.Focus();
                    label3.Text = "Old Password Should Not Be Empty";
                }
                else if ((textBox1.Text != "") && (textBox2.Text == ""))
                {
                    label3.ForeColor = Color.Red;
                    textBox2.Focus();
                    label3.Text = "New Password Should Not Be Empty";
                }
                else
                {
                    SqlConnection con = Class1.connection();
                    SqlDataReader dr1;
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("select password from login where username='admin' and password='" + textBox1.Text + "'", con);
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.Read())
                    {
                        if ((textBox1.Text == dr1.GetString(0)))
                        {
                            dr1.Close();
                            SqlCommand cmd2 = new SqlCommand("update login set password='" + textBox2.Text + "' where username='admin' and password='"+textBox1.Text+"'", con);
                            cmd2.ExecuteNonQuery();
                            label3.ForeColor = Color.Green;
                            label3.Text = "Password Changed";
                        }
                    }
                    else
                    {
                        label3.ForeColor = Color.Red;
                        label3.Text = "Old Password Mismatch";
                    }
                    dr1.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
