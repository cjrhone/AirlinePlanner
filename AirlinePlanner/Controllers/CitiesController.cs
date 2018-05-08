using Microsoft.AspNetCore.Mvc;
using AirlinePlanner.Models;
using System.Collections.Generic;

namespace AirlinePlanner.Controllers
{
  public class CitiesController : Controller
  {
    [HttpGet("/cities")]
    public ActionResult Index()
    {
      List<City> allCities = City.GetAll();
      return View(allCities);
    }

    [HttpGet("/cities/new")]
    public ActionResult CreateForm()
    {
        return View();
    }
    [HttpPost("/cities")]
    public ActionResult Create()
    {
        City newCity = new City(Request.Form["city-name"]);
        newCity.Save();
        return RedirectToAction("Index");
    }

    [HttpGet("/cities/{id}")]
    public ActionResult Details(int id)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        City selectedCity = City.Find(id);
        List<Flight> cityFlights = selectedCity.GetFlights();
        List<Flight> allFlights = Flight.GetAll();
        model.Add("selectedCity", selectedCity);
        model.Add("cityFlights", cityFlights);
        model.Add("allFlights", allFlights);
        return View(model);
    }

    // [HttpPost("/cities/{cityId}/flights/new")]
    // public ActionResult AddFlight(int cityId)
    // {
    //     City city = City.Find(cityId);
    //     Flight flight = Flight.Find(Int32.Parse(Request.Form["flight-id"]));
    //     city.AddFlight(flight);
    //     return RedirectToAction("Details",  new { id = cityId });
    // } //Adds flight to city
  }
}
