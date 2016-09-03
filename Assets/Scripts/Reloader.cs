using UnityEngine;
using System.Collections;

public class Reloader : MonoBehaviour {
    public PlayerController player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (player.state == PlayerController.State.Death && Input.GetButtonDown("Fire1"))
        {
            Reload();
        }
	}

    public void Reload()
    {
        player.Respawn();
        Application.LoadLevel(Application.loadedLevel);
    }
}
