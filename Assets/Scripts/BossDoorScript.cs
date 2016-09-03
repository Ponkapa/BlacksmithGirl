using UnityEngine;
using System.Collections;

public class BossDoorScript : MonoBehaviour
{
    public Enemy enemy;
    public RectTransform RightBar;
    public RectTransform LeftBar;
    public RectTransform MiddleBar;
    public GameObject HealthBar;
    public GameObject object1;
    public bool leftbool;
    public bool rightbool;
    public bool touching;
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!object1)
        {
            animator.SetTrigger("Done");
            HealthBar.SetActive(false);
        }
        if (touching && !rightbool && !leftbool && object1)
        {
            animator.SetTrigger("Clear");
            HealthBar.SetActive(true);
        }
        float currentFrac = (float)enemy.health / (float)enemy.MaxHealth;
        float lowFrac = (float)enemy.health / 5f;
        
            if (enemy.health >= enemy.MaxHealth)
            {
                RightBar.transform.localScale = new Vector3(1, 1, 1);
                LeftBar.transform.localScale = new Vector3(1, 1, 1);
                MiddleBar.transform.localScale = new Vector3(1, 1, 1);
            }
            if (enemy.health < enemy.MaxHealth)
            {
                RightBar.transform.localScale = new Vector3(0, 1, 1);
                LeftBar.transform.localScale = new Vector3(1, 1, 1);
                MiddleBar.transform.localScale = new Vector3(currentFrac, 1, 1);
            }
            if (enemy.health < 5)
            {
                RightBar.transform.localScale = new Vector3(0, 1, 1);
                LeftBar.transform.localScale = new Vector3(lowFrac, 1, 1);
                MiddleBar.transform.localScale = new Vector3(0, 1, 1);
            }
            if (enemy.health <= 0)
            {
                RightBar.transform.localScale = new Vector3(0, 1, 1);
                LeftBar.transform.localScale = new Vector3(0, 1, 1);
                MiddleBar.transform.localScale = new Vector3(0, 1, 1);
            }
        
    }
}