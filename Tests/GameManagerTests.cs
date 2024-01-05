using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class GameManagerTests
{
    [TestMethod]
    public void StartNewGame_ActivatesCompanyOptions()
    {
        var gameManager = new GameManager();
        var mainMenu = new GameObject("Canv_Main");
        var companyOptions = new GameObject("CompanyOptions");
        mainMenu.SetActive(true);
        companyOptions.SetActive(false);

        gameManager.StartNewGame();

        Assert.IsFalse(mainMenu.activeSelf);
        Assert.IsTrue(companyOptions.activeSelf);
    }

    [TestMethod]
    public void SetOptions_ResetsGameDataAndLoadsScene()
    {
        var gameManager = new GameManager();
        gameManager.nameInput = new TMP_InputField();
        gameManager.companyOptions = new GameObject();

        gameManager.SetOptions();

        Assert.IsTrue(gameManager.firstTime);
        Assert.AreEqual(0, gameManager.money);
        Assert.IsNotNull(gameManager.player);
        Assert.IsNotNull(gameManager.cities);
        Assert.IsNotNull(gameManager.projects);
        Assert.IsNotNull(gameManager.date);
    }

    [TestMethod]
    public void SaveGame_SavesDataToCloud()
    {
        var gameManager = new GameManager();
        gameManager.nameInput = new TMP_InputField();
        var cloudMock = new CloudDataMock();
        gameManager.cloud = cloudMock;
        gameManager.projects = new List<Project>();

        gameManager.SaveGame("TestSave");

        Assert.IsTrue(cloudMock.SaveCalled);
    }

    [TestMethod]
    public void LoadGame_LoadsDataFromCloud()
    {
        var gameManager = new GameManager();
        var cloudMock = new CloudDataMock();
        gameManager.cloud = cloudMock;

        gameManager.LoadGame("TestSave");

        Assert.IsTrue(cloudMock.LoadCalled);
    }

    [TestMethod]
    public void GenerateSaveButtons_CreatesButtonsCorrectly()
    {
        var gameManager = new GameManager();
        gameManager.savedGames = new List<string> { "Save1", "Save2", "Save3" };
        gameManager.saveButtonPrefab = new GameObject();
        gameManager.saveButtonContainer = new GameObject().transform;

        gameManager.GenerateSaveButtons();

        Assert.AreEqual(3, gameManager.saveButtonContainer.childCount);
    }
}

public class CloudDataMock : CloudData
{
    public bool SaveCalled { get; private set; }
    public bool LoadCalled { get; private set; }

    public override async Task Save(string saveName, SaveData data)
    {
        SaveCalled = true;
        await base.Save(saveName, data);
    }

    public override async Task<T> Load<T>(string saveName)
    {
        LoadCalled = true;
        return await base.Load<T>(saveName);
    }
}
