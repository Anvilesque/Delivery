using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrolPoints : MonoBehaviour
{
    public List<PatrolPoint> patrolPoints;

    // Start is called before the first frame update
    void Start()
    {
       patrolPoints = new List<PatrolPoint>(GetComponentsInChildren<PatrolPoint>());
    }
}
