using UnityEngine;
using TMPro;

public class Display : MonoBehaviour
{
    // Unity
    private CameraMovement cam;
    // Variables
    
    private void Awake()
    {
        cam = FindObjectOfType<CameraMovement>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        cam.isActive = false;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        cam.isActive = true;
    }

}
