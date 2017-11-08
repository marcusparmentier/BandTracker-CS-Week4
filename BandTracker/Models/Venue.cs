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
  }
}
