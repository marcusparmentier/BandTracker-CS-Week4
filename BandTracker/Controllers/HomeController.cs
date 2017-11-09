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

    //Bands
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

    [HttpGet("/bands/{id}")]
    public ActionResult BandDetails(int id)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band SelectedBand = Band.Find(id);
        List<Venue> BandVenues = SelectedBand.GetVenues();
        List<Venue> AllVenues = Venue.GetAll();
        model.Add("band", SelectedBand);
        model.Add("bandVenues", BandVenues);
        model.Add("allVenues", AllVenues);
        return View(model);
    }

    [HttpPost("bands/{bandId}/venues/new")]
    public ActionResult BandAddVenue(int bandId)
    {
        Band band = Band.Find(bandId);
        Venue venue = Venue.Find(Int32.Parse(Request.Form["venue-id"]));

        band.AddVenue(venue);
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
