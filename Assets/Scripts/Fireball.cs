using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
    private Rigidbody2D rb;
    public Sprite Hit;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () { 
        rb.velocity = new Vector2(10 * -rb.gameObject.transform.localScale.x, 0);
	}
    void Destroy()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().sprite = Hit;
        Destroy(GetComponent<Collider2D>());
        rb.velocity = new Vector2(0, 0);
        Invoke("Destroy", 0.1f);
    }
}