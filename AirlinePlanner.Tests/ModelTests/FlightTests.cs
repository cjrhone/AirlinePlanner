using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirlinePlanner.Models;
using System.Collections.Generic;
using System;

namespace AirlinePlanner.Tests
{

    [TestClass]
    public class FlightTests : IDisposable
    {

      public void Dispose()
      {
        Flight.DeleteAll();
      }

      [TestMethod]
      public void GetAll_DbStartsEmpty_0()
      {
        //Arrange
        //Act

        int result = Flight.GetAll().Count;

        //Assert
        Assert.AreEqual(0, result);
      }

      [TestMethod]
      public void Equals_ReturnsTrueIfNamesAreTheSame_Flight()
      {
        Flight firstFlight = new Flight("Delta", "11AM", "Seattle", "4PM", "Detroit", "Delayed");
        Flight secondFlight = new Flight("Delta", "11AM", "Seattle", "4PM", "Detroit", "Delayed");


        Assert.AreEqual(firstFlight, secondFlight);
      }

      // [TestMethod]
      // public void Save_SavesToDatabase_ItemList()
      // {
      //   Flight testCity = new Flight("Seattle");
      //
      //   testCity.Save();
      //   List<Flight> result = Flight.GetAll();
      //   List<Flight> testList = new List<Flight>{testCity};
      //   Console.WriteLine("result " + result.Count);
      //   Console.WriteLine("testList " + testList.Count);
      //
      //
      //   CollectionAssert.AreEqual(testList, result);
      // }
      //
      // [TestMethod]
      // public void Save_AssignsIdToObject_Id()
      // {
      //   //Arrange
      //   Flight testCity = new Flight("This a test");
      //   Flight testCity2 = new Flight("hi");
      //
      //   //Act
      //   testCity.Save();
      //   testCity2.Save();
      //   Flight savedCity = Flight.GetAll()[1];
      //
      //   int result = savedCity.GetCityId();
      //   int testId = testCity2.GetCityId();
      //
      //   //Assert
      //   Assert.AreEqual(testId, result);
      // }
    }
  }
