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
  }
}
