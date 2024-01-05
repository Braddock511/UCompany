using UnityEngine;
using TMPro;

public class Panel : MonoBehaviour
{
    // Unity
    private TextMeshProUGUI cityTitle;
    // Variables
    private bool isActive = false;
    
    private void Awake()
    {
        cityTitle = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Display(TextMeshPro cityName)
    {
        if (!isActive)
        {
            // Close other city panels
            Panel[] panels = FindObjectsOfType<Panel>();
            foreach (Panel panel in panels)
            {
                if (panel != this)
                {
                    panel.Hide();
                }
            }
        }
        isActive = true;
        gameObject.SetActive(true);
        cityTitle.text = cityName.text;
    }

    public void Hide()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

}
