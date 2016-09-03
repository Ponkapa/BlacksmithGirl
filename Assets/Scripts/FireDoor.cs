using UnityEngine;
using System.Collections;

public class FireDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if (PlayerController.gameComplete)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Invis()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void Ded()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D fire)
    {
        if (fire.gameObject.layer == 17)
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("Fire");
            Invoke("Invis", 0.9f);
            Invoke("Ded", 2f);
        }
    }
}
