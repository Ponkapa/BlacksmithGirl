using UnityEngine;
using System.Collections;

public class Smithy : MonoBehaviour {
    public GameObject Menu;
    public RectTransform[] Selection;
    public RectTransform Selector;
    public AudioClip audioClip;
    private AudioSource audioSource;
    public int selectint;
    private int lockout;
    public bool inMenu;
    public bool talking;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }
    void Update()
    {
        if (talking && hasPower())
        {
            if (inMenu)
            {
                if (hasPower())
                {
                    selectint = grabPowerIndex(PlayerController.currentPower);
                    if (selectint == 6)
                    {
                        selectint = 0;
                    }
                    for (int i = 0; i < Selection.Length; i++)
                    {
                        if (checkValid(i))
                        {
                            Selection[i].transform.GetChild(0).gameObject.SetActive(false);
                        }
                    }
                    writePower(selectint);
                    Selector.transform.position = Selection[selectint].transform.position;
                    if (lockout <= 0)
                    {
                        if (Input.GetAxis("Vertical") > 0.3f)
                        {
                            SelectIncrement();
                            lockout = 15;
                        }
                        else if (Input.GetAxis("Vertical") < -0.3f)
                        {
                            SelectDecrement();
                            lockout = 15;
                        }
                    }
                    else
                    {
                        lockout--;
                    }
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                toggle();
            }
        }
    }
    void toggle()
    {
        if (inMenu == true && hasPower())
        {
            writePower(selectint);
            Invoke("Smith", 0f);
        }
        inMenu = !inMenu;
        Menu.SetActive(inMenu);
    }
    void Smith()
    {
        Invoke("SmithHit", 0f);
        Invoke("SmithHit", 0.5f);
        Invoke("SmithHit", 1f);
        Invoke("SmithHit", 1.5f);
    }
    void SmithHit()
    {
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.Play();
    }
    void writePower(int power)
    {
        if (selectint == 0)
        {
            PlayerController.currentPower = "Fire";
        }
        else if (selectint == 1)
        {
            PlayerController.currentPower = "Ice";
        }
        else if (selectint == 2)
        {
            PlayerController.currentPower = "Earth";
        }
        else if (selectint == 3)
        {
            PlayerController.currentPower = "Lightning";
        }
        else if (selectint == 4)
        {
            PlayerController.currentPower = "Light";
        }
        else if (selectint == 5)
        {
            PlayerController.currentPower = "Shadow";
        }
        else
        {
            PlayerController.currentPower = "";
        }
    }
    int grabPowerIndex(string power)
    {
        if (power == "Fire")
        {
            return 0;
        }
        else if (power == "Ice")
        {
            return 1;
        }
        else if (power == "Earth")
        {
            return 2;
        }
        else if (power == "Lightning")
        {
            return 3;
        }
        else if (power == "Light")
        {
            return 4;
        }
        else if (power == "Shadow")
        {
            return 5;
        }
        else
        {
            return 6;
        }
    }
    bool hasPower()
    {
        for (int i = 0; i < Selection.Length; i++)
        {
            if (checkValid(i))
            {
                return true;
            }
        }
        return false;
    }
    void SelectIncrement()
    {
        bool valid = false;
        while (!valid)
        {
            if (selectint >= Selection.Length - 1)
            {
                selectint = 0;
            }
            else
            {
                selectint++;
            }
            valid = checkValid(selectint);
        }
        writePower(selectint);
    }
    void SelectDecrement()
    {
        bool valid = false;
        while (!valid)
        {
            if (selectint == 0)
            {
                selectint = Selection.Length - 1;
            }
            else
            {
                selectint--;
            }
            valid = checkValid(selectint);
        }
        writePower(selectint);
    }
    bool checkValid(int power)
    {
        if (power == 0)
        {
            if (PlayerController.hasFire)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (power == 1)
        {
            if (PlayerController.hasIce)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (power == 2)
        {
            if (PlayerController.hasEarth)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (power == 3)
        {
            if (PlayerController.hasLightning)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (power == 4)
        {
            if (PlayerController.hasLight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (power == 5)
        {
            if (PlayerController.hasShadow)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talking = true;
            other.GetComponent<PlayerController>().talking = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (inMenu)
            {
                toggle();
            }
            talking = false;
            other.GetComponent<PlayerController>().talking = false;
        }
    }
}
