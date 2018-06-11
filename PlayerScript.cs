using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject shovel;
    Vector2 mouseScreenPosition;
    Rigidbody2D rb;
    public  Paddle paddleScript;
    public GameObject prefrab;
    public bool moving = false;
    public float timeToSpawnWaypoint = .3f;
    public List<Vector2> wayPoints = new List<Vector2>();
    bool WaypointRoutineRunning = false;



    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        paddleScript = GameObject.FindWithTag("Shovel").gameObject.GetComponent<Paddle>();
    }
    // Use this for initialization
    void Start ()
    {
       
        //Saves Starting Point
        	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetMouseButton(0))
        //{
            //Gets the Mouse Position
            mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shovel.transform.position = mouseScreenPosition;
            // get direction you want to point at
            Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
            shovel.transform.rotation = new Quaternion(direction.x, direction.y, 0, 0);
            transform.up = direction;           
            Move();
        //}
            
        
        if (Input.GetMouseButtonDown(0))
        {
            moving = true;
            DeleteWayPoint();
            if (!WaypointRoutineRunning)
            {
                StartCoroutine(TimeBetweenTrail());
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            moving = false;
            StopCoroutine(TimeBetweenTrail());
            paddleScript.RefreshWaypoints();
           
        }
    }
  
    public void Move()
    {      
        rb.MovePosition(mouseScreenPosition + (Vector2)transform.forward * Time.deltaTime/100);
    }
    //This Function Times the Creation of Waypoints adjust the Global Variable time between waypoints to change timing
    public IEnumerator TimeBetweenTrail()
    {
        WaypointRoutineRunning = true;
        while (moving)
        {
            CreateWaypoint();
            yield return new WaitForSeconds(timeToSpawnWaypoint);
        }
        WaypointRoutineRunning = false;

    }
    //Creates the Waypoint and adds to the list
    public void CreateWaypoint()
    {
        Vector2 waypoint = gameObject.transform.position;
        wayPoints.Add(waypoint);
        Instantiate(prefrab,gameObject.transform.position,Quaternion.identity);
       
    }
    public void DeleteWayPoint()
    {
        List<GameObject> waypointsGameObject = new List<GameObject>();
        waypointsGameObject.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        foreach (GameObject waypoint in waypointsGameObject)
        {
           Destroy(waypoint);
        }
        wayPoints.Clear();
    }
   

}
