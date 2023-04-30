using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    public GuardDetect guard;
    private Vector2 pigeonVelocity;
    private float pigeonSpeed = 5.0f;
    private Rigidbody2D rb;
    public float durationTimer = 0f;
    public GetCooldown dashCooldown;
    public GetCooldown speedCooldown;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
