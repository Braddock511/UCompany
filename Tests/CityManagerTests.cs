using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[TestClass]
public class CityManagerTests
{
    [TestMethod]
    public void Buy_AddsCityToPlayerOwnCitiesAndActivatesOwnObject()
    {
        var cityManager = new GameObject().AddComponent<CityManager>();
        var player = new Player();
        cityManager.player = player;
        cityManager.buyObject = new GameObject();
        cityManager.ownObject = new GameObject();

        cityManager.buy();

        Assert.IsTrue(player.ownCities.Contains("citymanager"));
        Assert.IsFalse(cityManager.buyObject.activeSelf);
        Assert.IsTrue(cityManager.ownObject.activeSelf);
    }

    [TestMethod]
    public void Preview_OwnCityActivatesOwnObjectAndUpdatesText()
    {
        var cityManager = new GameObject().AddComponent<CityManager>();
        var player = new Player();
        player.ownCities.Add("citymanager");
        var city = new City();
        city.employees = new List<Employee> { new Employee(), new Employee() };
        city.income = 5000;
        var cities = new Dictionary<string, City> { { "citymanager", city } };
        var gameManager = new GameManager();
        gameManager.player = player;
        gameManager.cities = cities;
        cityManager.gameObject.name = "CityManager";
        cityManager.income = new GameObject().AddComponent<TextMeshProUGUI>();
        cityManager.employees = new GameObject().AddComponent<TextMeshProUGUI>();
        cityManager.cityTitle = new GameObject().AddComponent<TextMeshProUGUI>();
        cityManager.buyObject = new GameObject();
        cityManager.ownObject = new GameObject();

        cityManager.preview();

        Assert.IsTrue(cityManager.ownObject.activeSelf);
        Assert.IsFalse(cityManager.buyObject.activeSelf);
        Assert.AreEqual("2", cityManager.employees.text);
        Assert.AreEqual("5000", cityManager.income.text);
        Assert.AreEqual("CityManager", cityManager.cityTitle.text);
    }

    [TestMethod]
    public void Preview_NonOwnCityActivatesBuyObject()
    {
        var cityManager = new GameObject().AddComponent<CityManager>();
        var player = new Player();
        var gameManager = new GameManager();
        gameManager.player = player;
        cityManager.gameObject.name = "CityManager";
        cityManager.buyObject = new GameObject();
        cityManager.ownObject = new GameObject();

        cityManager.preview();

        Assert.IsFalse(cityManager.ownObject.activeSelf);
        Assert.IsTrue(cityManager.buyObject.activeSelf);
    }
}
