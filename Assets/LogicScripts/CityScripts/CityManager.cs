using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CityManager : MonoBehaviour
{
    // Unity
    public static CityManager Instance;
    public GameObject ownObject;
    public GameObject buyObject;
    public TextMeshProUGUI income;
    public TextMeshProUGUI employees;
    public TextMeshProUGUI cityTitle;

    // Variables
    private Dictionary<string, City> cities;
    private Player player;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void buy()
    {
        string cityName = gameObject.name.ToLower();

        player.ownCities.Add(cityName);
        buyObject.SetActive(false);
        ownObject.SetActive(true);
    }

    public void preview()
    {
        cities = GameManager.Instance.cities;
        player = GameManager.Instance.player;
        string cityName = gameObject.name.ToLower();

        if (player.ownCities.Contains(cityName))
        {
            ownObject.SetActive(true);
            City city = cities[cityName];
            string cityEmployees = city.employees.Count.ToString();
            string cityIncome = city.income.ToString();
            employees.text = cityEmployees;
            income.text = cityIncome;
        }
        else
        {
            buyObject.SetActive(true);
        }

        // Details
        cityTitle.text = gameObject.name;
    }

}
