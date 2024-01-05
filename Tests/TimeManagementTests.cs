using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine.UI;
using TMPro;
using System;

[TestClass]
public class TimeManagementTests
{
    [TestMethod]
    public void InitializeTime_SetsInitialValues()
    {
        var timeManagement = new GameObject().AddComponent<TimeManagement>();
        timeManagement.playButton = new GameObject().AddComponent<Button>();
        timeManagement.timeText = new GameObject().AddComponent<TextMeshProUGUI>();
        timeManagement.dateText = new GameObject().AddComponent<TextMeshProUGUI>();
        var gameManager = new GameManager();
        gameManager.date = new DateTime(2022, 1, 1, 12, 0, 0);
        timeManagement.Start();

        Assert.AreEqual("12:00", timeManagement.timeText.text);
        Assert.AreEqual("01.01.2022", timeManagement.dateText.text);
    }

    [TestMethod]
    public void UpdateTime_IncreasesCurrentDate()
    {
        var timeManagement = new GameObject().AddComponent<TimeManagement>();
        var gameManager = new GameManager();
        gameManager.date = new DateTime(2022, 1, 1, 12, 0, 0);
        timeManagement.Start();

        timeManagement.UpdateTime();

        Assert.AreEqual(new DateTime(2022, 1, 1, 12, 1, 0), gameManager.date);
    }

    [TestMethod]
    public void TogglePlayTime_TogglesPlayTime()
    {
        var timeManagement = new GameObject().AddComponent<TimeManagement>();

        timeManagement.TogglePlayTime();

        Assert.IsTrue(timeManagement.playTime);

        timeManagement.TogglePlayTime();

        Assert.IsFalse(timeManagement.playTime);
    }
}
