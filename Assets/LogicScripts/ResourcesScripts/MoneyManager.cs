using UnityEngine;
using System;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    // Unity
    private TimeManagement gameTime;
    private DateTime lastMoneyIncreaseDate;
    public TextMeshProUGUI moneyText;
    // Variables
    public int money;
    public int interval = 7;

    void Start()
    {
        gameTime = FindObjectOfType<TimeManagement>();
        moneyText = GetComponent<TextMeshProUGUI>();
        lastMoneyIncreaseDate = gameTime.CurrentDate;

        money = GameManager.Instance.money;
        moneyText.text = money.ToString();
    }

    void Update()
    {
        if (gameTime == null) return;

        DateTime currentDate = gameTime.CurrentDate;
        TimeSpan timePassed = currentDate - lastMoneyIncreaseDate;
        if (timePassed.TotalDays >= interval)
        {
            money += (int)Math.Round(100 * MoneyFactors.Instance.testFactor);
            GameManager.Instance.money = money;
            lastMoneyIncreaseDate = currentDate;
            moneyText.text = money.ToString();
        }
    }

    public void ChangeMoney(int newMoney)
    {
        GameManager.Instance.money += newMoney;
        moneyText.text = GameManager.Instance.money.ToString();
    }
}
