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
    private bool isTeleportingBack;
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
        if (guardAttack.isAttackingPaused) 
        {
            guardBody.velocity = Vector2.zero;
            return;
        }
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
        isTeleportingBack = true;
        StartCoroutine("TeleportBack", 1f);
    }

    IEnumerator TeleportBack(float duration)
    {
        float timer = 0f;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        Color finalMeshColor = mesh.material.color;
        GuardPatrol patrol = GetComponent<GuardPatrol>();
        Vector3 teleportPos = patrol.patrolPoints[patrol.currentPP].transform.position;
        while (timer <= (duration / 2))
        {
            timer += Time.deltaTime;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, (1 - timer / (duration / 2)));
            mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, finalMeshColor.a * (1 - timer / (duration / 2)));
            yield return null;
        }

        transform.position = teleportPos;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, (timer - (duration / 2)) / (duration / 2));
            mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, finalMeshColor.a * (timer - (duration / 2)) / (duration / 2));
            yield return null;
        }

        isTeleportingBack = false;
    }
}
