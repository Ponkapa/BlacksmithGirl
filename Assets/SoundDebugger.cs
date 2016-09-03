using UnityEngine;
using System.Collections;

public class SoundDebugger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<AudioSource>().Play();
        }
	}
}
