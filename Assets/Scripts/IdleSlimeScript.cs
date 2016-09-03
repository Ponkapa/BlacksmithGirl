using UnityEngine;
using System.Collections;

public class IdleSlimeScript : MonoBehaviour {
    private Enemy enemy;
    private BoxCollider2D bc;
    private CircleCollider2D cc;
	// Use this for initialization
	void Start () {
        enemy = GetComponent<Enemy>();
        bc = GetComponent<BoxCollider2D>();
        cc = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (enemy.state == Enemy.State.Idle && bc && cc)
        {
            bc.offset = new Vector2(bc.offset.x, -1.14f);
            bc.size = new Vector2(0.1f, 0.1f);
            cc.radius = 0;
        }
        else if (bc && cc)
        {
            bc.offset = new Vector2(bc.offset.x, -0.3077416f);
            bc.size = new Vector2(1.848375f, 1.329211f);
            cc.radius = 0.6882401f;
        }
	}
}
