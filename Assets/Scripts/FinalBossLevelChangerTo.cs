using UnityEngine;
using System.Collections;

public class FinalBossLevelChangerTo : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
        {
            FinalBoss.FinalStage = true;
        }
    }
}
