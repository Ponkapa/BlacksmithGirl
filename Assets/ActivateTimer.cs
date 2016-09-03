using UnityEngine;
using System.Collections;

public class ActivateTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.GetChild(0).gameObject.SetActive(Timer.isEnabled);
	}
}
