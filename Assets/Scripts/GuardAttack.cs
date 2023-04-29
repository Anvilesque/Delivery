using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAttack : MonoBehaviour
{
    private GuardDetect guardDetect;
    private GuardPatrol guardPatrol;
    public bool isReadyToAttack;
    public bool isAttacking;
    private float attackDelayTimer;
    private float attackDelayDuration;
    private float attackTimer;
    private float attackDuration;

    public event EventHandler<EventArgs> StopAttacking;

    // Start is called before the first frame update
    void Start()
    {
        guardDetect = GetComponentInChildren<GuardDetect>();
        guardPatrol = GetComponent<GuardPatrol>();
        isReadyToAttack = false;
        isAttacking = false;
        attackDelayTimer = 0f;
        attackDelayDuration = 1f;
        attackTimer = 0f;
        attackDuration = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= attackDuration)
        {
            ResetAttack();
        }
        if (guardDetect.isPigeonDetected) // yes see pigeon
        {
            isReadyToAttack = true;
            attackTimer = 0f;
        }
        else if (isAttacking) // no see pigeon, yes attacking--> increment attackTimer
        {
            attackTimer += Time.deltaTime;
        }
        else if (isReadyToAttack) // no see pigeon, no attacking, yes ready
        {
            if (attackDelayTimer >= attackDelayDuration)
            {
                isAttacking = true;
            }
            else
            {
                attackDelayTimer += Time.deltaTime;
            }
        }
    }

    void ResetAttack()
    {
        StopAttacking?.Invoke(this, EventArgs.Empty);
        attackDelayTimer = 0f;
        attackTimer = 0f;
        isReadyToAttack = false;
        isAttacking = false;
    }
}
