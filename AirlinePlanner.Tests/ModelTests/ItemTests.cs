using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirlinePlanner.Models;
using System.Collections.Generic;
using System;

namespace AirlinePlanner.Tests
{

    [TestClass]
    public class ItemTests : IDisposable
    {

      public void Dispose()
      {
        // Item.ClearAll();
      }

      [TestMethod]
      public void GetAll_DbStartsEmpty_0()
      {
        //Arrange
        //Act

        int result = Item.GetAll().Count;

        //Assert
        Assert.AreEqual(0, result);
      }

      [TestMethod]
      public void Equals_ReturnsTrueIfNamesAreTheSame_City()
      {
        Item firstCity = new Item("Chicago");
        Item secondCity = new Item("Chicago");
        Console.WriteLine("this1WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");

        Assert.AreEqual(firstCity, secondCity);
      }

      [TestMethod]
      public void Save_SavesToDatabase_ItemList()
      {
        Item testCity = new Item("Seattle");

        testCity.Save();
        List<Item> result = Item.GetAll();
        List<Item> testList = new List<Item>{testCity};
        Console.WriteLine("this2ZZzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz");

        CollectionAssert.AreEqual(testList, result);
      }
    }
  }
