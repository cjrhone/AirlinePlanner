using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirlinePlanner.Models;
using System.Collections.Generic;
using System;

namespace AirlinePlanner.Tests
{

    [TestClass]
    public class CategoriesTests : IDisposable
    {

      public void Dispose()
      {
        Categories.DeleteAll();
      }

      [TestMethod]
      public void GetAll_DbStartsEmpty_0()
      {
        //Arrange
        //Act

        int result = Categories.GetAll().Count;

        //Assert
        Assert.AreEqual(0, result);
      }

      // [TestMethod]
      // public void Equals_ReturnsTrueIfNamesAreTheSame_City()
      // {
      //   Item firstCity = new Item("Chicago");
      //   Item secondCity = new Item("Chicago");
      //
      //
      //   Assert.AreEqual(firstCity, secondCity);
      // }
      //
      // [TestMethod]
      // public void Save_SavesToDatabase_ItemList()
      // {
      //   Item testCity = new Item("Seattle");
      //
      //   testCity.Save();
      //   List<Item> result = Item.GetAll();
      //   List<Item> testList = new List<Item>{testCity};
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
      //   Item testCity = new Item("This a test");
      //   Item testCity2 = new Item("hi");
      //
      //   //Act
      //   testCity.Save();
      //   testCity2.Save();
      //   Item savedCity = Item.GetAll()[1];
      //
      //   int result = savedCity.GetCityId();
      //   int testId = testCity2.GetCityId();
      //
      //   //Assert
      //   Assert.AreEqual(testId, result);
      // }
    }
  }
