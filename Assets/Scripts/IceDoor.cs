using UnityEngine;
using System.Collections;

public class IceDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (PlayerController.hasIce)
        {
            Destroy(gameObject);
        }
	}
}
