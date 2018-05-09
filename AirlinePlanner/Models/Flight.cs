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
        private int _departure_city_id;
        private string _arrival_time;
        private int _arrival_city_id;
        private string _status;

        public Flight(string flight_name, string departure_time, int departure_city_id, string arrival_time, int arrival_city_id, string status, int id = 0)
        {
            _flight_name = flight_name;
            _departure_time = departure_time;
            _departure_city_id=departure_city_id;
            _arrival_time = arrival_time;
            _arrival_city_id=arrival_city_id;
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
        public string GetDepartureTime()
        {
            return _departure_time;
        }

        public string GetArrivalTime()
        {
            return _arrival_time;
        }

        public string GetStatus()
        {
          return _status;
        }

        public int GetId()
        {
            return _id;
        }

        public int GetDepartureCityId()
        {
            return _departure_city_id;
        }

        public int GetArrivalCityId()
        {
            return _arrival_city_id;
        }
        public List<City> GetCities()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT cities.* FROM flights
              JOIN cities_flights ON (flights.flight_id = cities_flights.flight_id)
              JOIN cities ON (cities_flights.city_id = cities.city_id)
              WHERE flights.flight_id = @FlightId;";

          MySqlParameter flightIdParameter = new MySqlParameter();
          flightIdParameter.ParameterName = "@FlightId";
          flightIdParameter.Value = _id;
          cmd.Parameters.Add(flightIdParameter);

          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
          List<City> cities = new List<City>{};

          while(rdr.Read())
          {
            string flight_name = rdr.GetString(1);
            int id = rdr.GetInt32(0);
            City newCity = new City(flight_name, id);
          }
          conn.Close();
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
            cmd.CommandText = @"INSERT INTO flights (flight_name, departure_time, departure_city_id, arrival_time, arrival_city_id, status) VALUES (@flightName, @departureTime, @departureCityID, @arrivalTime, @arrivalCityID, @status);";

            cmd.Parameters.Add(new MySqlParameter("@flightName", _flight_name));
            cmd.Parameters.Add(new MySqlParameter("@departureTime", _departure_time));
            cmd.Parameters.Add(new MySqlParameter("@departureCityID", _departure_city_id));
            cmd.Parameters.Add(new MySqlParameter("@arrivalTime", _arrival_time));
            cmd.Parameters.Add(new MySqlParameter("@arrivalCityID", _arrival_city_id));
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
              int departureCityId = rdr.GetInt32(3);
              string arrivalTime = rdr.GetString(4);
              int arrivalCityId = rdr.GetInt32(5);
              string status = rdr.GetString(6);

              Flight newFlight = new Flight(flightName, departureTime, departureCityId, arrivalTime, arrivalCityId, status, flightId);
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
            cmd.CommandText = @"SELECT * FROM flights WHERE flight_id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int flightId = 0;
            string flightName = "";
            string departureTime = "";
            int departureCityId=0;
            string arrivalTime="";
            int arrivalCityId=0;
            string status="";

            while(rdr.Read())
            {
              flightId = rdr.GetInt32(0);
              flightName = rdr.GetString(1);
              departureTime = rdr.GetString(2);
              departureCityId = rdr.GetInt32(3);
              arrivalTime = rdr.GetString(4);
              arrivalCityId = rdr.GetInt32(5);
              status = rdr.GetString(6);
            }
            Flight newFlight = new Flight(flightName, departureTime, departureCityId, arrivalTime, arrivalCityId, status, flightId);
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
