using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    private Vector2 pigeonVelocity;
    private float pigonSpeed = 10.0f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pigeonVelocity.x = Input.GetAxis("Horizontal")*pigonSpeed;
        pigeonVelocity.y = Input.GetAxis("Vertical")*pigonSpeed;
        rb.velocity = pigeonVelocity;
    }
}
