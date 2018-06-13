using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data
{
    public class ConnString
    {
        public SqlConnection Value { get; set; }

        public ConnString(ConnectionType ct)
        {
            switch (ct)
            {
                case ConnectionType.Remote: Value = new SqlConnection(@"Data Source=mssql.fhict.local;Initial Catalog=dbi393093;Persist Security Info=True;User ID=dbi393093;Password=welkom"); break;
                case ConnectionType.Local: Value = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lars\source\repos\GoalTracker\Data\localDB.mdf;Integrated Security=True;Connect Timeout=30"); break;
            }
        }
    }
}
