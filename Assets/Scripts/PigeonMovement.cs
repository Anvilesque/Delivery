using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    private Vector2 pigeonVelocity;
    private float pigeonSpeed = 5.0f;
    private Rigidbody2D rb;
    private int dashCooldown = 0;
    private int durationTimer = 0;
    private int sneakCooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if(Input.GetButton("Fire1") && dashCooldown == 0  && durationTimer == 0)
        {
            pigeonSpeed = 20.0f;
            dashCooldown = GetTime(5);
            durationTimer = 3;
        }
        else if(Input.GetButton("Fire2") && durationTimer == 0)
        {
            pigeonSpeed = 0;
        }
        else if(Input.GetButton("Fire3") && durationTimer == 0)
        {
            pigeonSpeed = 2.5f;
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
        if (durationTimer > 0)
            durationTimer--;
    }
}
