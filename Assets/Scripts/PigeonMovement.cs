using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    public GuardDetect guard;
    private Vector2 pigeonVelocity;
    private float pigeonSpeed = 5.0f;
    private Rigidbody2D rb;
    private int dashCooldown = 0;
    private int durationTimer = 0;
    private int speedCooldown = 0;
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
        if(Input.GetButton("Dash") && dashCooldown == 0  && durationTimer == 0)
        {
            pigeonSpeed = 20.0f;
            dashCooldown = GetTime(5);
            durationTimer = 3;
        }
        else if(Input.GetButton("SpeedBoost") && speedCooldown == 0 && durationTimer == 0)
        {
            pigeonSpeed = 7.5f;
            durationTimer = GetTime(4);
            speedCooldown = GetTime(20);
        }
        else if(Input.GetButton("Sneak") && durationTimer == 0)
        {
            pigeonSpeed = 2.5f;
            guard.detectRadius = 1f;
        }
        rb.MovePosition(transform.position + input * Time.deltaTime *pigeonSpeed);
        if(durationTimer == 0)
            pigeonSpeed = 5.0f;
        UpdateCooldowns();
    }

    public int GetTime(int seconds)
    {
        return seconds*60;
    }
    public void UpdateCooldowns()
    {
        if (dashCooldown > 0)
            dashCooldown--;
        if (speedCooldown > 0)
            speedCooldown--;
        if (durationTimer > 0)
            durationTimer--;
    }
}
