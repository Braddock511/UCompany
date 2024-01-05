using UnityEngine;
using System;
using System.Collections.Generic;

public class ProjectManager : MonoBehaviour
{
    // Unity
    public static ProjectManager Instance;
    public GameObject prefab;
    public GameObject parentObject;
    // Variables
    public List<Project> projects;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartProject(string title, DateTime deadline, int max)
    {
        GameObject projectPrefab = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        Project project = projectPrefab.GetComponent<Project>();
        projectPrefab.transform.SetParent(parentObject.transform);
        projectPrefab.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

        project.title = title;
        project.deadline = deadline;
        project.titleText.text = title;
        project.deadlineText.text = deadline.ToString();
        project.progressBar.maximum = max; 
        project.progressBar.current = 0;

        projects.Add(project);

    }

    public void UpdateDaysLeft()
    {
        foreach(Project project in projects)
        {
            print(project);
            TimeSpan timeLeft = project.deadline - GameManager.Instance.date;
            Debug.Log("Days left for project '" + project.title + "': " + timeLeft);

            if (timeLeft.TotalDays <= 0)
            {
                Debug.Log("end");
            }
        }
    }

    private void FinishProject(Project project)
    {
        project.active = false;

        Debug.Log("Project finished: " + project.title);
    }
}

