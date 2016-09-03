using UnityEngine;
using System.Collections;

public class FinallBosslevelChanger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>()) 
        {
            FinalBoss.FinalStage = false;
        }
    }
}
