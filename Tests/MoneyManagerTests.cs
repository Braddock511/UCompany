using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;
using TMPro;
using System;

[TestClass]
public class MoneyManagerTests
{
    [TestMethod]
    public void Update_TimeIntervalPassed_IncreasesMoneyAndUpdatesLastMoneyIncreaseDate()
    {
        var moneyManager = new GameObject().AddComponent<MoneyManager>();
        var timeManagement = new GameObject().AddComponent<TimeManagement>();
        timeManagement.CurrentDate = DateTime.Now.AddDays(10);
        moneyManager.gameTime = timeManagement;
        moneyManager.lastMoneyIncreaseDate = DateTime.Now.AddDays(-10);
        moneyManager.money = 1000;
        moneyManager.moneyText = new GameObject().AddComponent<TextMeshProUGUI>();

        moneyManager.Update();

        Assert.AreEqual(1100, moneyManager.money);
        Assert.AreEqual(timeManagement.CurrentDate, moneyManager.lastMoneyIncreaseDate);
        Assert.AreEqual("1100", moneyManager.moneyText.text);
    }

    [TestMethod]
    public void Update_NotEnoughTimeIntervalPassed_DoesNotChangeMoneyOrLastMoneyIncreaseDate()
    {
        var moneyManager = new GameObject().AddComponent<MoneyManager>();
        var timeManagement = new GameObject().AddComponent<TimeManagement>();
        timeManagement.CurrentDate = DateTime.Now.AddDays(5);
        moneyManager.gameTime = timeManagement;
        moneyManager.lastMoneyIncreaseDate = DateTime.Now.AddDays(-3);
        moneyManager.money = 1000;
        moneyManager.moneyText = new GameObject().AddComponent<TextMeshProUGUI>();

        moneyManager.Update();

        Assert.AreEqual(1000, moneyManager.money);
        Assert.AreEqual(DateTime.Now.AddDays(-3), moneyManager.lastMoneyIncreaseDate);
        Assert.AreEqual("1000", moneyManager.moneyText.text);
    }

    [TestMethod]
    public void ChangeMoney_AddsNewMoneyToGameManagerAndUpdatesText()
    {
        var moneyManager = new GameObject().AddComponent<MoneyManager>();
        moneyManager.money = 500;
        moneyManager.moneyText = new GameObject().AddComponent<TextMeshProUGUI>();

        moneyManager.ChangeMoney(200);

        Assert.AreEqual(700, GameManager.Instance.money);
        Assert.AreEqual("700", moneyManager.moneyText.text);
    }
}
