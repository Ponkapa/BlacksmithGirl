using UnityEngine;
using System.Collections;

public class LoadTown1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerController.Reset();
        Application.LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
