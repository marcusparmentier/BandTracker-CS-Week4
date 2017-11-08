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

    public static Venue Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `venues` WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int venueId = 0;
      string venueName = "";

      while (rdr.Read())
      {
        venueId = rdr.GetInt32(0);
        venueName = rdr.GetString(1);
      }

      Venue newVenue= new Venue(venueName, venueId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newVenue;
    }

    public void UpdateVenueName(string newVenueName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET venue_name = @newVenueName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter venueName = new MySqlParameter();
      venueName.ParameterName = "@newVenueName";
      venueName.Value = newVenueName;
      cmd.Parameters.Add(venueName);

      cmd.ExecuteNonQuery();
      _venueName = newVenueName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddBand(Band newBand)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues_bands (venue_id, band_id) VALUES (@VenueId, @BandId);";

      MySqlParameter author_id = new MySqlParameter();
      author_id.ParameterName = "@VenueId";
      author_id.Value = _id;
      cmd.Parameters.Add(author_id);

      MySqlParameter book_id = new MySqlParameter();
      book_id.ParameterName = "@BandId";
      book_id.Value = newBand.GetId();
      cmd.Parameters.Add(book_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Band> GetBands()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT band_id FROM venues_bands WHERE venue_id = @venueId;";

      MySqlParameter venueIdParameter = new MySqlParameter();
      venueIdParameter.ParameterName = "@venueId";
      venueIdParameter.Value = _id;
      cmd.Parameters.Add(venueIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<int> bandIds = new List<int> {};
      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        bandIds.Add(bandId);
      }
      rdr.Dispose();

      List<Band> bands = new List<Band> {};
      foreach (int bandId in bandIds)
      {
        var bandQuery = conn.CreateCommand() as MySqlCommand;
        bandQuery.CommandText = @"SELECT * FROM bands WHERE id = @BandId;";

        MySqlParameter bandIdParameter = new MySqlParameter();
        bandIdParameter.ParameterName = "@BandId";
        bandIdParameter.Value = bandId;
        bandQuery.Parameters.Add(bandIdParameter);

        var bandQueryRdr = bandQuery.ExecuteReader() as MySqlDataReader;
        while(bandQueryRdr.Read())
        {
          int thisBandId = bandQueryRdr.GetInt32(0);
          string bandBandName = bandQueryRdr.GetString(1);
          string bandGenre = bandQueryRdr.GetString(2);
          Band foundBand = new Band(bandBandName, bandGenre, thisBandId);
          bands.Add(foundBand);
        }
        bandQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return bands;
    }
  }
}
