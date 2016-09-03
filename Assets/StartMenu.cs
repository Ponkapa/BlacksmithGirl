using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
    public GameObject ChildCanvas;
    private bool isActive = true;
    void Update()
    {
        if (Application.loadedLevel != 0)
        {
            if (Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.Escape))
            {
                ChildCanvas.SetActive(isActive);
                isActive = !isActive;
            }
        }
    }
    public void LoadLevel(int level)
    {
        Application.LoadLevel(level);
    }
}
