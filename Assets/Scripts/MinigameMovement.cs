using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float pigeonSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        rb.MovePosition(transform.position + input * Time.deltaTime *pigeonSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Package")
        {
            Destroy(collision.gameObject);
            int balance = PlayerPrefs.GetInt("Money", 0);
            PlayerPrefs.SetInt("Money", balance + 5);
        }
    }
}
