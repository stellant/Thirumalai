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
    public partial class vieworders : Form
    {
        public vieworders()
        {
            InitializeComponent();
        }
        private void loadorderid()
        {
            try
            {
                SqlConnection con=Class1.connection();
                con.Open();
                SqlCommand cmd=new SqlCommand("select oid from orderr",con);
                SqlDataReader dr=cmd.ExecuteReader();
                while(dr.Read())
                {
                    comboBox1.Items.Add(dr.GetDecimal(0).ToString());
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loaddetails()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select odate,otamount,oremain from orderrr where csname='"+comboBox1.Text+"' and oid="+Convert.ToDecimal(comboBox2.Text),con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dateTimePicker3.Value = dr.GetDateTime(0);
                    textBox1.Text = dr.GetDecimal(1).ToString();
                    textBox2.Text = dr.GetDecimal(2).ToString();
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadgrid1()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter("select tid as TID,chequeno as CHEQUENO,amount as AMOUNT,convert(varchar(12),date) as DATE from orderrrtransaction where csname='" + comboBox1.Text + "' and oid=" + comboBox2.Text, con);
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                dataGridView1.Columns[0].Width = 300;
                dataGridView1.Columns[1].Width = 300;
                dataGridView1.Columns[2].Width = 300;
                dataGridView1.Columns[3].Width = 400;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadgrid2()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter("select pid as PID, pname as PRODUCTNAME,quantity as QUANTITY,free as FREE from orderrrdetails where csname='" + comboBox1.Text + "' and oid=" + comboBox2.Text, con);
                ada.Fill(dt);
                dataGridView2.DataSource = dt;
                con.Close();
                dataGridView2.Columns[0].Width = 300;
                dataGridView2.Columns[1].Width = 300;
                dataGridView2.Columns[2].Width = 300;
                dataGridView2.Columns[3].Width = 400;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadproduct()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                comboBox2.Items.Clear();
                SqlCommand cmd = new SqlCommand("select oid from orderrr where csname='" + comboBox1.Text + "'", con);
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
                comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;

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
                SqlCommand cmd = new SqlCommand("select distinct csname from orderrr", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr.GetString(0).ToString());
                }
                dr.Close();
                con.Close();
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                loadproduct();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void vieworders_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            groupBox1.Left = ((groupBox1.Parent.Width - groupBox1.Width) / 2) - 5;
            loadcompany();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadproduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loaddetails();
                loadgrid1();
                loadgrid2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
