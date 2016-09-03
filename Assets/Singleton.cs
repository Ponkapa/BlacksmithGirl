using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour {
    public static GameObject instance;
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
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
