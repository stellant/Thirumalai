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
    public partial class stockview : Form
    {
        public stockview()
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
                SqlDataAdapter ada = new SqlDataAdapter("select pid as ProductID,pname as ProductName,quantity as Quantity from stock where csname='"+comboBox1.Text+"'",con);
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
        private void loadcompany()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select distinct csname from stock", con);
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
                loadgrid();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void stockview_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            loadcompany();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadgrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Select Customer Name Before Printing Report");
                }
                else
                {
                    Form3 salesrepor = new Form3();
                    salesrepor.textBox1.Text = this.comboBox1.Text;
                    salesrepor.textBox2.Text = this.dateTimePicker1.Value.ToShortDateString();
                    salesrepor.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
