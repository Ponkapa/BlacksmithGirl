using UnityEngine;
using System.Collections;

public class LightEffect : MonoBehaviour {
    
    // Use this for initialization
    void Start(){
        Invoke("ded", 0.25f);
    }
    void ded()
    {
        Destroy(gameObject);
    }	
}
