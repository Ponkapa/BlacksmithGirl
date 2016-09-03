using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if (PlayerController.gameComplete)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
