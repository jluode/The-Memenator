using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    #region public Variables
    //public Transform raycast;
    //public LayerMask raycastMask;
    //public float rayCastLength = 5f;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public GameObject player;
    public GameObject followSpot;
    public float chaseDistance = 10.0f;
    public GameObject territory;
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
    private Transform playerTransform;
    #endregion

    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        
        //if (playerTransform != null)
        //{

        //   transform.position = Vector3.Lerp(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        //}
        //PerformRaycast();
        if (!attackMode)
        {
            //Move(playerTransform.position);
        }

        if (inRange)
        {
            anim.SetBool("Attack", true);
            //Debug.Log(anim.GetBool("Attack"));
            //hit = Physics2D.Raycast(raycast.position, Vector2.left, rayCastLength, raycastMask);
           // RaycastDebugger();
        }
        //Ray ray = new Ray(transform.position, Vector3.left);
        //RaycastHit hit;
       // if (Physics.Raycast(ray, out hit, rayCastLength, raycastMask))
        //{
            
        //    Debug.Log("Hit something: " + hit.collider.gameObject.name);
        //}
        // if(hit.collider != null)
        //{
        //EnemyLogic();
        //}
        
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
            //Debug.Log("target");


        }
    }
    private void OnTriggerExit(Collider trig)
    {
        if (trig.CompareTag("Player"))
        {
            target = trig.gameObject;
            inRange = false;
            //Debug.Log("targetOut");
        }
    }

    void EnemyLogic()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < attackDistance)
        {
            ///Move();
            StopAttack();
            //Debug.Log("Attack");
        }

        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
            //Debug.Log("Attack");
        }

        if(cooling) 
        {
            anim.SetBool("Attack", false);
            //Debug.Log("Attack3");

        }
    }

    //void PerformRaycast()
    //{
        //Vector3 localRaycastDirection = Vector3.right;
        //Vector3 worldRaycastDirection = transform.TransformDirection(localRaycastDirection);
        //Vector3 raycastDirection = transform.right * 1f;
        //Ray ray = new Ray(transform.position, Vector3.right);
        //RaycastHit hit;
        
        //if (Physics.Raycast(ray, out hit, rayCastLength, raycastMask))
        //{
            
          //  if (hit.collider.CompareTag("Player"))
            //{
              //  playerTransform = hit.collider.transform;
                //Move(hit.point);
            //}
        //}
    //}
    void Move(Vector3 playerPosition)
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        anim.SetBool("CanWalk", true);

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("NEWGrumpyCatattack"))
        {
            //Vector3 direction = (playerPosition - transform.position).normalized;
            //transform.Translate(direction * moveSpeed * Time.deltaTime);
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

   // void RaycastDebugger()
    //{
    //    if(distance > attackDistance)
   // {
   // Debug.DrawRay(raycast.position, Vector3.left * rayCastLength, Color.red);
   // Debug.Log(distance);
   // }

    //    else if (attackDistance > distance)
   //     {
    
   //     Debug.DrawRay(raycast.position, Vector3.left * rayCastLength, Color.green);
   //     }
   // }

    public void TriggerCooling()
    {
        cooling = true;
    }
}