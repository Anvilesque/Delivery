using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDetect : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D guardCollider;
    private GuardProximityCircle guardCircle;
    private GameObject pigeon;
    private LayerMask playerLayer;
    private float rotation;
    public float detectRadius = 2f;
    public bool isPigeonDetected;
    // private float detectConeAngle = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        guardCollider = GetComponent<BoxCollider2D>();
        guardCircle = transform.GetComponentInChildren<GuardProximityCircle>();
        rotation = gameObject.transform.rotation.z;
        
        pigeon = FindObjectOfType<PigeonMovement>().gameObject;
        playerLayer = LayerMask.NameToLayer("Player");
        isPigeonDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotation = gameObject.transform.rotation.z;
        if (!guardCircle.isNearPlayer) {}
        else
        {
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * rotation), Mathf.Sin(Mathf.Deg2Rad * rotation));
            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, detectRadius, playerLayer);
            // Debug.Log(ray.collider);
            if (ray.collider == pigeon.GetComponent<Collider2D>())
            {
                isPigeonDetected = true;
            }
            else
            {
                isPigeonDetected = false;
            }
        }
    }

    bool PigeonInConeAngle()
    {
        return true;
    }
}
