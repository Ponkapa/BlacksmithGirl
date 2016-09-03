using UnityEngine;
using System.Collections;

public class EarthParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("ded", 3);
	}
	
    void ded()
    {
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
