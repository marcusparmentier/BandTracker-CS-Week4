using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;
using System;
using System.Collections.Generic;

namespace BandTracker.Tests
{
  [TestClass]
  public class VenueTest
  {

    public VenueTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }

    // public void Dispose()
    // {
    //   Venue.DeleteAll();
    //   Band.DeleteAll();
    // }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
  }
}
