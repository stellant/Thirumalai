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
    public partial class stockupdate : Form
    {
        public stockupdate()
        {
            InitializeComponent();
        }
        private void loadstock()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select quantity from stock where pid="+Convert.ToDecimal(comboBox2.Text),con);
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    textBox1.Text = dr.GetDecimal(0).ToString();
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
        public void loadproducts()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                SqlCommand cmd = new SqlCommand("select pid,pname from stock where csname='"+comboBox1.Text+"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr.GetDecimal(0).ToString());
                    comboBox3.Items.Add(dr.GetString(1));
                }
                dr.Close();
                con.Close();
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = 0;
                }
                if (comboBox3.Items.Count > 0)
                {
                    comboBox3.SelectedIndex = 0;
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
                SqlCommand cmd = new SqlCommand("select distinct csname from stock",con);
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
                loadproducts();
            }
            catch (Exception ex)
            {
                con.Close();
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
                SqlDataAdapter ada = new SqlDataAdapter("select pid as ProductID,pname as ProductName,quantity as Quantity from stock",con);
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
        private void stockupdate_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            loadcompany();
            loadgrid();
            textBox2.Text = "";
            textBox2.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                loadproducts();
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
                comboBox3.SelectedIndex = comboBox2.SelectedIndex;
                loadstock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.SelectedIndex = comboBox3.SelectedIndex;
                loadstock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    if (MessageBox.Show("Are You Sure To Update", "Warning!!!") == System.Windows.Forms.DialogResult.OK)
                    {
                        SqlCommand cmd = new SqlCommand("update stock set quantity="+(Convert.ToDecimal(textBox1.Text)+Convert.ToDecimal(textBox2.Text))+" where pid='"+Convert.ToDecimal(comboBox2.Text)+"'",con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadgrid();
                        loadstock();
                        textBox2.Text = "";
                        textBox2.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
