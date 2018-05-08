using Microsoft.AspNetCore.Mvc;
using AirlinePlanner.Models;
using System.Collections.Generic;

namespace AirlinePlanner.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
        return View();
    }
  }
}
