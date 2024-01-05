using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimeManagement : MonoBehaviour
{
    // Unity
    public Button playButton;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dateText;
    // Variables
    private DateTime currentDate;
    public float timeScale = 4f;
    public bool playTime = false;
    private int hours;
    private int minutes;


    private void Start()
    {
        InitializeTime();
        UpdateTimeText();
    }

    private void Update()
    {
        if(playTime)
        {
            UpdateTimeText();
            ProjectManager.Instance.UpdateDaysLeft();
            EmployeesManager.Instance.Work("First project", "David");
        }
    }

    private void FixedUpdate()
    {
        if(playTime)
        {
            UpdateTime();
            UpdateFixedDeltaTime();
        }
    }

    private void InitializeTime()
    {
        playButton.onClick.AddListener(TogglePlayTime);
        currentDate = GameManager.Instance.date;
        hours = currentDate.Hour;
        minutes = currentDate.Minute;
    }

    private void UpdateTime()
    {
        TimeSpan timePassed = TimeSpan.FromSeconds(Time.fixedDeltaTime * timeScale * 3600);
        currentDate = currentDate.Add(timePassed);
        GameManager.Instance.date = currentDate;
        hours = currentDate.Hour;
        minutes = currentDate.Minute;
    }

    private void UpdateFixedDeltaTime()
    {
        Time.fixedDeltaTime = 0.02f * timeScale;
    }

    private void UpdateTimeText()
    {
        string hoursText = hours.ToString("D2");
        string minutesText = minutes.ToString("D2");
        timeText.SetText($"{hoursText}:{minutesText}");

        string newDate = currentDate.ToString("dd.MM.yyyy");
        dateText.SetText(newDate);
    }

    public void TogglePlayTime()
    {
        playTime = !playTime;
    }

    public DateTime CurrentDate
    {
        get { return currentDate; }
    }
}
