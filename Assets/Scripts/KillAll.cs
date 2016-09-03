using UnityEngine;
using System.Collections;

public class KillAll : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().Health = 0;
            other.GetComponent<PlayerController>().kill();
        }
        if (other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().health = 0;
            other.GetComponent<Enemy>().kill();
        }
    }
}
