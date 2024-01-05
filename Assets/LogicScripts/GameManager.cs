using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEditor.SearchService;

[System.Serializable]
public class City
{
    public string cityName;
    public List<Employee> employees;
    public int income;
}

[System.Serializable]
public class Player
{
    public List<string> ownCities { get; set; }
    public float income { get; set; }
    public float expense { get; set; }
}

[System.Serializable]
public class SaveData
{
    public bool firstTime;
    public int money;
    public Dictionary<string, City> cities;
    public Player player;
    public List<Project> projects;
    public System.DateTime date;
}

public class GameManager : MonoBehaviour
{
    // Unity
    public static GameManager Instance;
    public GameObject saveButtonPrefab;
    public Transform saveButtonContainer;
    public TMP_InputField nameInput;
    public GameObject companyOptions;
    private CloudData cloud;
    // Variables
    public bool firstTime;
    public int money;
    public Player player;
    public Dictionary<string, City> cities;
    public List<string> savedGames;
    public System.DateTime date;
    public List<Project> projects;

    void Awake()
    {
        // Set the target frame rate to 60 FPS
        Application.targetFrameRate = 60;
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    async void Start()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        cloud = new CloudData();
        savedGames = await cloud.GetAllSaves();
    }

    public void StartNewGame()
    {
        GameObject mainMenu = GameObject.Find("Canv_Main");
        mainMenu.SetActive(false);
        companyOptions.SetActive(true);
    }

    async public void SetOptions()
    {
        SceneManager.LoadScene("Loading");
        firstTime = true;
        InitializeCities();
        InitializePlayer();
        money = 0;
        projects = new List<Project>();
        date = System.DateTime.Now;
        string saveName = nameInput.text;
        await SaveGame(saveName);
        SceneManager.LoadScene("Game");
    }

    private void InitializeCities()
    {
        string filePath = "Assets/cities.json";
        string jsonText = File.ReadAllText(filePath);

        City[] cityDataArray = JsonConvert.DeserializeObject<City[]>(jsonText);
        cities = new Dictionary<string, City>();

        for (int i = 0; i < cityDataArray.Length; i++)
        {
            City cityData = cityDataArray[i];
            string cityName = cityData.cityName;

            cities.Add(cityName, cityData);
        }
    }

    public void InitializePlayer()
    {
        string filePath = "Assets/player.json";
        string jsonText = File.ReadAllText(filePath);
        player = JsonConvert.DeserializeObject<Player>(jsonText);
        
        Player newPlayer = player;
    }

    public async Task SaveGame(string saveName)
    {
        SaveData saveData = new()
        {
            firstTime = firstTime,
            money = money,
            cities = cities,
            player = player,
            projects = ProjectManager.Instance != null ? ProjectManager.Instance.projects : new List<Project>(),
            date = date
        };
        print(saveData);
        await cloud.Save(saveName, saveData);
    }

    public async void LoadGame(string saveName)
    {
        SceneManager.LoadScene("Loading");

        SaveData saveData = await cloud.Load<SaveData>(saveName);
        firstTime = saveData.firstTime;
        money = saveData.money;
        cities = saveData.cities;
        player = saveData.player;
        ProjectManager.Instance.projects = saveData.projects;
        date = saveData.date;

        SceneManager.LoadScene("Game");
    }

    public void GenerateSaveButtons()
    {
        float yOffset = 0f;

        foreach (Transform child in saveButtonContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (string saveName in savedGames)
        {
            GameObject saveButton = Instantiate(saveButtonPrefab, saveButtonContainer);

            RectTransform buttonTransform = saveButton.GetComponent<RectTransform>();
            Vector3 buttonPosition = buttonTransform.localPosition;
            buttonPosition += new Vector3(0f, yOffset, 0f);
            buttonTransform.localPosition = buttonPosition;

            saveButton.GetComponentInChildren<TextMeshProUGUI>().text = saveName;
            saveButton.GetComponent<Button>().onClick.AddListener(() => LoadGame(saveName));

            yOffset += 100f;
        }
    }
}
