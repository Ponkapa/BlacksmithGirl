using UnityEngine;
using System.Collections;

public class CanStand : MonoBehaviour {
    public PlayerController Player;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        Player.Standable = false;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Player.Standable = true;
    }
}
