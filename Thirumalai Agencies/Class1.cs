using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Thirumalai_Agencies
{
    class Class1
    {
        public static SqlConnection con = new SqlConnection();
        public static SqlConnection connection()
        {
            con.ConnectionString = "Data Source=THANGA-PC\\SQLEXPRESSS;Initial Catalog=thirumalai;Integrated Security=True";
            return con;
        }
        public static void clean()
        {
            con = null;    
        }
    }

}
