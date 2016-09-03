using UnityEngine;
using System.Collections;

public class DisableNGPlus : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerController.NGPlus)
        {
            gameObject.SetActive(false);
        }
    }
}
