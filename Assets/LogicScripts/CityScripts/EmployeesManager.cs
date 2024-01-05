using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class EmployeesManager : MonoBehaviour
{
    // Unity
    public static EmployeesManager Instance;
    public GameObject employeePrefab;
    // Variables
    private List<Project> projects;
    public List<Employee> activeEmployees;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Stat()
    {
        UpdateEmployees();
        projects = ProjectManager.Instance.projects;
    }

    public void DisplayEmployees()
    {
        // hireButton.onClick.AddListener(() => AddEmployee(""));
    }

    public void AddEmployee(string cityName, string employeeName, int age, float salary, string skills)
    {
        Employee newEmployee = new (employeeName, age, salary, skills);

        Dictionary<string, City> cities = GameManager.Instance.cities;
        City city = cities[cityName];

        city.employees.Add(newEmployee);
    }

    public void UpdateEmployees()
    {
        List<string> playerCities = GameManager.Instance.player.ownCities;
        Dictionary<string, City> cities = GameManager.Instance.cities;

        foreach(string city in playerCities)
        {
            activeEmployees.AddRange(cities[city].employees);
        }
    }

    public void Work(string nameProject, string nameEmployee)
    {
        Project project = projects.First(project => project.title == nameProject);
        // Dodać jakieś przeliczenia w zależności od skillsów pracownika
        // Employee employee = activeEmployees.FirstOrDefault(employee => employee.employeeName == nameEmployee);
        // float amountProgress = employee.skills 
        // project.progressBar.UpdateProgress(amountProgress);
        project.progressBar.UpdateProgress(0.01f);
        
    }

}
