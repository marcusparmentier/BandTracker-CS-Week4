using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
  public class Venue
  {
    private string _venueName;
    private int _id;


    public Venue(string venueName, int id = 0)
    {
      _venueName = venueName;
      _id = id;
    }

    public override bool Equals(System.Object otherVenue)
    {
    if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool venueNameEquality = (this.GetVenueName() == newVenue.GetVenueName());
        return (idEquality && venueNameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetVenueName().GetHashCode();
    }

    public string GetVenueName()
    {
      return _venueName;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueVenueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueVenueName, venueId);
        allVenues.Add(newVenue);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allVenues;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues (venue_name) VALUES (@venueName);";

      MySqlParameter venueName = new MySqlParameter();
      venueName.ParameterName = "@venueName";
      venueName.Value = this._venueName;
      cmd.Parameters.Add(venueName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    
  }
}
