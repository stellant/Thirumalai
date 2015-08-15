using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace Thirumalai_Agencies
{
    public partial class salesbill : Form
    {
        private String pid = "";
        private String pname = "";
        public salesbill()
        {
            InitializeComponent();
            dataGridView1.UserDeletingRow += dataGridView1_UserDeletingRow;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }
        private void companyload()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select csname from cs where cstype='Customer'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr.GetString(0));
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Customer Details Found...", "Warning...");
                con.Close();
            }
        }
        private void pidload()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select pid from product", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr.GetDecimal(0).ToString());
                }
                // comboBox2.SelectedIndex = 0;       
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                comboBox2.Focus();
                con.Close();
            }
        }
        private void pnameload()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select pname from product", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox3.Items.Add(dr.GetString(0));
                }
                // comboBox2.SelectedIndex = 0;       
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                comboBox3.Focus();
                con.Close();
            }
        }
        private void billnoload()
        {
            try
            {
                SqlConnection con = Class1.connection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select top 1 bno from sales order by bno desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = (dr.GetDecimal(0) + 1).ToString();
                }
                else
                {
                    textBox1.Text = "1";
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void salesbill_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            button4.Enabled = false;
            billnoload(); //load bill no
            companyload();  //load company detail into combobox1
            pnameload();//load name detail into combobox3
            pidload();//load pid detail into combobox2
            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }
            comboBox2.Focus();
            dataGridView1.Columns.Add("pid", "Product ID");
            dataGridView1.Columns.Add("pname", "Product Name");
            dataGridView1.Columns.Add("sprice", "Selling Price");
            dataGridView1.Columns.Add("vat", "VAT");
            dataGridView1.Columns.Add("discount", "Discount");
            dataGridView1.Columns.Add("free", "Free");
            dataGridView1.Columns.Add("quantity", "Quantity");
            dataGridView1.Columns.Add("tprice", "Price");
            dataGridView1.Columns.Add("tvat", "VAT Price");
            dataGridView1.Columns.Add("currentstock", "Current Stock");
            calculate();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            dateTimePicker2.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select product.pid,product.pname,product.sprice,product.pvat,product.discount,product.free,stock.quantity from product,stock where product.pid=stock.pid", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetDecimal(0).ToString() == comboBox2.Text)
                    {
                        comboBox3.SelectedIndex = comboBox2.SelectedIndex;
                        textBox3.Text = dr.GetDecimal(2).ToString();
                        textBox4.Text = dr.GetDecimal(3).ToString();
                        textBox5.Text = dr.GetDecimal(4).ToString();
                        textBox6.Text = dr.GetString(5);
                        label1.Text = dr.GetDecimal(6).ToString();
                        if (Convert.ToDecimal(label1.Text) > 0)
                        {
                            label1.ForeColor = Color.Green;
                        }
                        else
                        {
                            label1.ForeColor = Color.Red;
                        }
                        break;
                    }
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Products Found", "Warning...");
                comboBox2.Focus();
                con.Close();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.SelectedIndex = comboBox3.SelectedIndex;
            }
            catch (Exception ex)
            {

            }
        }
        private void loadgrid(int i)
        {
            try
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Quantity Field is Empty");
                    textBox2.Text = "";
                    textBox2.Focus();
                }
                else if (Convert.ToDecimal(textBox2.Text) > Convert.ToDecimal(label1.Text))
                {
                    MessageBox.Show("Quantity is Greater than Stock");
                    textBox2.Text = "";
                    textBox2.Focus();
                }
                else if (Convert.ToDecimal(textBox2.Text) < 0)
                {
                    MessageBox.Show("Quantity Should Not Be Negative");
                    textBox2.Text = "";
                    textBox2.Focus();
                }
                else if (Convert.ToDecimal(textBox2.Text) == 0)
                {
                    MessageBox.Show("Quantity Should Not Be 0");
                    textBox2.Text = "";
                    textBox2.Focus();
                }
                else
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["pid"].Value = comboBox2.Text;
                    dataGridView1.Rows[i].Cells["pname"].Value = comboBox3.Text;
                    dataGridView1.Rows[i].Cells["sprice"].Value = textBox3.Text;
                    dataGridView1.Rows[i].Cells["vat"].Value = textBox4.Text;
                    dataGridView1.Rows[i].Cells["discount"].Value = textBox5.Text;
                    dataGridView1.Rows[i].Cells["free"].Value = textBox6.Text;
                    dataGridView1.Rows[i].Cells["quantity"].Value = textBox2.Text;
                    decimal sprice = Convert.ToDecimal(dataGridView1.Rows[i].Cells["sprice"].Value);
                    decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["quantity"].Value);
                    decimal subprice = sprice * quantity;
                    decimal vat = Convert.ToDecimal(dataGridView1.Rows[i].Cells["vat"].Value);
                    decimal discount = ((Convert.ToDecimal(dataGridView1.Rows[i].Cells["discount"].Value))/100)*subprice;
                    dataGridView1.Rows[i].Cells["tprice"].Value = subprice-discount;
                    decimal tprice = Convert.ToDecimal(dataGridView1.Rows[i].Cells["tprice"].Value);
                    dataGridView1.Rows[i].Cells["tvat"].Value = ((vat / 100) * tprice);
                    dataGridView1.Rows[i].Cells["currentstock"].Value = Convert.ToDecimal(label1.Text);
                    if (comboBox2.Items.Count <= 1)
                    {
                        comboBox2.Items.Remove(comboBox2.Text);
                        comboBox3.Items.Remove(comboBox3.Text);
                        comboBox2.Text = comboBox3.Text = "";
                        textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = label1.Text = "";
                        comboBox2.Focus();
                        button1.Enabled = false;
                    }
                    else
                    {
                        comboBox2.Items.Remove(comboBox2.Text);
                        comboBox3.Items.Remove(comboBox3.Text);
                        comboBox2.SelectedIndex = comboBox3.SelectedIndex = 0;
                        textBox2.Text = "";
                        comboBox2.Focus();
                        button1.Enabled = true;
                    }
                }
                calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void calculate()
        {
            try
            {
                int i = dataGridView1.Rows.Count;
                decimal vat = 0;
                decimal price = 0;
                for (int j = 0; j < i; j++)
                {
                    vat = vat + (Convert.ToDecimal(dataGridView1.Rows[j].Cells[8].Value));
                    price = price + (Convert.ToDecimal(dataGridView1.Rows[j].Cells[7].Value));
                }
                textBox7.Text = "" + Math.Round(vat, 2);
                textBox8.Text = "" + Math.Round(price, 2);
                textBox9.Text = "" + Math.Round((vat + price), 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadgrid((dataGridView1.Rows.Count) - 1);
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                pid = "" + dataGridView1.SelectedRows[0].Cells[0].Value;
                pname = "" + dataGridView1.SelectedRows[0].Cells[1].Value;
            }
            catch (Exception ex)
            {

            }
        }
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            comboBox2.Items.Add(pid);
            comboBox3.Items.Add(pname);
            textBox2.Text = "";
            button1.Enabled = true;
            textBox7.Text = textBox8.Text = textBox9.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                comboBox2.Items.Add(pid);
                comboBox3.Items.Add(pname);
                textBox2.Text = "";
                button1.Enabled = true;
                textBox7.Text = textBox8.Text = textBox9.Text = "0";
                calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Rows To Delete", "Warning...");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void insertsales(String sa)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(sa, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
        private void insertsalesdetails()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                if (dataGridView1.Rows.Count > 1)
                {
                    int i = dataGridView1.Rows.Count - 1;
                    for (int j = 0; j < i; j++)
                    {
                        String c = "insert into salesdetails values('"+comboBox1.Text+"'," + Convert.ToDecimal(textBox1.Text) + "," + Convert.ToDecimal(dataGridView1.Rows[j].Cells[0].Value.ToString()) + ",'" + dataGridView1.Rows[j].Cells[1].Value.ToString() + "'," + Convert.ToDecimal(dataGridView1.Rows[j].Cells[2].Value.ToString()) + "," + Convert.ToDecimal(dataGridView1.Rows[j].Cells[3].Value.ToString()) + "," + Convert.ToDecimal(dataGridView1.Rows[j].Cells[4].Value.ToString()) + ",'" + dataGridView1.Rows[j].Cells[5].Value.ToString() + "'," + Convert.ToDecimal(dataGridView1.Rows[j].Cells[6].Value.ToString()) + "," + Convert.ToDecimal(dataGridView1.Rows[j].Cells[7].Value.ToString()) + "," + Convert.ToDecimal(dataGridView1.Rows[j].Cells[8].Value.ToString()) + "," +(j+1)+")";
                        SqlCommand cmd = new SqlCommand(c, con);
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
        private void updatestock()
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                if (dataGridView1.Rows.Count > 1)
                {
                    int i = dataGridView1.Rows.Count - 1;
                    for (int j = 0; j < i; j++)
                    {
                        decimal currentstock = Convert.ToDecimal(dataGridView1.Rows[j].Cells[9].Value.ToString());
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[j].Cells[6].Value.ToString());
                        decimal free = Convert.ToDecimal(dataGridView1.Rows[j].Cells[5].Value.ToString());
                        String c = "update stock set quantity=" + ((currentstock)-(quantity+free)) + " where pid=" + Convert.ToDecimal(dataGridView1.Rows[j].Cells[0].Value.ToString());
                        //MessageBox.Show(c);
                        SqlCommand cmd = new SqlCommand(c, con);
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
        private void salestransaction(String s)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(s,con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Select Customer Name Before Printing Report");
            }
            else
            {
                try
                {
                    if (radioButton1.Checked == false && radioButton2.Checked == false)
                    {
                        MessageBox.Show("Select Mode of Payment Before Save");
                    }
                    else if (dataGridView1.Rows.Count > 1)
                    {
                        String s = "";
                        String sa = "";
                        if (radioButton1.Checked == true && radioButton3.Checked == false && radioButton4.Checked == false)
                        {
                            MessageBox.Show("Select Any Payment Method");
                        }
                        else if (radioButton1.Checked == true && radioButton4.Checked == true)
                        {
                            sa="insert into sales values('" + comboBox1.Text + "'," + Convert.ToDecimal(textBox1.Text) + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Convert.ToDecimal(textBox7.Text) + "," + Convert.ToDecimal(textBox8.Text) + "," + Convert.ToDecimal(textBox9.Text) + ","+(Convert.ToDecimal(textBox9.Text)-Convert.ToDecimal(textBox10.Text))+")";
                            insertsales(sa);
                            insertsalesdetails();
                            updatestock();
                            s = "insert into salestransaction values('"+comboBox1.Text+"'," + Convert.ToDecimal(textBox1.Text) + ",1,'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','0','0'," + Convert.ToDecimal(textBox10.Text) + ")";
                            salestransaction(s);
                            button4.Enabled = true;
                            button3.Enabled = false;
                            MessageBox.Show("Immediate Sales with Cash Confirmed");
                        }
                        else if (radioButton1.Checked == true && radioButton3.Checked == true)
                        {
                            sa = "insert into sales values('" + comboBox1.Text + "'," + Convert.ToDecimal(textBox1.Text) + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Convert.ToDecimal(textBox7.Text) + "," + Convert.ToDecimal(textBox8.Text) + "," + Convert.ToDecimal(textBox9.Text) + "," + (Convert.ToDecimal(textBox9.Text) - Convert.ToDecimal(textBox10.Text)) + ")";
                            insertsales(sa);
                            insertsalesdetails();
                            updatestock();
                            s = "insert into salestransaction values('"+comboBox1.Text+"'," + Convert.ToDecimal(textBox1.Text) + ",1,'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + textBox12.Text + "','" + textBox11.Text + "'," + Convert.ToDecimal(textBox10.Text) + ")";
                            salestransaction(s);
                            button4.Enabled = true;
                            button3.Enabled = false;
                            MessageBox.Show("Immediate Sales with Cheque Confirmed");
                        }
                        else if (radioButton2.Checked == true)
                        {
                            sa = "insert into sales values('" + comboBox1.Text + "'," + Convert.ToDecimal(textBox1.Text) + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Convert.ToDecimal(textBox7.Text) + "," + Convert.ToDecimal(textBox8.Text) + "," + Convert.ToDecimal(textBox9.Text) + "," +Convert.ToDecimal(textBox9.Text) + ")";
                            insertsales(sa);
                            insertsalesdetails();
                            updatestock();
                            s = "insert into salestransaction values('"+comboBox1.Text+"'," + Convert.ToDecimal(textBox1.Text) + ",1,'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','0','0',0)";
                            salestransaction(s);
                            button4.Enabled = true;
                            button3.Enabled = false;
                            MessageBox.Show("Credit Sales Confirmed");
                        }

                    }
                    else
                    {
                        MessageBox.Show("No Products Added");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                dateTimePicker2.Visible = true;
                textBox10.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                label16.Enabled = false;
                label17.Enabled = false;
                label18.Enabled = false;
                label19.Enabled = false;
                dateTimePicker2.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;


            }
            else if(radioButton1.Checked==false&&radioButton2.Checked==true)
            {
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label18.Visible = false;
                label19.Visible = false;
                dateTimePicker2.Visible = false;
                textBox10.Visible = false;
                textBox11.Visible = false;
                textBox12.Visible = false;
                label16.Enabled = false;
                label17.Enabled = false;
                label18.Enabled = false;
                label19.Enabled = false;
                dateTimePicker2.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                dateTimePicker2.Visible = true;
                textBox10.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                label16.Enabled = false;
                label17.Enabled = false;
                label18.Enabled = false;
                label19.Enabled = false;
                dateTimePicker2.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;

            }
            else if (radioButton1.Checked == false && radioButton2.Checked == true)
            {
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label18.Visible = false;
                label19.Visible = false;
                dateTimePicker2.Visible = false;
                textBox10.Visible = false;
                textBox11.Visible = false;
                textBox12.Visible = false;
                label16.Enabled = false;
                label17.Enabled = false;
                label18.Enabled = false;
                label19.Enabled = false;
                dateTimePicker2.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true && radioButton3.Checked == false)
            {
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                dateTimePicker2.Visible = true;
                textBox10.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                label16.Enabled = true;
                label17.Enabled = false;
                label18.Enabled = false;
                label19.Enabled = false;
                dateTimePicker2.Enabled = false;
                textBox10.Enabled = true;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
             }
            else if (radioButton4.Checked == false && radioButton3.Checked == true)
            {
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                dateTimePicker2.Visible = true;
                textBox10.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                label16.Enabled = true;
                label17.Enabled = true;
                label18.Enabled = true;
                label19.Enabled = true;
                dateTimePicker2.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                textBox12.Enabled = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true && radioButton3.Checked == false)
            {
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                dateTimePicker2.Visible = true;
                textBox10.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                label16.Enabled = true;
                label17.Enabled = false;
                label18.Enabled = false;
                label19.Enabled = false;
                dateTimePicker2.Enabled = false;
                textBox10.Enabled = true;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
            }
            else if (radioButton4.Checked == false && radioButton3.Checked == true)
            {
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                dateTimePicker2.Visible = true;
                textBox10.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                label16.Enabled = true;
                label17.Enabled = true;
                label18.Enabled = true;
                label19.Enabled = true;
                dateTimePicker2.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                textBox12.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Select Customer Name Before Printing Report");
                }
                else
                {
                    Form4 salesrepor = new Form4();
                    salesrepor.textBox1.Text = this.textBox1.Text;
                    salesrepor.textBox2.Text = this.comboBox1.Text;
                    salesrepor.textBox3.Text = this.textBox13.Text;
                    salesrepor.textBox4.Text = this.textBox14.Text;
                    salesrepor.textBox5.Text = this.dateTimePicker1.Value.ToShortDateString();
                    salesrepor.textBox6.Text = this.textBox7.Text;
                    salesrepor.textBox7.Text = this.textBox8.Text;
                    salesrepor.textBox8.Text = this.textBox9.Text;
                    salesrepor.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
           
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select csaddress,cstinno from cs where csname='"+comboBox1.Text+"'",con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox13.Text = dr.GetString(0);
                    textBox14.Text = dr.GetString(1);
                }
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
