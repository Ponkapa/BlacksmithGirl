using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    private PlayerController Player;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public static GameObject instance;
    public float yCutoff;
    // Use this for initialization
/*    void Awake()
    {
        if (instance == null)

            //if not, set instance to this
            instance = this.gameObject;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    */
    void Start () {
        Player = FindObjectOfType<PlayerController>();
	}
    // Update is called once per frame
    void Update()
    {
        Vector3 target = Player.gameObject.transform.position;
        target.z = -10;
        target.y += 1;
        if (FinalBoss.FinalStage)
        {
            target.y += 3;
            GetComponent<Camera>().orthographicSize = 12;
        }
        else
        {
            GetComponent<Camera>().orthographicSize = 8;
        }

        if (Player.PlayerFacingRight())
        {
            target.x += 3;
        }
        if (!Player.PlayerFacingRight())
        {
            target.x -= 3;
        }

        Vector3 point = gameObject.GetComponent<Camera>().WorldToViewportPoint(target);
        Vector3 delta = target - gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        if (destination.y < yCutoff)
        {
            destination.y = yCutoff;
        }
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        
    }


}
