using UnityEngine;
using System.Collections;

public class FinalBossDoorScript : MonoBehaviour {

    public FinalBoss finalBoss;
    public RectTransform RightBar;
    public RectTransform LeftBar;
    public RectTransform MiddleBar;
    public GameObject HealthBar;
    public GameObject object1;
    public bool leftbool;
    public bool rightbool;
    public bool touching;
    public bool Crossed = false;
    public bool notPlaying = false;
    private Animator animator;
    public AudioClip EvilLaugh;
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
        if (Enemy.checkRespawn || PlayerController.gameComplete)
        {
            animator.SetTrigger("Clear");
            animator.SetTrigger("Done");
        }
        if (touching && !rightbool && !leftbool && object1 && !Enemy.checkRespawn && !PlayerController.gameComplete)
        {
            animator.SetTrigger("Clear");
            HealthBar.SetActive(true);
            object1.SetActive(true);
            Crossed = true;
        }
        if (Crossed && !notPlaying)
        {
            AudioSource.PlayClipAtPoint(EvilLaugh, transform.position);
            if (Application.loadedLevel == 9)
            {
                FindObjectOfType<ManageMusic>().FinalBossFight();
            }
            notPlaying = true;
        }
        float currentFrac = (float)finalBoss.health / (float)finalBoss.MaxHealth;
        float lowFrac = (float)finalBoss.health / 5f;

        if (finalBoss.health >= finalBoss.MaxHealth)
        {
            RightBar.transform.localScale = new Vector3(1, 1, 1);
            LeftBar.transform.localScale = new Vector3(1, 1, 1);
            MiddleBar.transform.localScale = new Vector3(1, 1, 1);
        }
        if (finalBoss.health < finalBoss.MaxHealth)
        {
            RightBar.transform.localScale = new Vector3(0, 1, 1);
            LeftBar.transform.localScale = new Vector3(1, 1, 1);
            MiddleBar.transform.localScale = new Vector3(currentFrac, 1, 1);
        }
        if (finalBoss.health < 5)
        {
            RightBar.transform.localScale = new Vector3(0, 1, 1);
            LeftBar.transform.localScale = new Vector3(lowFrac, 1, 1);
            MiddleBar.transform.localScale = new Vector3(0, 1, 1);
        }
        if (finalBoss.health <= 0)
        {
            RightBar.transform.localScale = new Vector3(0, 1, 1);
            LeftBar.transform.localScale = new Vector3(0, 1, 1);
            MiddleBar.transform.localScale = new Vector3(0, 1, 1);
        }

    }
}
