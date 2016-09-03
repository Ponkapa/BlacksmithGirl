using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private Rigidbody2D rb;
    public int damage = 5;
    public bool parried = false;
    private GameObject target;
    public float projectileSpeed;
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().gameObject;
        Vector3 random = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
        rb.velocity = new Vector2((target.transform.position+random - rb.transform.position).normalized.x * projectileSpeed , (target.transform.position + random - rb.transform.position).normalized.y * projectileSpeed);
        Quaternion rotation;
          if (target.transform.position.x + random.x - transform.position.x < 0 && target.transform.position.y + random.y - transform.position.y > 0)
          {
              rotation = Quaternion.LookRotation((target.transform.position + random - transform.position), transform.TransformDirection(Vector3.right));
          }
          else if (target.transform.position.x + random.x - transform.position.x > 0 && target.transform.position.y + random.y - transform.position.y > 0)
          {
              rotation = Quaternion.LookRotation((target.transform.position + random - transform.position), transform.TransformDirection(Vector3.down));
          }
          else
          {
              rotation = Quaternion.LookRotation((target.transform.position + random - transform.position), transform.TransformDirection(Vector3.left));
          }
          

        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
	
	// Update is called once per frame
	void Update () {
        
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!parried)
        {
            Destroy(gameObject);
        }
        else
        {
            parried = false;
        }
    }
}
