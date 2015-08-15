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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet("DataSet1");
                SqlDataAdapter ada = new SqlDataAdapter("select * from salesdetails where csname='"+textBox2.Text+"' and bno=" + Convert.ToDecimal(textBox1.Text), con);
                ada.Fill(dt);
                ds.Tables.Add(dt);
                ReportParameter billno = new ReportParameter("billno", textBox1.Text);
                ReportParameter companyname = new ReportParameter("companyname", textBox2.Text);
                ReportParameter companyaddress = new ReportParameter("companyaddress", textBox3.Text);
                ReportParameter companytinno = new ReportParameter("companytin", textBox4.Text);
                ReportParameter date = new ReportParameter("date", textBox5.Text);
                ReportParameter subvat = new ReportParameter("subvat", textBox6.Text);
                ReportParameter subtotal = new ReportParameter("subtotal", textBox7.Text);
                ReportParameter total = new ReportParameter("total", textBox8.Text);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { companyname, companyaddress, companytinno, billno, date,subvat,subtotal,total });
                this.reportViewer1.RefreshReport();
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
