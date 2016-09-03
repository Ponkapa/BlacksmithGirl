using UnityEngine;
using System.Collections;

public class LightningPower : MonoBehaviour {
    public bool cleared = false;
    void Update()
    {
        LevelLoader ll = GetComponent<LevelLoader>();

        if (ll.gainPower)
        {
            if (!PlayerController.hasLightning && !cleared)
            {
                FinalBoss.clearStages++;
                cleared = true;
            }
            PlayerController.hasLightning = true;
        }
    }
}
