using UnityEngine;
using System.Collections;

public class MobSpawner : MonoBehaviour {
    public GameObject[] enemies;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!PlayerController.gameComplete)
        {
            if (Random.Range(0, 500) == 1)
            {
                GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity) as GameObject;
                newEnemy.GetComponentInChildren<Enemy>().DontRespawn = true;
            }
        }
	}
}
