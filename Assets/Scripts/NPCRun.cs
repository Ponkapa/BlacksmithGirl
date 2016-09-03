using UnityEngine;
using System.Collections;

public class NPCRun : MonoBehaviour {
    public NPC npc;
	// Use this for initialization
	void Start () {
        npc = GetComponentInChildren<NPC>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            if (npc.state == NPC.State.Standing)
            {
                npc.SwitchMoving();
            }
            if (npc.facing == NPC.Facing.Left && (transform.position.x - other.gameObject.transform.position.x) > 0)
            {
                npc.SwitchFacing();
            }
            else if (npc.facing == NPC.Facing.Right && (transform.position.x - other.gameObject.transform.position.x) < 0)
            {
                npc.SwitchFacing();
            }
        }
    }
}
