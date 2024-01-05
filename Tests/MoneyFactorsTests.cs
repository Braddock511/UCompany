using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;
using TMPro;

[TestClass]
public class MoneyFactorsTests
{
    [TestMethod]
    public void Upgrade_WithEnoughMoney_DecreasesMoneyAndIncreasesTestFactor()
    {
        var moneyFactors = new GameObject().AddComponent<MoneyFactors>();
        var moneyText = new GameObject().AddComponent<TextMeshProUGUI>();
        moneyFactors.moneyText = moneyText;
        moneyFactors.money = 1500;

        moneyFactors.Upgrade();

        Assert.AreEqual(1400, moneyFactors.money);
        Assert.AreEqual(1.1f, moneyFactors.testFactor);
        Assert.AreEqual("1400", moneyFactors.moneyText.text);
    }

    [TestMethod]
    public void Upgrade_WithoutEnoughMoney_DoesNotChangeMoneyOrTestFactor()
    {
        var moneyFactors = new GameObject().AddComponent<MoneyFactors>();
        var moneyText = new GameObject().AddComponent<TextMeshProUGUI>();
        moneyFactors.moneyText = moneyText;
        moneyFactors.money = 900;

        moneyFactors.Upgrade();

        Assert.AreEqual(900, moneyFactors.money);
        Assert.AreEqual(1.0f, moneyFactors.testFactor);
        Assert.AreEqual("900", moneyFactors.moneyText.text);
    }
}
