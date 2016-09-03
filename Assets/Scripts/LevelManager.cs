using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public static GameObject instance;
    public GameObject ChildCanvas;
    private bool isActive = true;
    void Awake()
    {
        if (instance == null)

            //if not, set instance to this
            instance = this.gameObject;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

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

    public void ExitGame()
    {
        Application.Quit();
    }
	public void ToggleTimer()
    {
        Timer.Enabler();
    }
    public void ToggleNG()
    {
        PlayerController.NGPlus = !PlayerController.NGPlus;
    }
}
