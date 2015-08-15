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
    public partial class addcompany : Form
    {
        public addcompany()
        {
            InitializeComponent();
        }

        private void addcompany_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            groupBox1.Left = (groupBox1.Parent.Width - groupBox1.Width) / 2;
            groupBox1.Top = ((groupBox1.Parent.Height - groupBox1.Height) / 2)-10;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            label1.Text = "Name of the Customer/Seller";
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                label1.Text = "Name of the Customer's Concern";
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                textBox1.Focus();
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == true)
            {
                label1.Text = "Name of the Seller's Concern";
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                textBox1.Focus();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                label1.Text = "Name of the Customer's Concern";
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                textBox1.Focus();
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == true)
            {
                label1.Text = "Name of the Seller's Concern";
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "")
            {
                try
                {
                    String type = null;
                    SqlConnection con = null;
                    if (radioButton1.Checked == true)
                    {
                        type = "Customer";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        type = "Seller";
                    }
                    con = Class1.connection();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into cs values('" + textBox1.Text + "','" + type + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    String name = "Company Name "+textBox1.Text.ToString()+" added.";
                    MessageBox.Show(name,"Success");
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
                    radioButton1.Checked = radioButton2.Checked = false;
                    label1.Text = "Name of the Customer/Seller";
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = false;
                    
                    
                }
                catch (Exception ex)
                {
                    String msg = "" + ex.ToString();
                    MessageBox.Show(msg,"Warning!!!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Some Fields are Empty","Warning!!!");
            }
            
        }
    }
}