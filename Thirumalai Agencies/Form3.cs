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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SqlConnection con = Class1.connection();
            con.Open();
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet("DataSet1");
                SqlDataAdapter ada = new SqlDataAdapter("select * from stock where csname='" + textBox1.Text + "'", con);
                ada.Fill(dt);
                ds.Tables.Add(dt);
                ReportParameter csname  = new ReportParameter("csname", textBox1.Text);
                ReportParameter date = new ReportParameter("date", textBox2.Text);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { csname,date });
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
