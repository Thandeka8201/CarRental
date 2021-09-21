using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CarRental
{
    public static class SqlServerHelper
    {
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gansievoges\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");
            return Con;
        }
            
    }
}
