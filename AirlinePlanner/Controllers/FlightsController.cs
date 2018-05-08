using Microsoft.AspNetCore.Mvc;
using AirlinePlanner.Models;
using System.Collections.Generic;

namespace AirlinePlanner.Controllers
{
  public class FlightsController : Controller
  {
    [HttpGet("/flights")]
    public ActionResult Index()
    {
        List<Flight> allFlights = Flight.GetAll();
        return View(allFlights);

    }

    [HttpGet("/flights/new")]
    public ActionResult CreateForm()
    {
      List<City> allCities = City.GetAll();
      return View(allCities);
    }
    [HttpPost("/flights")]
    public ActionResult Create()
    {
        Flight newFlight = new Flight(Request.Form["flight-name"], Request.Form["departure-time"], 0, Request.Form["departure-city"], Request.Form["arrival-time"],
        0, Request.Form["arrival-city"], Request.Form["status"]);
        newFlight.Save();
        return RedirectToAction("Index");
    }

    // [HttpGet("/flights/{id}")]
    // public ActionResult Details(int id)
    // {
    //   List<City> allCities = City.GetAll();
    //   Flight selectedFlight = Flight.Find(id);
    //   //has selected flight as its value
    //   List<City> flightCities = selectedFlight.GetCities();
    //   //has list of all selected flight's cities as its value
    //   List<City> allCities = City.GetAll();
    //   //has all cities that have been added to list, regardless of flight as its value
    //   model.Add("selectedFlight", selectedFlight);
    //   model.Add("flightCities", flightCities);
    //   model.Add("allCities", allCities);
    //   return View(model);
    // }

    // [HttpPost("/flights/{flightId}/cities/new")]
    // public ActionResult AddCity(int flightId)
    // {
    //     Flight flight = Flight.Find(flightId);
    //     City city = City.Find(Int32.Parse(Request.Form["city-id"]));
    //     flight.AddCity(city);
    //     return RedirectToAction("Details",  new { id = flightId });
    // } //Adds city to flight
  }
}
