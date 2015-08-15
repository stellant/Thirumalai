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
    public partial class neworder : Form
    {
        public neworder()
        {
            InitializeComponent();
        }
        private void loadproductname()
        {
            SqlConnection con = Class1.connection();
            con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("select pname from product where csname='"+comboBox1.Text+"'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        comboBox3.Items.Add(dr.GetString(0));
                    }
                    if (comboBox3.Items.Count > 0)
                    {
                        comboBox3.SelectedIndex = 0;
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
        private void loadcompany()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from cs where cstype='Seller'",con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr.GetString(0).ToString());
                }
                dr.Close();
                con.Close();
                loadproduct();
               // loadproductname();
            }
            catch (Exception ex)
            {
                String s = ex.ToString();
                MessageBox.Show(s);
            }
        }
        private void loadproduct()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox2.Text = "";
                comboBox3.Text = "";
                SqlCommand cmd = new SqlCommand("select pid,pname from product where csname='"+comboBox1.Text+"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr.GetDecimal(0).ToString());
                    comboBox3.Items.Add(dr.GetString(1));
                }
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = 0;
                }
                if (comboBox3.Items.Count > 0)
                {
                    comboBox3.SelectedIndex = 0;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                String s = ex.ToString();
                MessageBox.Show(s);
            }
        }
        private void neworder_Load(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = false;
                this.WindowState = FormWindowState.Maximized;
                groupBox1.Left = ((groupBox1.Parent.Width - groupBox1.Width) / 2) - 5;
                groupBox4.Left = ((groupBox4.Parent.Width - groupBox4.Width) / 2) - 5;
                groupBox5.Left = ((groupBox5.Parent.Width - groupBox5.Width) / 2) - 5;
                groupBox6.Left = ((groupBox6.Parent.Width - groupBox6.Width) / 2) - 5;
                loadcompany();
              //  loadproduct();
              //  loadproductname();
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0;
                }
               
                comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = 0;
                }
               
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                label9.Visible = label10.Visible = label11.Visible = label12.Visible = label13.Visible = false;
                dateTimePicker2.Visible = textBox6.Visible = textBox7.Visible = textBox8.Visible = textBox9.Visible = label14.Visible = dateTimePicker3.Visible = false;
                dataGridView1.AllowUserToDeleteRows = true;
                truncatetable();
                updategrid();
                dataGridView1.Columns[0].Width = 310;
                dataGridView1.Columns[1].Width = 310;
                dataGridView1.Columns[2].Width = 310;
                dataGridView1.Columns[3].Width = 310;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                label9.Visible = label10.Visible = label11.Visible = label12.Visible = label13.Visible = true;
                dateTimePicker2.Visible = textBox6.Visible = textBox7.Visible = textBox8.Visible = textBox9.Visible = true;
                label14.Visible = dateTimePicker3.Visible = false;
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == true)
            {
                label9.Visible = label10.Visible = label11.Visible = label12.Visible = label13.Visible = false;
                dateTimePicker2.Visible = textBox6.Visible = textBox7.Visible = textBox8.Visible = textBox9.Visible = false;
                label14.Visible = dateTimePicker3.Visible = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                label9.Visible = label10.Visible = label11.Visible = label12.Visible = label13.Visible = true;
                dateTimePicker2.Visible = textBox6.Visible = textBox7.Visible = textBox8.Visible = textBox9.Visible = true;
                label14.Visible = dateTimePicker3.Visible=false;
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == true)
            {
                label9.Visible = label10.Visible = label11.Visible = label12.Visible = label13.Visible = false;
                dateTimePicker2.Visible = textBox6.Visible = textBox7.Visible = textBox8.Visible = textBox9.Visible = false;
                label14.Visible = dateTimePicker3.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                if (comboBox2.Text != "" && textBox1.Text !="" && comboBox3.Text != "" && textBox3.Text != ""&&textBox5.Text!="")
                {
                    String csname = comboBox1.Text.ToString();
                    decimal oid = Convert.ToDecimal(textBox1.Text.ToString());
                    decimal pid = Convert.ToDecimal(comboBox2.Text.ToString());
                    String pname = comboBox3.Text.ToString();
                    decimal quantity = Convert.ToDecimal(textBox3.Text.ToString());
                    decimal free = Convert.ToDecimal(textBox5.Text.ToString());
                   // MessageBox.Show("insert into temp values('" + csname + "'," + oid + "," + pid + ",'" + pname + "'," + quantity + "," + free + ")");
                    SqlCommand cmd = new SqlCommand("insert into temp values('"+csname+"',"+oid+","+pid+",'"+pname+"',"+quantity+","+free+")", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    textBox3.Text = "";
                    textBox5.Text = "";
                    comboBox2.Focus();
                    updategrid();
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Some Fields are Empty","Warning!!!");
                    textBox3.Text = "";
                    textBox4.Text = "";
                    comboBox2.Focus();
                }
                
            }
            catch (Exception ex)
            {
                con.Close();
                textBox3.Text = "";
                textBox4.Text = "";
                comboBox2.Focus();
                MessageBox.Show(ex.Message);
            }
        }

        private void updategrid()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter("select pid as PRODUCTID,pname as PRODUCTNAME,quantity as QUANTITY,free as FREE from temp", con);
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void truncatetable()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table temp", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                int i = 0;
                SqlCommand cmd = new SqlCommand("select * from temp",con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                }
                dr.Close();
                if ((i == 0) || (textBox4.Text.ToString() == "") || (radioButton1.Checked == false && radioButton2.Checked == false))
                {
                    MessageBox.Show("Items,Amount Fields and Mode of Payment are Empty");
                }
                else
                {
                    if (radioButton1.Checked == true)
                    {
                        if (textBox6.Text == "" && textBox7.Text == "" && textBox8.Text == "" && textBox9.Text == "")
                        {
                            MessageBox.Show("Cheque Details are Empty");
                        }
                        else
                        {
                            try
                            {
                                SqlCommand cmd1 = new SqlCommand("insert into orderrr values('"+comboBox1.Text+"'," + Convert.ToDecimal(textBox1.Text) + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + comboBox1.Text + "'," + Convert.ToDecimal(textBox4.Text) + "," + ((Convert.ToDecimal(textBox4.Text)) - (Convert.ToDecimal(textBox8.Text))) + ",'" + ((dateTimePicker3.Value.ToString())) + "')", con);
                                cmd1.ExecuteNonQuery();
                                SqlCommand cmd2 = new SqlCommand("insert into orderrrtransaction values('" + comboBox1.Text + "'," + Convert.ToDecimal(textBox1.Text) + "," + Convert.ToDecimal("1") + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox9.Text + "'," + Convert.ToDecimal(textBox8.Text) + ",'" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')", con);
                                cmd2.ExecuteNonQuery();
                                SqlCommand cmd4 = new SqlCommand("insert into orderrrdetails select csname,oid,pid,pname,quantity,free from temp",con);
                                cmd4.ExecuteNonQuery();
                                SqlCommand cmd5 = new SqlCommand("update stock set stock.quantity=(stock.quantity+temp.quantity+temp.free) from stock,temp where stock.pid=temp.pid",con);
                                cmd5.ExecuteNonQuery();
                                MessageBox.Show("Purchase Added");
                             }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    else if (radioButton2.Checked == true)
                    {
                            try
                            {
                                //SqlCommand cmd1 = new SqlCommand("insert into orderr values(" + Convert.ToDecimal(textBox1.Text) + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + comboBox1.Text + "'," + Convert.ToDecimal(textBox4.Text) + "," + ((Convert.ToDecimal(textBox4.Text)) - (Convert.ToDecimal(textBox8.Text))) + ",'" + ((dateTimePicker3.Value.ToString("yyyy-MM-dd HH:mm:ss"))) + "')", con);
                                SqlCommand cmd1 = new SqlCommand("insert into orderrr values('"+comboBox1.Text+"'," + Convert.ToDecimal(textBox1.Text) + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + comboBox1.Text + "'," + Convert.ToDecimal(textBox4.Text) + "," + Convert.ToDecimal(textBox4.Text) + ",'" + ((dateTimePicker3.Value.ToString())) + "')", con);
                                cmd1.ExecuteNonQuery();
                                //SqlCommand cmd2 = new SqlCommand("insert into orderrrtransaction values('" + comboBox1.Text + "'," + Convert.ToDecimal(textBox1.Text) + "," + Convert.ToDecimal("1") + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox9.Text + "',0,'" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')", con);
                                //cmd2.ExecuteNonQuery();
                                SqlCommand cmd4 = new SqlCommand("insert into orderrrdetails select csname,oid,pid,pname,quantity,free from temp", con);
                                cmd4.ExecuteNonQuery();
                                SqlCommand cmd5 = new SqlCommand("update stock set stock.quantity=(stock.quantity+temp.quantity+temp.free) from stock,temp where stock.pid=temp.pid", con);
                                cmd5.ExecuteNonQuery();
                                MessageBox.Show("Purchase Added");
                                this.Refresh();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.SelectedIndex = comboBox3.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
       
    }
}
