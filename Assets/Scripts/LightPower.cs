using UnityEngine;
using System.Collections;

public class LightPower : MonoBehaviour {
    public bool cleared = false;
    void Update()
    {
        LevelLoader ll = GetComponent<LevelLoader>();

        if (ll.gainPower)
        {
            if (!PlayerController.hasLight && !cleared)
            {
                FinalBoss.clearStages++;
                cleared = true;
            }
            PlayerController.hasLight = true;
        }
    }
}
