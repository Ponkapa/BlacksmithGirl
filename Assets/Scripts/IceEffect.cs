using UnityEngine;
using System.Collections;

public class IceEffect : MonoBehaviour {
    public int i;
    private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        i = 100;
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Color temp = sr.color;
        temp.a-= 0.05f;
        sr.color = temp;
        i--;
        if (i <= 0)
        {
            Destroy(gameObject);
        }
	}
}
