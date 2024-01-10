using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class RangedAttack : MonoBehaviour
    {

        private Camera mainCamera;
        private Vector3 mousePos;
        
        
        void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        }

        
        void Update()
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 rotation = mousePos - transform.position;

            float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
            
        }
    }

