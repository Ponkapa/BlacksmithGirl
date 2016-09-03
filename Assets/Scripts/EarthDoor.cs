using UnityEngine;
using System.Collections;

public class EarthDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerController.hasEarth)
        {
            Destroy(gameObject);
        }
    }
}
