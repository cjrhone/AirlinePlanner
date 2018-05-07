using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace AirlinePlanner.Models
{
    public class Category
    {
        private int _id;
        private string _flight_name;
        private string _departure_time;
        private string _departure_city;
        private string _arrival_time;
        private string _arrival_city;
        private string _status;

        public Flight(string flight_name, string departure_time, string departure_city, string arrival_time, string arrival_city, string status, int id = 0)
        {
            _flight_name = flight_name;
            _departure_time = departure_time;
            _departure_city = departure_city;
            _arrival_time = arrival_time;
            _arrival_city = arrival_city;
            _status=status;
            _id = id;
        }
        public override bool Equals(System.Object otherCategory)
        {
            if (!(otherCategory is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherCategory;
                return this.GetId().Equals(newStylist.GetId());
            }
        }
        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public List<Client> GetClients()
        {
          List<Client> allStylistClients = new List<Client> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";

          MySqlParameter StylistId = new MySqlParameter();
          StylistId.ParameterName = "@stylist_id";
          StylistId.Value = this._id;
          cmd.Parameters.Add(StylistId);


          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int clientId = rdr.GetInt32(0);
            string clientName = rdr.GetString(1);
            int clientStylistId = rdr.GetInt32(2);
            Client newClient = new Client(clientName, clientId);
            allStylistClients.Add(newClient);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allStylistClients;
        }
        // public void AddClient(Client client)
        // {
        //   _clients.Add(client);
        // }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int StylistId = rdr.GetInt32(0);
              string StylistName = rdr.GetString(1);
              Stylist newStylist = new Stylist(StylistName, StylistId);
              allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }
        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int StylistId = 0;
            string StylistName = "";

            while(rdr.Read())
            {
              StylistId = rdr.GetInt32(0);
              StylistName = rdr.GetString(1);
            }
            Stylist newStylist = new Stylist(StylistName, StylistId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newStylist;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
