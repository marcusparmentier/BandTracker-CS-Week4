using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;
using System.Collections.Generic;
using System;

namespace BandTracker.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
        return View();
    }

    [HttpGet("/bands")]
    public ActionResult Bands()
    {
        List<Band> allBands = Band.GetAll();
        return View(allBands);
    }

    [HttpGet("/bands/new")]
    public ActionResult BandForm()
    {
        return View();
    }

    [HttpPost("/bands/new")]
    public ActionResult BandCreate()
    {
        Band newBand = new Band(Request.Form["band-bandName"], Request.Form["band-genre"]);
        newBand.Save();
        return View("Success");
    }

    //Venues
    [HttpGet("/venues")]
    public ActionResult Venues()
    {
        List<Venue> allVenues = Venue.GetAll();
        return View(allVenues);
    }

    [HttpGet("/venues/new")]
    public ActionResult VenueForm()
    {
        return View();
    }

    [HttpPost("/venues/new")]
    public ActionResult VenueCreate()
    {
        Venue newVenue = new Venue(Request.Form["venue-venueName"]);
        newVenue.Save();
        return View("Success");
    }

  }
}
