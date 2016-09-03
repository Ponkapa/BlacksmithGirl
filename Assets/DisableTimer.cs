using UnityEngine;
using System.Collections;

public class DisableTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Timer.isEnabled)
        {
            gameObject.SetActive(false);
        }
    }
}
