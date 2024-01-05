using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class EmployeesManagerTests
{
    [TestMethod]
    public void AddEmployee_AddsEmployeeToCity()
    {
        var employeesManager = new GameObject().AddComponent<EmployeesManager>();
        var city = new City();
        var cities = new Dictionary<string, City> { { "TestCity", city } };
        var gameManager = new GameManager();
        gameManager.cities = cities;
        employeesManager.gameObject.name = "TestCity";
        employeesManager.Stat();

        employeesManager.AddEmployee("TestCity", "John Doe", 30, 5000, "Programming");

        Assert.AreEqual(1, city.employees.Count);
        Assert.AreEqual("John Doe", city.employees[0].employeeName);
    }

    [TestMethod]
    public void UpdateEmployees_PopulatesActiveEmployeesList()
    {
        var employeesManager = new GameObject().AddComponent<EmployeesManager>();
        var player = new Player();
        player.ownCities.Add("City1");
        player.ownCities.Add("City2");
        var city1 = new City();
        city1.employees.Add(new Employee());
        var city2 = new City();
        city2.employees.Add(new Employee());
        var cities = new Dictionary<string, City> { { "City1", city1 }, { "City2", city2 } };
        var gameManager = new GameManager();
        gameManager.cities = cities;
        gameManager.player = player;

        employeesManager.UpdateEmployees();

        Assert.AreEqual(2, employeesManager.activeEmployees.Count);
    }

    [TestMethod]
    public void Work_UpdatesProjectProgressBar()
    {
        var employeesManager = new GameObject().AddComponent<EmployeesManager>();
        var projectManager = new GameObject().AddComponent<ProjectManager>();
        var project = new Project { title = "TestProject", progressBar = new ProgressBar() };
        var projects = new List<Project> { project };
        projectManager.projects = projects;
        employeesManager.projects = projects;

        employeesManager.Work("TestProject", "John Doe");

        Assert.AreEqual(0.01f, project.progressBar.current);
    }
}
