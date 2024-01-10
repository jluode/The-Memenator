using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    #region public Variables
    public Transform raycast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float movespeed;
    public float timer;
    #endregion

    #region private variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion

    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (inRange)
        {
            anim.SetBool("Attack", true);
            Debug.Log(anim.GetBool("Attack"));
            //hit = Physics2D.Raycast(raycast.position, Vector2.left, rayCastLength, raycastMask);
            //RaycastDebugger();
        }

        if(hit.collider != null)
        {
            //EnemyLogic();
        }

        else if (hit.collider == null) 
        {
            //inRange = false;
        }

        if(!inRange)
        {
            anim.SetBool("Attack", false);
            //StopAttack();
        }
    }

    private void OnTriggerEnter(Collider trig)
    {
        if(trig.CompareTag("Player"))
        {
            target = trig.gameObject;
            inRange = true;
            Debug.Log(target);
        }
    }
    private void OnTriggerExit(Collider trig)
    {
        if (trig.CompareTag("Player"))
        {
            target = trig.gameObject;
            inRange = false;
            Debug.Log(target);
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance < attackDistance)
        {
            Move();
            StopAttack();
            Debug.Log("Attack");
        }

        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
            Debug.Log("Attack");
        }

        if(cooling) 
        {
            anim.SetBool("Attack", false);
            Debug.Log("Attack3");

        }
    }

    void Move()
    {
        anim.SetBool("CanWalk", true);

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("NEWGrumpyCatattack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movespeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("CanWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * rayCastLength, Color.red);
            Debug.Log(distance);
        }

        else if (attackDistance > distance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }
}