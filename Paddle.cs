using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    PlayerScript player;
    
    public List <Vector2> waypoints = new List<Vector2>();
    public SnowShovelFillAmount snowFillImage;
    bool atEnd = false;
    bool notAtWaypoint = true;
    Rigidbody2D rb;
    public float speed = 1f;
    public int waypointAt = 0;
    Vector2 waypoint;
    BoxCollider2D colliderBox;
    public float timeToTurnOnCollider = 2f;
    public int countOfWaypoints;
    public float volume;
    public float maxVolume;

    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerScript>();
        colliderBox = gameObject.GetComponent<BoxCollider2D>();
        snowFillImage = GameObject.FindWithTag("Fill").gameObject.GetComponent<SnowShovelFillAmount>();
        rb = gameObject.GetComponent<Rigidbody2D>();    
    }
    private void Update()
    {
        if (player != null)
        {          
            if (!player.moving)
            {
                MoveToNextWayPoint();
            }         
        }
    }

    void MoveToNextWayPoint()
    {
             
        if (waypoints.Count > 0)
        {          
            Vector2 direction = waypoint - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * 400f;
            rb.velocity = transform.up * speed;
        }
       
      
    }
    public  void RefreshWaypoints()
    {

        waypoints = player.wayPoints;
        waypoint = player.wayPoints[0];
        countOfWaypoints = player.wayPoints.Count - 1;
        Invoke("ResetCollider", timeToTurnOnCollider);
        waypointAt = 0;
        rb.freezeRotation = false;
    }
    void DeleteWayPoints()
    {
        waypoints.Clear();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Waypoint")
        {
            if (countOfWaypoints != waypointAt)
            {
                waypointAt++;
                waypoint = waypoints[waypointAt];
                Destroy(collision.gameObject);

            }
            else
            {
                Destroy(collision.gameObject);
                DeleteWayPoints();
                StopMovement();
            }           
        }
        if (collision.CompareTag("Stop"))
        {
            DeleteWayPoints();
            StopMovement();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("HeavySnow") && volume <= maxVolume)
        {
            Snow snow = collision.gameObject.GetComponent<Snow>();
            volume += snow.fillAmount;
            snowFillImage.UpdateFillAmount(volume);
            Destroy(collision.gameObject);
        }
        if (collision.collider.CompareTag("Snow") && volume <= maxVolume)
        {
            Snow snow = collision.gameObject.GetComponent<Snow>();
            volume += snow.fillAmount;
            snowFillImage.UpdateFillAmount(volume);
            Destroy(collision.gameObject);
        }
    }  
    void StopMovement()
    {
        colliderBox.enabled = false;
        rb.velocity = new Vector2(0, 0);
        rb.freezeRotation = true;       
    }
    public void ResetCollider()
    {
        colliderBox.enabled = true;
    }
   
}
