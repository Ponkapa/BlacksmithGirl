using UnityEngine;
using System.Collections;

public class HitboxScript : MonoBehaviour {
    public int damage;
    public AudioClip audioClip;
    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (audioClip)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }
}
