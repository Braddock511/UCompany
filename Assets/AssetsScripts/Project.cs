using UnityEngine;
using TMPro;
using System;
public class Project : MonoBehaviour
{
    public bool active = true;
    public string title;
    public int progress = 0;
    public DateTime deadline;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI deadlineText;
    public ProgressBar progressBar;
}
