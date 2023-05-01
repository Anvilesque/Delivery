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

    public bool isPatrolStopping;
    private float patrolStopTimer;
    private float patrolStopDuration;
    
    public bool isRotating;
    private bool isRotationSet;
    private Quaternion rotationPrev, rotationTarget;
    private float rotationTimer;
    private float rotationDuration;

    public bool isPatrolDelaying;
    private float patrolDelayTimer;
    private float patrolDelayDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        guardBody = GetComponent<Rigidbody2D>();
        guardAttack = GetComponent<GuardAttack>();
        patrolPoints = transform.parent.GetComponentInChildren<GuardPatrolPoints>().patrolPoints;
        currentPP = 0;
        nextPP = (currentPP + 1) % patrolPoints.Count;
        isPatrolling = false;
        isPatrolStopping = false;
        isRotating = false;
        isPatrolDelaying = true;
        
        ResetPatrolTimers();
        SetPatrolParameters();
        foreach (PatrolPoint point in patrolPoints)
        {
            point.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoints.Count == 1) return;
        if (guardAttack.isReadyToAttack) return;

        CheckDistToDest();
        if (patrolStopTimer >= patrolStopDuration)
        {
            isPatrolStopping = false;
            isRotating = true;
        }
        if (rotationTimer >= rotationDuration)
        {
            isRotationSet = false;
            isRotating = false;
            isPatrolDelaying = true;
        }
        if (patrolDelayTimer >= patrolDelayDuration)
        {
            isPatrolDelaying = false;
            ResetPatrolTimers();
            isPatrolling = true;
        }

        if (isPatrolling)
        {
            // Finds the angle using arctan and x/y-distances between next patrol point and transform.position
            Vector2 patrolDirection = Vector3.Normalize(patrolPoints[nextPP].transform.position - transform.position);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, patrolDirection);
            guardBody.velocity = patrolDirection * patrolSpeed;
            return;
        }
        else
        {
            guardBody.velocity = Vector2.zero;

            if (isPatrolStopping)
            {
                patrolStopTimer += Time.deltaTime;
                return;
            }
            else if (isRotating)
            {
                if (!isRotationSet)
                {
                    SetRotation();
                }
                transform.rotation = Quaternion.Slerp(rotationPrev, rotationTarget, rotationTimer / rotationDuration);
                rotationTimer += Time.deltaTime;
                return;
            }
            else if (isPatrolDelaying)
            {
                patrolDelayTimer += Time.deltaTime;
                return;
            }
        }
    }

    void CheckDistToDest()
    {
        if (Mathf.Abs(transform.position.x - patrolPoints[nextPP].transform.position.x) <= 0.05 &&
        Mathf.Abs(transform.position.y - patrolPoints[nextPP].transform.position.y) <= 0.05)
        {
            isPatrolling = false;
            isPatrolStopping = true;
            guardBody.transform.position = patrolPoints[nextPP].transform.position;
            currentPP = nextPP;
            nextPP = (currentPP + 1) % patrolPoints.Count;
            SetPatrolParameters();
        }
    }

    public void ResetPatrolTimers()
    {
        patrolStopTimer = 0f;
        rotationTimer = 0f;
        patrolDelayTimer = 0f;
    }

    void SetPatrolParameters()
    {
        patrolSpeed = patrolPoints[currentPP].patrolSpeed;
        patrolStopDuration = patrolPoints[currentPP].patrolStopDuration;
        rotationDuration = patrolPoints[currentPP].rotationDuration;
        patrolDelayDuration = patrolPoints[currentPP].patrolDelayDuration;
    }

    void SetRotation()
    {
        Vector2 patrolDirection = Vector3.Normalize(patrolPoints[nextPP].transform.position - transform.position);
        rotationPrev = transform.rotation;
        rotationTarget = Quaternion.LookRotation(Vector3.forward, patrolDirection);
        isRotationSet = true;
    }
}
