using UnityEngine;
using System.Collections;

public class NPCSpawner : MonoBehaviour
{
    public int Timer;
    public bool Spawnable = true;
    public GameObject npc;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, Timer) == 1)
        {
            if (Spawnable)
            {
                GameObject newNpc = Instantiate(npc, transform.position, Quaternion.identity) as GameObject;
                newNpc.GetComponentInChildren<SpriteRenderer>().sortingOrder = Random.Range(0, 100) * 2;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Random.Range(0, 4) == 1 && other.GetComponentInChildren<NPC>().CanBeDeleted)
        {
            Destroy(other.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Spawnable = false;
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Spawnable = true;
    }
}