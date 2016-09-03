using UnityEngine;
using System.Collections;

public class FinalBossDoor : MonoBehaviour {
    public Vector3 spawnPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.GetComponent<PlayerController>())
        {
            player.GetComponent<PlayerController>().respawnPosition = spawnPos;
        }
    }
}
