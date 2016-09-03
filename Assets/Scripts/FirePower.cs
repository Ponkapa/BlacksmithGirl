using UnityEngine;
using System.Collections;

public class FirePower : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        LevelLoader ll = GetComponent<LevelLoader>();
        if(ll.gainPower)
        {
            PlayerController.hasFire = true;
        }
    }
}
