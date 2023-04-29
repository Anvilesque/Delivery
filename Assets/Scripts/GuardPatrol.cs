using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    private Rigidbody2D guardBody;
    private GuardAttack guardAttack;
    public List<PatrolPoint> patrolPoints {get; private set;}
    public bool isPatrolling;
    public int currentPP {get; private set;}   // PP = patrol point
    public int nextPP {get; private set;}
    private float patrolSpeed;
    private float patrolStopTimer;
    private float patrolStopDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        guardBody = GetComponent<Rigidbody2D>();
        guardAttack = GetComponent<GuardAttack>();
        patrolPoints = transform.parent.GetComponentInChildren<GuardPatrolPoints>().patrolPoints;
        currentPP = 0;
        nextPP = (currentPP + 1) % patrolPoints.Count;
        isPatrolling = false;
        patrolStopTimer = 0f;
        patrolSpeed = patrolPoints[currentPP].patrolSpeed;
        patrolStopDuration = patrolPoints[currentPP].patrolStopDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoints.Count == 1) return;
        if (guardAttack.isReadyToAttack) return;
        CheckDistToDest();
        if (isPatrolling)
        {
            // Finds the angle using arctan and x/y-distances between next patrol point and transform.position
            Vector2 patrolDirection = Vector3.Normalize(patrolPoints[nextPP].transform.position - transform.position);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, patrolDirection);
            guardBody.velocity = patrolDirection * patrolSpeed;
        }
        else
        {
            guardBody.velocity = Vector2.zero;
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
            guardBody.transform.position = patrolPoints[nextPP].transform.position;
            currentPP = nextPP;
            nextPP = (currentPP + 1) % patrolPoints.Count;
            patrolSpeed = patrolPoints[currentPP].patrolSpeed;
            patrolStopDuration = patrolPoints[currentPP].patrolStopDuration;
        }
    }

}
