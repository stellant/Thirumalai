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
    public partial class printbill : Form
    {
        public printbill()
        {
            InitializeComponent();
        }
        private void loadgrid()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter("select sno as SNo,pname as Products,sprice as Rate,pvat as VATPercent,tvat as VATPrice,discount as Discount,free as Free,quantity as Quantity,tprice as TotalPrice from salesdetails where csname='"+comboBox1.Text+"' and bno="+Convert.ToDecimal(comboBox2.Text),con);
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void loaddetails()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select date,svat,sprice,total from sales where csname='"+comboBox1.Text+"' and bno="+Convert.ToDecimal(comboBox2.Text),con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dateTimePicker2.Value = dr.GetDateTime(0);
                    textBox3.Text = dr.GetDecimal(1).ToString();
                    textBox2.Text = dr.GetDecimal(2).ToString();
                    textBox1.Text = dr.GetDecimal(3).ToString();
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
        private void loadaddress()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select csaddress,cstinno from cs where csname='"+comboBox1.Text+"'",con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox10.Text = dr.GetString(0).ToString();
                    textBox4.Text = dr.GetString(1).ToString();
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
        private void loadproduct()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select bno from sales where csname='"+comboBox1.Text+"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr.GetDecimal(0).ToString());
                }
                dr.Close();
                con.Close();
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }

        }
        private void loadcompany()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select distinct csname from sales",con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr.GetString(0));
                }
                dr.Close();
                con.Close();
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
                loadproduct();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void printbill_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            loadcompany();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadaddress();
                loadproduct();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loaddetails();
                loadgrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Form4 salesreport = new Form4();
               salesreport.textBox1.Text = this.comboBox2.Text;
               salesreport.textBox2.Text = this.comboBox1.Text;
               salesreport.textBox3.Text = this.textBox10.Text;
               salesreport.textBox4.Text = this.textBox4.Text;
               salesreport.textBox5.Text = this.dateTimePicker2.Value.ToShortDateString();
               salesreport.textBox6.Text = this.textBox3.Text;
               salesreport.textBox7.Text = this.textBox2.Text;
               salesreport.textBox8.Text = this.textBox1.Text;
               salesreport.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
