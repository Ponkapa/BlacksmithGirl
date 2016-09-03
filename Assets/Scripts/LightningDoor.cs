using UnityEngine;
using System.Collections;

public class LightningDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerController.hasLightning)
        {
            Destroy(gameObject);
        }
    }
}
