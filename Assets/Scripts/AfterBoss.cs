using UnityEngine;
using System.Collections;

public class AfterBoss : MonoBehaviour {
    public GameObject[] gameObjects;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Enemy.BossDead())
        {
            for(int i = 0; i<gameObjects.Length; i++)
            {
                if (gameObjects[i])
                {
                    gameObjects[i].SetActive(true);
                }
            }
        }
	}
}
