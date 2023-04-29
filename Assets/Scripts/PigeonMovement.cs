using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    private Vector2 pigeonVelocity;
    private float pigonSpeed = 5.0f;
    private Rigidbody2D rb;
    private int dashCooldown = 0;
    private int durationTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if(Input.GetButton("Fire1") && dashCooldown == 0)
        {
            pigonSpeed = 20.0f;
            dashCooldown = GetTime(4);
            durationTimer = 3;
        }
        rb.MovePosition(transform.position + input * Time.deltaTime *pigonSpeed);
        if(durationTimer == 0)
            pigonSpeed = 5.0f;
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
