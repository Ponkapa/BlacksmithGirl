using UnityEngine;
using System.Collections;

public class IcePower : MonoBehaviour {
    public bool cleared = false;
    void Update()
    {
        LevelLoader ll = GetComponent<LevelLoader>();
        
        if (ll.gainPower)
        {
            if (!PlayerController.hasIce && !cleared)
            {
                FinalBoss.clearStages++;
                cleared = true;
            }
            PlayerController.hasIce = true;
        }
    }
}
