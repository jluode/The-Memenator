using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol: MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform Vasen;
    [SerializeField] private Transform Oikea;

    [Header("Enemy")]
    [SerializeField] private Transform Enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    private void Awake()
    {
        initScale = Enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft) 
        {
            if (Enemy.position.x >= Vasen.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
            
        
        }

        else 
        {
            if (Enemy.position.x <= Oikea.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
            

        }
        
    }

    private void DirectionChange()
    {
        movingLeft = !movingLeft;
    }
    private void MoveInDirection(int _direction)
    {
        //Face direction
        Enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        //Move Direction
        Enemy.position = new Vector3(Enemy.position.x + Time.deltaTime * _direction * speed, Enemy.position.y, Enemy.position.z);

    }


    
}
