using UnityEngine;
using System.Collections;

public class FinalBossPlatform : MonoBehaviour {
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (PlayerController.hasEarth && PlayerController.hasFire && PlayerController.hasIce && PlayerController.hasLight && PlayerController.hasShadow && PlayerController.hasLightning)
        {
            animator.SetTrigger("HasAllPowers");
        }
	}
}
