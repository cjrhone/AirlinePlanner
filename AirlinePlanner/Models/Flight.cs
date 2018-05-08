using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace AirlinePlanner.Models
{
    public class Flight
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
        public override bool Equals(System.Object otherFlight)
        {
            if (!(otherFlight is Flight))
            {
                return false;
            }
            else
            {
                Flight newFlight = (Flight) otherFlight;
                return this.GetId().Equals(newFlight.GetId());
            }
        }
        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string GetFlightName()
        {
            return _flight_name;
        }
        public int GetId()
        {
            return _id;
        }
        public List<City> GetCities()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlConnection cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT cities.* FROM flights
              JOIN flights_cities ON (flights.id = flights_cities_flights_id)
              JOIN cities ON (flights_cities.cities_id = cities.id)
              WHERE flights.id = @FlightId;";

          MySqlParameter flightIdParameter = new MySqlCommand();
          flightIdParameter.ParameterName = "@FlightId";
          flightIdParameter.Value = _id;
          cmd.Parameters.Add(flightIdParameter);

          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
          List<City> cities = new List<City>{};

          while(rdr.Read())
          {
            int cityId = rdr.GetInt32(0);
            string cityName = rdr.GetString(1);
            City newCity = new City(cityName, cityId);
            cities.Add(newCity);
          }
          conn.Close()
          if (conn != null)
          {
            conn.Dispose();
          }
          return cities;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO flights (flight_name, departure_time, departure_city, arrival_time, arrival_city, status) VALUES (@flightName, @departureTime, @departureCity, @arrivalTime, @arrivalCity, @status);";

            cmd.Parameters.Add(new MySqlParameter("@flightName", _flight_name));
            cmd.Parameters.Add(new MySqlParameter("@departureTime", _departure_time));
            cmd.Parameters.Add(new MySqlParameter("@departureCity", _departure_city));
            cmd.Parameters.Add(new MySqlParameter("@arrivalTime", _arrival_time));
            cmd.Parameters.Add(new MySqlParameter("@arrivalCity", _arrival_city));
            cmd.Parameters.Add(new MySqlParameter("@status", _status));


            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public static List<Flight> GetAll()
        {
            List<Flight> allFlights = new List<Flight> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM flights;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int flightId = rdr.GetInt32(0);
              string flightName = rdr.GetString(1);
              string departureTime = rdr.GetString(2);
              string departureCity = rdr.GetString(3);
              string arrivalTime = rdr.GetString(4);
              string arrivalCity = rdr.GetString(5);
              string status = rdr.GetString(6);

              Flight newFlight = new Flight(flightName, departureTime, departureCity, arrivalTime, arrivalCity, status, flightId);
              allFlights.Add(newFlight);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allFlights;
        }
        public static Flight Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM flights WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int flightId = 0;
            string flightName = "";
            string departureTime = "";
            string departureCity = "";
            string arrivalTime="";
            string arrivalCity="";
            string status="";

            while(rdr.Read())
            {
              flightId = rdr.GetInt32(0);
              flightName = rdr.GetString(1);
              departureTime = rdr.GetString(2);
              departureCity = rdr.GetString(3);
              arrivalTime = rdr.GetString(4);
              arrivalCity = rdr.GetString(5);
              status = rdr.GetString(6);
            }
            Flight newFlight = new Flight(flightName, departureTime, departureCity, arrivalTime, arrivalCity, status, flightId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newFlight;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM flights;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
