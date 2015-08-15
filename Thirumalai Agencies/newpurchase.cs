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
    public partial class newpurchase : Form
    {
        public newpurchase()
        {
            InitializeComponent();
        }
        private void truncatetable()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table purchasetemp",con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadcount()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select quantity from orderdetails where oid="+Convert.ToDecimal(comboBox1.Text)+" and pid="+Convert.ToDecimal(comboBox2.Text), con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox4.Text = dr.GetDecimal(0).ToString();
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadgrid()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter("select pid as PID,pname as PRODUCTNAME, quantity as QUANTITY from purchasetemp",con);
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                dataGridView1.Columns[0].Width = 420;
                dataGridView1.Columns[1].Width = 420;
                dataGridView1.Columns[2].Width = 420;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadorder()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select oid from orderr",con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr.GetDecimal(0).ToString());
                }
                dr.Close();
                con.Close();
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No Orders Found. Add Orders Before Coming");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadproduct()
        {
            try
            {
                comboBox2.Items.Clear();
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from orderdetails where oid="+Convert.ToDecimal(comboBox1.Text),con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr.GetDecimal(1).ToString());
                }
                dr.Close();
                con.Close();
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No orders Found. Please Add Before Coming");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void newpurchase_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            groupBox1.Left = (groupBox1.Parent.Width - groupBox1.Width) / 2;
            loadorder();
            truncatetable();
            loadgrid();
            textBox3.Text = "";
            textBox3.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from orderr where oid="+Convert.ToDecimal(comboBox1.Text),con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dateTimePicker1.Value = dr.GetDateTime(1);
                    textBox1.Text = dr.GetString(2);
                }
                dr.Close();
                con.Close();
                truncatetable();
                loadgrid();
                loadproduct();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadtotalcount()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select sum(quantity) from purchases where oid="+Convert.ToDecimal(comboBox1.Text)+" and pid="+Convert.ToDecimal(comboBox2.Text),con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox5.Text = dr.GetDecimal(0).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select pname from product where pid="+Convert.ToDecimal(comboBox2.Text),con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox2.Text = dr.GetString(0);
                }
                dr.Close();
                con.Close();
                loadcount();
                loadtotalcount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(((Convert.ToDecimal(textBox3.Text)+(Convert.ToDecimal(textBox5.Text)))>(Convert.ToDecimal(textBox4.Text))))
            {
                MessageBox.Show("Purchase Exceeds Order Placed");
                textBox3.Text = "";
                textBox3.Focus();
            }
            else
            {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                long i = 0;
               
                SqlCommand cmd1 = new SqlCommand("select quantity from stock where pid="+Convert.ToDecimal(comboBox2.Text),con);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    i = Convert.ToInt64(dr.GetDecimal(0));
                }
                dr.Close();
                i = i + Convert.ToInt64(textBox3.Text);
                SqlCommand cmd2 = new SqlCommand("insert into purchasetemp values(" + Convert.ToDecimal(comboBox1.Text) + "," + Convert.ToDecimal(comboBox2.Text) + ",'" + textBox2.Text + "'," + Convert.ToDecimal(textBox3.Text) + "," + Convert.ToDecimal(i) + ",'" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')", con);
                cmd2.ExecuteNonQuery();
                con.Close();
                loadgrid();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("insert into purchases select oid,pid,quantity,date from purchasetemp", con);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("update stock set quantity=purchasetemp.tquantity from stock,purchasetemp where stock.pid=purchasetemp.pid", con);
                cmd2.ExecuteNonQuery();
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
