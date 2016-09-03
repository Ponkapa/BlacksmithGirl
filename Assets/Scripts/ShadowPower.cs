using UnityEngine;
using System.Collections;

public class ShadowPower : MonoBehaviour {
    public bool cleared = false;
    void Update()
    {
        LevelLoader ll = GetComponent<LevelLoader>();

        if (ll.gainPower)
        {
            if (!PlayerController.hasShadow && !cleared)
            {
                FinalBoss.clearStages++;
                cleared = true;
            }
            PlayerController.hasShadow = true;
        }
    }
}
