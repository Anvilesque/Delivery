using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    private Rigidbody2D guardBody;
    private List<PatrolPoint> patrolPoints;
    public bool isPatrolling;
    public int currentPP;   // PP = patrol point
    public int nextPP;
    public float patrolSpeed;
    public float patrolStopTimer;
    public float patrolStopDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        guardBody = GetComponent<Rigidbody2D>();
        patrolPoints = transform.parent.GetComponentInChildren<GuardPatrolPoints>().patrolPoints;
        currentPP = 0;
        nextPP = (currentPP + 1) % patrolPoints.Count;
        isPatrolling = false;
        patrolSpeed = 2f;
        patrolStopTimer = 0f;
        patrolStopDuration = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoints.Count == 1) return;
        CheckDistToDest();
        if (isPatrolling)
        {
            // Finds the angle using arctan and x/y-distances between next patrol point and transform.position
            float angle = Mathf.Atan((patrolPoints[nextPP].transform.position.y - transform.position.y) / (patrolPoints[nextPP].transform.position.x - transform.position.x));
            float sign = Mathf.Sign(patrolPoints[nextPP].transform.position.x - transform.position.x);
            Vector2 patrolDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * sign;
            guardBody.velocity = patrolDirection * patrolSpeed;
        }
        else
        {
            patrolStopTimer += Time.deltaTime;
        }

        if (patrolStopTimer >= patrolStopDuration)
        {
            patrolStopTimer = 0f;
            isPatrolling = true;
        }
    }

    void CheckDistToDest()
    {
        if (Mathf.Abs(transform.position.x - patrolPoints[nextPP].transform.position.x) <= 0.05 &&
        Mathf.Abs(transform.position.y - patrolPoints[nextPP].transform.position.y) <= 0.05)
        {
            isPatrolling = false;
            guardBody.velocity = Vector2.zero;
            guardBody.transform.position = patrolPoints[nextPP].transform.position;
            currentPP = nextPP;
            nextPP = (currentPP + 1) % patrolPoints.Count;
            patrolSpeed = patrolPoints[currentPP].patrolSpeed;
            patrolStopDuration = patrolPoints[currentPP].patrolStopDuration;
        }
    }

}
