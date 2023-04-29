using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    private Vector2 pigeonVelocity;
    private float pigonSpeed = 5.0f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        rb.MovePosition(transform.position + input * Time.deltaTime *pigonSpeed);
    }
}
