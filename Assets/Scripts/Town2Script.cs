using UnityEngine;
using System.Collections;

public class Town2Script : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
        {
            NarrationScript.Town2 = true;
        }
    }
}
