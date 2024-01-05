using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public float maximum;
    public float current;
    public Image mask;

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }

    public void UpdateProgress(float newValue)
    {
        if (current < maximum)
        {
            current += newValue;
        }
    }
}
