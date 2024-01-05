using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Pause : MonoBehaviour
{
    // Unity
    public GameObject pauseMenu;
    private CameraMovement cam;
    private TimeManagement time;
    public GameObject savePanel;
    public TMP_InputField nameInput;
    // Variables


    void Start()
    {
        time = FindObjectOfType<TimeManagement>();
        cam = FindObjectOfType<CameraMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            time.playTime = false;
            cam.isActive = false;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        cam.isActive = true;
    }

    public void DisplaySavePanel()
    {
        savePanel.SetActive(true);
    }

    public async void Save()
    {
        string saveName = nameInput.text;
        await GameManager.Instance.SaveGame(saveName);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
