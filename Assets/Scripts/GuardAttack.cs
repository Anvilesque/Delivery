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
    public bool isAttackingPaused;
    private float attackStartDelayTimer;
    private float attackStartDelayDuration;
    private float attackTimer;
    private float attackDuration;
    private float attackStopDelayTimer;
    private float attackStopDelayDuration;

    public event EventHandler<EventArgs> StopAttacking;

    // Start is called before the first frame update
    void Start()
    {
        guardDetect = transform.parent.GetComponentInChildren<GuardDetect>();
        guardPatrol = GetComponent<GuardPatrol>();
        isReadyToAttack = false;
        isAttacking = false;
        isAttackingPaused = false;
        attackStartDelayTimer = 0f;
        attackStartDelayDuration = 1f;
        attackTimer = 0f;
        attackDuration = 3f;
        attackStopDelayTimer = 0f;
        attackStopDelayDuration = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= attackDuration)
        {
            if (attackStopDelayTimer < attackStopDelayDuration)
            {
                isAttackingPaused = true;
                attackStopDelayTimer += Time.deltaTime;
            }
            else ResetAttack();
        }
        if (guardDetect.isPigeonDetected) // yes see pigeon
        {
            isReadyToAttack = true;
            isAttackingPaused = false;
            attackStopDelayTimer = 0f;
            attackTimer = 0f;
        }
        else if (isAttacking) // no see pigeon, yes attacking--> increment attackTimer
        {
            attackTimer += Time.deltaTime;
        }
        else if (isReadyToAttack) // no see pigeon, no attacking, yes ready
        {
            if (attackStartDelayTimer >= attackStartDelayDuration)
            {
                isAttacking = true;
            }
            else
            {
                attackStartDelayTimer += Time.deltaTime;
            }
        }
    }

    void ResetAttack()
    {
        StopAttacking?.Invoke(this, EventArgs.Empty);
        attackStartDelayTimer = 0f;
        attackStopDelayTimer = 0f;
        attackTimer = 0f;
        isReadyToAttack = false;
        isAttacking = false;
        isAttackingPaused = false;
    }
}
