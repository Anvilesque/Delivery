using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    private List<GuardDetect> guardList;
    private Vector2 pigeonVelocity;
    private float pigeonSpeed = 5.0f;
    private Rigidbody2D rb;
    public float durationTimer = 0f;
    private GetCooldown dashCooldown;
    private GetCooldown speedCooldown;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashCooldown = GameObject.Find("DashCooldown").GetComponent<GetCooldown>();
        speedCooldown = GameObject.Find("SpeedCooldown").GetComponent<GetCooldown>();
        guardList = new List<GuardDetect>(FindObjectsOfType<GuardDetect>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GuardDetect guard in guardList)
            guard.detectRadius = 2f;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if(Input.GetButton("Dash") && dashCooldown.cooldownTimer <= 0 && durationTimer <= 0)
        {
            pigeonSpeed = 20.0f;
            dashCooldown.cooldownTimer = dashCooldown.cooldownTime;
            durationTimer = 3*Time.deltaTime;
        }
        else if(Input.GetButton("SpeedBoost") && speedCooldown.cooldownTimer <= 0 && durationTimer <= 0)
        {
            pigeonSpeed = 7.5f;
            durationTimer = 4f;
            speedCooldown.cooldownTimer = speedCooldown.cooldownTime;
        }
        else if(Input.GetButton("Sneak") && durationTimer <= 0) 
        {
            pigeonSpeed = 2.5f;
            foreach (GuardDetect guard in guardList)
                guard.detectRadius = 1f;
        }
        rb.MovePosition(transform.position + input * Time.deltaTime *pigeonSpeed);
        if(durationTimer <= 0)
            pigeonSpeed = 5.0f;
        durationTimer -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Receiver")
        {
            Destroy(collision.gameObject);
            Debug.Log("Package Delivered!");
        }
    }
}
