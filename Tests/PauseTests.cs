using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[TestFixture]
public class PauseTests
{
    [Test]
    public void Resume_ShouldDeactivatePauseMenuAndActivateCamera()
    {
        Pause pause = new GameObject().AddComponent<Pause>();
        pause.pauseMenu = new GameObject();
        pause.cam = new GameObject().AddComponent<CameraMovement>();

        pause.Resume();

        Assert.IsFalse(pause.pauseMenu.activeSelf);
        Assert.IsTrue(pause.cam.isActive);
    }

    [Test]
    public void DisplaySavePanel_ShouldActivateSavePanel()
    {
        Pause pause = new GameObject().AddComponent<Pause>();
        pause.savePanel = new GameObject();

        pause.DisplaySavePanel();

        Assert.IsTrue(pause.savePanel.activeSelf);
    }

    [Test]
    public void Save_ShouldCallSaveGameInGameManagerWithCorrectName()
    {
        Pause pause = new GameObject().AddComponent<Pause>();
        pause.nameInput = new GameObject().AddComponent<TMP_InputField>();
        pause.nameInput.text = "TestSave";

        GameManager.Instance = new GameManager(); 

        pause.Save();
    }

    [Test]
    public void ToMenu_ShouldLoadMenuScene()
    {
        Pause pause = new GameObject().AddComponent<Pause>();

        pause.ToMenu();

        Assert.AreEqual("Menu", SceneManager.GetActiveScene().name);
    }
}
