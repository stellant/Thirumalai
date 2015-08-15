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
    public partial class addproduct : Form
    {
        public addproduct()
        {
            InitializeComponent();
        }
        private void loadgrid()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                DataTable dt=new DataTable();
                SqlDataAdapter ada=new SqlDataAdapter("select pid as PRODUCTID,pname as PRODUCTNAME,sprice as SALESPRICE,pvat as VAT,discount as DISCOUNT,free as FREEOFFERS from product order by pid ASC",con);
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                String s = ex.ToString();
                MessageBox.Show(s);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void loadcompany()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select csname from cs where cstype='Seller'",con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr.GetString(0).ToString());
                }
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void addproduct_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            groupBox1.Left = (groupBox1.Parent.Width - groupBox1.Width) / 2;
            groupBox1.Top = ((groupBox1.Parent.Height - groupBox1.Height) / 2) - 10;
            dataGridView1.ReadOnly = true;
            dataGridView1.Enabled = false;
            loadcompany();
            loadgrid();
            for (int i = 0; i < 6; i++)
            {
                if (i!=1)
                {
                    dataGridView1.Columns[i].Width = 170;
                }
                else
                {
                    dataGridView1.Columns[i].Width = 350;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "")
            {
                SqlConnection con =null;
                String query="";
                SqlCommand command1=null;
                try
                {
                    con = Class1.con;
                    con.Open();
                    query = "insert into product values (" + Convert.ToDecimal(textBox2.Text) + ",'" + textBox4.Text + "'," + Convert.ToDecimal(textBox5.Text) + "," + Convert.ToDecimal(textBox6.Text) + "," + Convert.ToDecimal(textBox7.Text) + "," + Convert.ToDecimal(textBox8.Text) + ",'" + textBox1.Text + "','"+comboBox1.Text+"')";
                    //MessageBox.Show(query);
                    command1 = new SqlCommand(query, con);
                    SqlCommand cmd2 = new SqlCommand("insert into stock values("+Convert.ToDecimal(textBox2.Text)+",'"+textBox4.Text+"',"+0+",'"+comboBox1.Text+"')",con);
                    command1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    if (MessageBox.Show("Added..", "SUCCESS") == System.Windows.Forms.DialogResult.OK)
                    {
                        textBox1.Text = textBox2.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
                        loadgrid();
                    }
                    
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Some Data Missing","Warning..!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
