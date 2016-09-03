using UnityEngine;
using System.Collections;

public class AggroScript : MonoBehaviour {
    public Enemy enemy;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = enemy.gameObject.transform.position;
	}


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.aggro = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.aggro = false;
        }
    }
}
