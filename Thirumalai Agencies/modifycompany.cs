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
    public partial class modifycompany : Form
    {
        public void reload()
        {
            try
            {
                SqlConnection con = null;
                con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select csname from cs", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.comboBox1.Items.Add(dr.GetString(0));
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                String msg = ex.ToString();
                MessageBox.Show(msg);
            }
        }
        public modifycompany()
        {
            InitializeComponent();
        }

        private void modifycompany_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            groupBox1.Left = (groupBox1.Parent.Width - groupBox1.Width) / 2;
            groupBox1.Top = ((groupBox1.Parent.Height - groupBox1.Height) / 2) - 10;
            try
            {
                SqlConnection con = null;
                con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select csname from cs", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.comboBox1.Items.Add(dr.GetString(0));
                }
                dr.Close();
                con.Close();
                try
                {
                    comboBox1.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("List Empty");
                }
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                String msg = ex.ToString();
                MessageBox.Show(msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "")
                {
                    String type = "";
                    SqlConnection con = Class1.connection();
                    con.Open();
                    if (radioButton1.Checked == true && radioButton2.Checked == false)
                    {
                        type = "Customer";
                    }
                    else if (radioButton1.Checked == false && radioButton2.Checked == true)
                    {
                        type = "Seller";
                    }
                    SqlCommand cmd = new SqlCommand("update cs set cstype='" + type + "',csperson='" + textBox2.Text + "',csaddress='" + textBox3.Text + "',csphone='" + textBox4.Text + "',csmobile='" + textBox5.Text + "',csemail='" + textBox6.Text + "',cstinno='" + textBox7.Text + "',cscsicno='" + textBox8.Text + "' where csname='"+comboBox1.Text+"'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    String name = "Company Name " + comboBox1.Text + " updated.";
                    MessageBox.Show(name, "Success");
                }
                else
                {
                    MessageBox.Show("ComboBox Field Empty", "Warning!!!");
                }
                
            }
            catch (Exception ex)
            {
                String msg = ex.ToString();
                MessageBox.Show(msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "")
                {
                    SqlConnection con = Class1.connection();
                    con.Open();
                    if (MessageBox.Show("Are You Sure to Delete Entry", "Confirm!!!") == System.Windows.Forms.DialogResult.OK)
                    {
                        SqlCommand cmd = new SqlCommand("delete from cs where csname='"+comboBox1.Text+"'",con);
                        cmd.ExecuteNonQuery();
                        String name = "Company Name " + comboBox1.Text + " deleted.";
                        MessageBox.Show(name, "Success");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("ComboBox Field Empty","Warning!!!");
                }
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
                reload();
            }
            catch (Exception ex)
            {
                String msg = ex.ToString();
                MessageBox.Show(msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String csname = comboBox1.Text;
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from cs where csname='"+csname+"'",con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr.GetString(1) == "Customer")
                    {
                        radioButton1.Checked = true;
                        radioButton2.Checked = false;
                    }
                    else if (dr.GetString(1) == "Seller")
                    {
                        radioButton1.Checked = false;
                        radioButton2.Checked = true;
                    }
                    textBox2.Text = dr.GetString(2);
                    textBox3.Text = dr.GetString(3);
                    textBox4.Text = dr.GetString(4);
                    textBox5.Text = dr.GetString(5);
                    textBox6.Text = dr.GetString(6);
                    textBox7.Text = dr.GetString(7);
                    textBox8.Text = dr.GetString(8);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                String msg = ex.ToString();
                MessageBox.Show(msg);
            }
        }

      
        
    }
}

