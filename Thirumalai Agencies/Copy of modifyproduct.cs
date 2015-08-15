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
    public partial class modifyproduct : Form
    {
        SqlConnection con = null;
        string query = "";
        SqlCommand command1 = null;
        SqlDataReader reader1 = null;

        public modifyproduct()
        {
            InitializeComponent();
        }

        private void modifyproduct_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            groupBox1.Left = (groupBox1.Parent.Width - groupBox1.Width) / 2;
            groupBox1.Top = ((groupBox1.Parent.Height - groupBox1.Height) / 2) - 10;
            comboBox1.Focus();
            con = Class1.connection();
            UpdateCombo();
            try
            {
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("List Empty");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && comboBox1.Text != "")
            {
                con = Class1.connection();
                query = "update product set pid="+Convert.ToDecimal(comboBox1.Text)+", pname='"+textBox4.Text+"', pprice="+Convert.ToDecimal(textBox5.Text)+", sprice ="+Convert.ToDecimal (textBox6.Text) +", pvat="+ Convert.ToDecimal (textBox7.Text )+", discount="+Convert.ToDecimal(textBox8.Text)+", free='"+textBox1.Text+"' where pid ="+Convert.ToDecimal(comboBox1.Text)+"";
                //MessageBox.Show(query);
                command1 = new SqlCommand(query, con);
                con.Open();
                command1.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Updated..", "SUCCESS..");
                //textBox1.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
            }
            else
            {
                con.Close();
                MessageBox.Show("Some Data Missing..","Warning..!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateCombo()
        {
            try
            {
                query = "select pid from product";
                con.Open();
                command1 = new SqlCommand(query, con);
                reader1 = command1.ExecuteReader();

                comboBox1.Items.Clear();
                while (reader1.Read())
                {
                    comboBox1.Items.Add(reader1.GetDecimal(0));
                }

                reader1.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                try
                {
                    if (MessageBox.Show("Are You Sure to Delete Entry", "Confirm!!!") == System.Windows.Forms.DialogResult.OK)
                    {
                        query = "delete from product where pid =" + Convert.ToDecimal(comboBox1.Text) + "";
                        command1 = new SqlCommand(query, con);
                        con.Open();
                        command1.ExecuteNonQuery();
                        MessageBox.Show("Deleted..", "Success");
                    }
                    con.Close();
                    comboBox1.Items.Clear();
                    comboBox1.Text = "";
                    textBox1.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
                    UpdateCombo();
                    comboBox1.SelectedIndex = 0;
                    
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Select Product ID..", "Alert..!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                query = "select * from product where pid="+Convert.ToDecimal(comboBox1.Text)+"";

                command1 = new SqlCommand(query, con);
                con.Open();
                reader1 = command1.ExecuteReader();
                if (reader1.Read())
                {
                    textBox4.Text = reader1.GetString(1) + "";
                    textBox5.Text = reader1.GetDecimal(2) + "";
                    textBox6.Text = reader1.GetDecimal(3) + "";
                    textBox7.Text = reader1.GetDecimal(4) + "";
                    textBox8.Text = reader1.GetDecimal(5) + "";
                    textBox1.Text = reader1.GetString(6) + "";
                }
                reader1.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
