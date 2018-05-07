using System;
using MySql.Data.MySqlClient;
using AirlinePlanner;

namespace AirlinePlanner.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        //uses the connection string we just definied in Startup.cs
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
        //ultimately, this is connecting to and calling our database
    }
}
