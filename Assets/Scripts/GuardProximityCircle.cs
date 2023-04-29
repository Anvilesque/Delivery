using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardProximityCircle : MonoBehaviour
{
    private CircleCollider2D proxCollider;
    private GameObject pigeon;
    public bool isNearPlayer;

    // Start is called before the first frame update
    void Start()
    {
        proxCollider = GetComponent<CircleCollider2D>();
        proxCollider.radius = transform.parent.GetComponent<GuardDetect>().detectRadius;
        pigeon = FindObjectOfType<PigeonMovement>().gameObject;
        isNearPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == pigeon.GetComponent<Collider2D>())
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == pigeon.GetComponent<Collider2D>())
        {
            isNearPlayer = false;
        }
    }
}
