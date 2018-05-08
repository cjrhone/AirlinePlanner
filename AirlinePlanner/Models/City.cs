using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace AirlinePlanner.Models
{
  public class City
  {
    private int _city_id;
    private string _city_name;

    public City (string city_name, int id=0)
    {
      _city_name = city_name;
      _city_id = id;
    }

    public int GetCityId()
    {
      return _city_id;
    }

    public string GetCityName()
    {
      return _city_name;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities (city_name) VALUES (@city_name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@city_name";
      name.Value = _city_name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _city_id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }

    public static List<City> GetAll()
    {
      List<City> allCitys = new List<City> {};
      MySqlConnection conn = DB.Connection();

      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"SELECT * FROM cities;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())

      {

        int cityId = rdr.GetInt32(0);
        string cityName = rdr.GetString(1);

        City newCity = new City(cityName, cityId);

        allCitys.Add(newCity);

      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCitys;
    }

    public override bool Equals(System.Object otherCity)
      {
        if (!(otherCity is City))
        {
          return false;
        }
        else
        {
          City newCity = (City) otherCity;
          bool idEquality = (this.GetCityId() == newCity.GetCityId());
          //when we change an object from one type to another, its called "TYPE CASTING"
          bool nameEquality = (this.GetCityName() == newCity.GetCityName());
          return (idEquality && nameEquality);
        }
      }

      public static City Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM cities WHERE city_id = @thisId;";
        //@thisId is the placeholder for the ID property of the City we're seeking in the database

        MySqlParameter thisId = new MySqlParameter();
        //Create a MySqlParamter called thisId
        thisId.ParameterName = "@thisId";
        //Define ParameterName property as @thisId to match the SQL command
        thisId.Value = id;
        //Define Value property of thisId as id
        cmd.Parameters.Add(thisId);
        //Adds thisId to Parameters property of cmd

        var rdr = cmd.ExecuteReader() as MySqlDataReader;

        int cityId = 0;
        string cityName = "";
        //defined outside of while loop to ensure we don't hit unanticipated errors ( like not being able to define values)

        while (rdr.Read())
        //To initiate reading the database, we run a while loop
        {
          cityId = rdr.GetInt32(0);
          //corresponds to the index positions
          cityName = rdr.GetString(1);

        }

        City foundCity = new City(cityName, cityId);

          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }

          return foundCity;
      }

      public static void DeleteAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        //Creates conn object representing our connection to the database

        //manually opens the connection ( conn ) with conn.Open()
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM cities;";
        //Define cmd as --> creating command --> MySqlCommand... then...
        cmd.ExecuteNonQuery();
        //...Define CommandText property using SQL statement, which will clear the items table in our database

        //Executes SQL statements that modify data (like deletion)
        conn.Close();
        if (conn != null)
        //Finally, we make sure to close our connection with Close()...
        {
          conn.Dispose();
        }
      }

      public List<Flight> GetFlights()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT flights.* FROM cities
            JOIN cities_flights ON (cities.city_id = cities_flights.city_id)
            JOIN flights ON (cities_flights.flight_id = flights.flight_id)
            WHERE cities.city_id = @CityId;";

        MySqlParameter cityIdParameter = new MySqlParameter();
        cityIdParameter.ParameterName = "@CityId";
        cityIdParameter.Value = _city_id;
        cmd.Parameters.Add(cityIdParameter);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Flight> flights = new List<Flight>{};

        while(rdr.Read())
        {
          string flight_name = rdr.GetString(1);
          string departure_time = rdr.GetString(2);
          int departure_city_id = rdr.GetInt32(3);
          string departure_city = rdr.GetString(4);
          string arrival_time = rdr.GetString(5);
          int arrival_city_id = rdr.GetInt32(6);
          string arrival_city = rdr.GetString(7);
          string status = rdr.GetString(8);
          int id = rdr.GetInt32(0);
          Flight newFlight = new Flight(flight_name, departure_time, departure_city_id, departure_city, arrival_time, arrival_city_id, arrival_city, status, id);
          flights.Add(newFlight);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return flights;
      }
  }
}
