using UnityEngine;
using System.Collections;

public class LandingScript : MonoBehaviour {
    public PlayerController Player;
    private int GroundTimer;
    private int framesOffGround;
    private bool CanLand;
	// Use this for initialization
	void Start () {
        GroundTimer = 10;
        framesOffGround = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (framesOffGround < GroundTimer)
        {
            framesOffGround++;
    //        CanLand = false;
        }
        else
        {
            CanLand = true;
        }
	}

    
    void OnTriggerStay2D(Collider2D other)
    {
        if (CanLand)
        {
            Player.Grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        framesOffGround = 0;
        Player.Grounded = false;
    }
}
