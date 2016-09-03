using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
    private Enemy enemy;
    private PlayerController player;
    public Vector3 RespawnPosition;
	// Use this for initialization
	void Start () {
        enemy = GetComponent<Enemy>();
        player = FindObjectOfType <PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (enemy.dead)
        {
            Enemy.checkRespawn = true;
            player.SetRespawn(RespawnPosition);
        }
	}
}
