using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAttackChase : MonoBehaviour
{
    private GameObject pigeon;
    private Rigidbody2D guardBody;
    private GuardAttack guardAttack;
    private bool isPreparedChase;
    private Vector2 direction;
    public float chaseSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        pigeon = FindObjectOfType<PigeonMovement>().gameObject;
        guardBody = GetComponent<Rigidbody2D>();
        guardAttack = GetComponent<GuardAttack>();
        isPreparedChase = false;
        guardAttack.StopAttacking += StopChase;
    }

    // Update is called once per frame
    void Update()
    {
        if (guardAttack.isReadyToAttack)
        {
            if (!isPreparedChase)
            {
                guardBody.velocity = Vector2.zero;
                isPreparedChase = true;
            }
            UpdateDirection();
        }
        if (guardAttack.isAttacking) Chase();
    }

    void Chase()
    {
        UpdateDirection();
        guardBody.velocity = direction * chaseSpeed;
    }

    void UpdateDirection()
    {
        direction = Vector3.Normalize(pigeon.transform.position - transform.position);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    void StopChase(object sender, EventArgs e)
    {
        guardBody.velocity = Vector3.zero;
    }
}
