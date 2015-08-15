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
    public partial class salescomplete : Form
    {
        public salescomplete()
        {
            InitializeComponent();
        }
        private void amount()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select sum(total),sum(sremain) from sales where csname='"+comboBox1.Text+"' and date > = '"+dateTimePicker1.Value.ToShortDateString()+"' and date < '"+dateTimePicker2.Value.ToShortDateString()+"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr.GetDecimal(0).ToString();
                    textBox2.Text = dr.GetDecimal(1).ToString();
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                textBox1.Text = "0.00";
                textBox2.Text = "0.00";
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
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void salescomplete_Load(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = false;
                this.WindowState = FormWindowState.Maximized;
                loadcompany();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadgrid()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd1 = new SqlCommand("create view view1 as select salesdetails.csname,salesdetails.bno,salesdetails.pid,salesdetails.pname,sales.date,salesdetails.quantity,convert(numeric(18,0),salesdetails.free) as free from salesdetails,sales where salesdetails.csname=sales.csname and salesdetails.bno=sales.bno", con);
                cmd1.ExecuteNonQuery();
                SqlDataAdapter ada = new SqlDataAdapter("select pid as ProductID,pname as ProductName,sum(quantity) as Quantity,sum(convert(numeric(18,0),free)) as free from view1 where csname='"+comboBox1.Text+"' and date >= '"+dateTimePicker1.Value.ToShortDateString()+"' and date < '"+dateTimePicker2.Value.ToShortDateString()+"' group by pid,pname order by pid",con);
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                SqlCommand cmd = new SqlCommand("drop view view1", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                SqlCommand cmd = new SqlCommand("drop view view1",con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void loadgrid1()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter("select bno as BillNo,sum(amount) as TotalCollected from salestransaction where csname='"+comboBox1.Text+"' and date >='"+dateTimePicker1.Value.ToShortDateString()+"' and date < '"+dateTimePicker2.Value.ToShortDateString()+"' group by bno;", con);
                ada.Fill(dt);
                dataGridView2.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                amount();
                loadgrid();
                loadgrid1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                amount();
                loadgrid();
                loadgrid1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                amount();
                loadgrid();
                loadgrid1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
