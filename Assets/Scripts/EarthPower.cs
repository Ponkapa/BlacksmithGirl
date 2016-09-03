using UnityEngine;
using System.Collections;

public class EarthPower : MonoBehaviour {
    public bool cleared = false;
    void Update()
    {
        LevelLoader ll = GetComponent<LevelLoader>();

        if (ll.gainPower)
        {
            if (!PlayerController.hasEarth && !cleared)
            {
                FinalBoss.clearStages++;
                cleared = true;
            }
            PlayerController.hasEarth = true;
        }
    }
}
