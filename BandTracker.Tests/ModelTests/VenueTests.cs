using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;
using System;
using System.Collections.Generic;

namespace BandTracker.Tests
{
  [TestClass]
  public class VenueTest : IDisposable
  {

    public VenueTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      // Band.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfNamesAreTheSame_Venue()
    {
      // Arrange, Act
      Venue firstVenue = new Venue("The Firebird");
      Venue secondVenue = new Venue("The Firebird");
      Venue failVenue = new Venue("The Firebirds");

      // Assert
      Assert.AreEqual(firstVenue, secondVenue);
    }
  }
}
