using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class BandTests : IDisposable
  {
    public BandTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }

    [TestMethod]
    public void GetAll_BandsEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameBandNameAndGenre_Band()
    {
      //Arrange, Act
      Band firstBand = new Band("Robinhood","Grunge");
      Band secondBand = new Band("Robinhood","Grunge");
      Band failBand = new Band("Failhood","Failure");

      //Assert
      Assert.AreEqual(firstBand, secondBand);
    }

    [TestMethod]
    public void Save_SavesBandToDatabase_BandList()
    {
      //Arrange
      Band testBand = new Band("Robinhood","Grunge");
      testBand.Save();
      Band failBand = new Band("Failhood","Failure");

      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }


    [TestMethod]
    public void Save_DatabaseAssignsIdToBand_Id()
    {
      //Arrange
      Band testBand = new Band("Robinhood","Grunge");
      testBand.Save();
      Band failBand = new Band("Failhood","Failure");

      //Act
      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetId();
      int testId = testBand.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsBandInDatabase_Band()
    {
      //Arrange
      Band testBand = new Band("Robinhood","Grunge");
      testBand.Save();
      Band failBand = new Band("Failhood","Failure");

      //Act
      Band foundBand = Band.Find(testBand.GetId());

      //Assert
      Assert.AreEqual(testBand, foundBand);
    }

    [TestMethod]
    public void Test_AddVenue_AddsVenueToBand()
    {
      //Arrange
      Band testBand = new Band("Robinhood", "Grunge");
      testBand.Save();

      Venue testVenue = new Venue("The Firebird");
      testVenue.Save();

      Venue testVenue2 = new Venue("Scottrade Center");
      testVenue2.Save();

      //Act
      testBand.AddVenue(testVenue);
      testBand.AddVenue(testVenue2);

      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenue, testVenue2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetVenues_ReturnsAllBandVenues_VenueList()
    {
      //Arrange
      Band testBand = new Band("Robinhood", "Grunge");
      testBand.Save();

      Venue testVenue1 = new Venue("The Firebird");
      testVenue1.Save();

      // Venue failVenue = new Venue("United Center");
      // failVenue.Save();

      //Act
      testBand.AddVenue(testVenue1);
      List<Venue> savedVenues = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1};

      //Assert
      CollectionAssert.AreEqual(testList, savedVenues);
    }
  }
}
