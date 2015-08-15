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
    public partial class salescollection : Form
    {
        public salescollection()
        {
            InitializeComponent();
        }
        private void loadtransactionid()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select top 1 tno from salestransaction where csname='" + comboBox1.Text + "' and bno=" + Convert.ToDecimal(comboBox2.Text) + " order by tno desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox2.Text = (dr.GetDecimal(0) + 1).ToString();
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
        private void loadgrid()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter("select tno as TRANSACTIONID,chequeno as CHEQUENO,amount as AMOUNT,convert(varchar(11),date) as DATE from salestransaction where csname='" + comboBox1.Text + "' and bno=" + Convert.ToDecimal(comboBox2.Text), con);
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadremaining()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select sremain from sales where csname='" + comboBox1.Text + "' and bno=" + Convert.ToDecimal(comboBox2.Text), con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (Convert.ToDouble(dr.GetDecimal(0)) <= 0.00)
                    {
                        textBox3.Text = dr.GetDecimal(0).ToString();
                        MessageBox.Show("No Due to Pay", "Alert!!!");
                    }
                    else
                    {
                        textBox3.Text = dr.GetDecimal(0).ToString();
                    }
                }
                dr.Close();
                con.Close();
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
                SqlCommand cmd = new SqlCommand("select bno from sales where csname='" + comboBox1.Text + "'", con);
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
                SqlCommand cmd = new SqlCommand("select distinct csname from sales", con);
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
        private void salescollection_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            groupBox1.Left = (groupBox1.Parent.Width - groupBox1.Width) / 2;
            groupBox1.Top = ((groupBox1.Parent.Height - groupBox1.Height) / 2) - 10;
            loadcompany();
            radioButton1.Checked=true;
            radioButton2.Checked=false;
            comboBox1.Focus();
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
                loadtransactionid();
                loadgrid();
                loadremaining();
                dataGridView1.Columns[0].Width = 300;
                dataGridView1.Columns[1].Width = 300;
                dataGridView1.Columns[2].Width = 300;
                dataGridView1.Columns[3].Width = 300;
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
                if (Convert.ToDouble(textBox3.Text) <= 0.00)
                {
                    MessageBox.Show("No Due to Pay","Alert!!!");
                }
                else if ((Convert.ToDouble(textBox3.Text) - Convert.ToDouble(textBox8.Text)) < 0.00)
                {
                    MessageBox.Show("Amount Exceeds Due to be Paid", "Alert!!!");
                    textBox8.Text = "";
                    textBox8.Focus();
                }
                else
                {
                    if (radioButton1.Checked == true)
                    {
                        if (textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "")
                        {
                            SqlCommand cmd1 = new SqlCommand("insert into salestransaction values('" + comboBox1.Text + "'," + Convert.ToDecimal(comboBox2.Text) + "," + Convert.ToDecimal(textBox2.Text) + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + textBox6.Text + "','" + textBox7.Text + "'," + Convert.ToDecimal(textBox8.Text) + ")", con);
                            cmd1.ExecuteNonQuery();
                            SqlCommand cmd2 = new SqlCommand("update sales set sremain=" + (Convert.ToDecimal(textBox3.Text) - Convert.ToDecimal(textBox8.Text)) + " where csname='" + comboBox1.Text + "' and bno=" + Convert.ToDecimal(comboBox2.Text), con);
                            cmd2.ExecuteNonQuery();
                            con.Close();
                            textBox6.Text = textBox7.Text = textBox8.Text = "";
                            loadtransactionid();
                            loadproduct();
                            loadremaining();
                            loadgrid();
                        }
                        else
                        {
                            MessageBox.Show("Some Fields are Empty");
                        }
                    }
                    else if (radioButton2.Checked == true)
                    {
                        if (textBox8.Text != "")
                        {
                            SqlCommand cmd1 = new SqlCommand("insert into salestransaction values('" + comboBox1.Text + "'," + Convert.ToDecimal(comboBox2.Text) + "," + Convert.ToDecimal(textBox2.Text) + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','0','0'," + Convert.ToDecimal(textBox8.Text) + ")", con);
                            cmd1.ExecuteNonQuery();
                            SqlCommand cmd2 = new SqlCommand("update sales set sremain=" + (Convert.ToDecimal(textBox3.Text) - Convert.ToDecimal(textBox8.Text)) + " where csname='" + comboBox1.Text + "' and bno=" + Convert.ToDecimal(comboBox2.Text), con);
                            cmd2.ExecuteNonQuery();
                            con.Close();
                            textBox6.Text = textBox7.Text = textBox8.Text = "";
                            loadtransactionid();
                            loadproduct();
                            loadremaining();
                            loadgrid();
                        }
                        else
                        {
                            MessageBox.Show("Some Fields are Empty");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nothing Added");
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
                
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                label9.Enabled=true;
                label10.Enabled=true;
                label11.Enabled=true;
                label12.Enabled=true;
                dateTimePicker2.Enabled=true;
                textBox6.Enabled=true;
                textBox7.Enabled=true;
                textBox8.Enabled=true;
                dateTimePicker2.Focus();
            }
            else if(radioButton2.Checked==true)
            {
                label9.Enabled=false;
                label10.Enabled=false;
                label11.Enabled=false;
                label12.Enabled=true;
                dateTimePicker2.Enabled=false;
                textBox6.Enabled=false;
                textBox7.Enabled=false;
                textBox8.Enabled=true;
                textBox8.Focus();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                label9.Enabled=true;
                label10.Enabled=true;
                label11.Enabled=true;
                label12.Enabled=true;
                dateTimePicker2.Enabled=true;
                textBox6.Enabled=true;
                textBox7.Enabled=true;
                textBox8.Enabled=true;
                dateTimePicker2.Focus();
            }
            else if(radioButton2.Checked==true)
            {
                label9.Enabled=false;
                label10.Enabled=false;
                label11.Enabled=false;
                label12.Enabled=true;
                dateTimePicker2.Enabled=false;
                textBox6.Enabled=false;
                textBox7.Enabled=false;
                textBox8.Enabled=true;
                textBox8.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        }
    }

