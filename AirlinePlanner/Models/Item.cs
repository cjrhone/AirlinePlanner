using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace AirlinePlanner.Models
{
  public class Item
  {
    private int _city_id;
    private string _city_name;

    public Item (string city_name, int id=0)
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

    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> {};
      MySqlConnection conn = DB.Connection();

      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"SELECT * FROM cities;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())

      {

        int cityId = rdr.GetInt32(0);
        string cityName = rdr.GetString(1);

        Item newItem = new Item(cityName, cityId);

        allItems.Add(newItem);

      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }

    public override bool Equals(System.Object otherCity)
      {
        if (!(otherCity is Item))
        {
          return false;
        }
        else
        {
          Item newCity = (Item) otherCity;
          bool idEquality = (this.GetCityId() == newCity.GetCityId());
          //when we change an object from one type to another, its called "TYPE CASTING"
          bool nameEquality = (this.GetCityName() == newCity.GetCityName());
          return (idEquality && nameEquality);
        }
      }




  }
}
