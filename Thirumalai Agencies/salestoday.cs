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
    public partial class salestoday : Form
    {
        public salestoday()
        {
            InitializeComponent();
        }
        private void loadgrid()
        {
            SqlConnection con = Class1.connection();
            con.Close();
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd1 = new SqlCommand("create view view2 as select salesdetails.csname,salesdetails.bno,salesdetails.pid,salesdetails.pname,salesdetails.quantity,salesdetails.free,sales.date from salesdetails,sales where salesdetails.csname=sales.csname and salesdetails.bno=sales.bno", con);
                cmd1.ExecuteNonQuery();
                String date=dateTimePicker1.Value.AddDays(1).ToShortDateString();
                SqlDataAdapter ada = new SqlDataAdapter("select pid as ProductID,pname as ProductName,sum(quantity) as TotalQuantity,sum(convert(numeric(18,0),free)) as TotalFree from view2 where date >= '"+dateTimePicker1.Value.ToShortDateString()+"' and date < '"+date+"' group by pid,pname ",con);
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                SqlCommand cmd2 = new SqlCommand("drop view view2", con);
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                SqlCommand cmd2 = new SqlCommand("drop view view2", con);
                cmd2.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void salestoday_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            loadgrid();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
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
    }
}
