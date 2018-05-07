using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirlinePlanner.Models;
using System.Collections.Generic;
using System;

namespace AirlinePlanner.Tests
{

    [TestClass]
    public class CityTests : IDisposable
    {

      public void Dispose()
      {
        City.DeleteAll();
      }

      [TestMethod]
      public void GetAll_DbStartsEmpty_0()
      {
        //Arrange
        //Act

        int result = City.GetAll().Count;

        //Assert
        Assert.AreEqual(0, result);
      }

      [TestMethod]
      public void Equals_ReturnsTrueIfNamesAreTheSame_City()
      {
        City firstCity = new City("Chicago");
        City secondCity = new City("Chicago");


        Assert.AreEqual(firstCity, secondCity);
      }

      [TestMethod]
      public void Save_SavesToDatabase_CityList()
      {
        City testCity = new City("Seattle");

        testCity.Save();
        List<City> result = City.GetAll();
        List<City> testList = new List<City>{testCity};
        Console.WriteLine("result " + result.Count);
        Console.WriteLine("testList " + testList.Count);


        CollectionAssert.AreEqual(testList, result);
      }

      [TestMethod]
      public void Save_AssignsIdToObject_Id()
      {
        //Arrange
        City testCity = new City("This a test");
        City testCity2 = new City("hi");

        //Act
        testCity.Save();
        testCity2.Save();
        City savedCity = City.GetAll()[1];

        int result = savedCity.GetCityId();
        int testId = testCity2.GetCityId();

        //Assert
        Assert.AreEqual(testId, result);
      }
    }
  }
