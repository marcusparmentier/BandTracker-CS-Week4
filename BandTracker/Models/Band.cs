using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
  public class Band
  {
    private string _bandName;
    private string _genre;
    private int _id;

    public Band(string bandName, string genre, int id = 0)
    {
      _bandName = bandName;
      _genre = genre;
      _id = id;
    }

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool bandNameEquality = (this.GetBandName() == newBand.GetBandName());
        bool genreEquality = (this.GetGenre() == newBand.GetGenre());
        return (idEquality && bandNameEquality && genreEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }
    public string GetBandName()
    {
      return _bandName;
    }
    public string GetGenre()
    {
      return _genre;
    }
    public int GetId()
    {
      return _id;
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int BandId = rdr.GetInt32(0);
        string BandBandName = rdr.GetString(1);
        string BandGenre = rdr.GetString(2);
        Band newBand = new Band(BandBandName, BandGenre, BandId);
        allBands.Add(newBand);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBands;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands;";
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
      cmd.CommandText = @"INSERT INTO bands (band_name, genre) VALUES (@bandName, @genre);";

      MySqlParameter bandName = new MySqlParameter();
      bandName.ParameterName = "@bandName";
      bandName.Value = this._bandName;
      cmd.Parameters.Add(bandName);

      MySqlParameter genre = new MySqlParameter();
      genre.ParameterName = "@genre";
      genre.Value = this._genre;
      cmd.Parameters.Add(genre);

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
