using NUnit.Framework;
using TMPro;
using UnityEngine;

[TestFixture]
public class PanelTests
{
    [Test]
    public void Display_ShouldActivatePanelAndSetTitle()
    {
        Panel panel = new GameObject().AddComponent<Panel>();
        panel.cityTitle = new GameObject().AddComponent<TextMeshProUGUI>();
        TextMeshPro cityName = new GameObject().AddComponent<TextMeshPro>();
        cityName.text = "TestCity";

        panel.Display(cityName);

        Assert.IsTrue(panel.isActive);
        Assert.IsTrue(panel.gameObject.activeSelf);
        Assert.AreEqual("TestCity", panel.cityTitle.text);
    }

    [Test]
    public void Hide_ShouldDeactivatePanel()
    {
        Panel panel = new GameObject().AddComponent<Panel>();
        panel.isActive = true;
        panel.gameObject.SetActive(true);

        panel.Hide();

        Assert.IsFalse(panel.isActive);
        Assert.IsFalse(panel.gameObject.activeSelf);
    }

    [Test]
    public void Display_ShouldHideOtherCityPanels()
    {
        Panel panel1 = new GameObject().AddComponent<Panel>();
        Panel panel2 = new GameObject().AddComponent<Panel>();

        panel1.Display(new TextMeshPro());
        
        Assert.IsFalse(panel2.gameObject.activeSelf);
    }
}
