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

    [TestMethod]
    public void Save_SavesToDatabase_VenueList()
    {
      //Arrange
      Venue testVenue = new Venue("The Firebird");
      // Venue failVenue = new Venue("The Failbird");

      //Act
      testVenue.Save();
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Venue testVenue = new Venue("The Firebird");

      //Act
      testVenue.Save();
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsVenueInDatabase_Venue()
    {
      //Arrange
      Venue testVenue = new Venue("The Firebird");
      testVenue.Save();
      Venue failVenue = new Venue("The Failbird");

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.AreEqual(testVenue, foundVenue);
    }

    [TestMethod]
    public void Update_UpdatesVenueInDatabase_String()
    {
      //Arrange
      Venue testVenue = new Venue("The Firebird");
      testVenue.Save();
      string newName = "Scottrade Center";
      // string failName = "United Center";

      //Act
      testVenue.UpdateVenueName(newName);

      string result = Venue.Find(testVenue.GetId()).GetVenueName();

      //Assert
      Assert.AreEqual(newName, result);
    }
  }
}
