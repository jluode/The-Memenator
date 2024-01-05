using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memenator
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] GameObject katana;
        [SerializeField] GameObject ninjaStar;

        [SerializeField] GameObject currentWeapon;
        
        Animator animator;

        bool inFightMode = false;
        

        void Start()
        {
        katana.SetActive(false);
        ninjaStar.SetActive(false);
        animator = GetComponent<Animator>();
        }


        void Update()
        {
            // Fight mode päälle ja pois F napilla
            if (Input.GetKeyDown(KeyCode.F))
            {
                FightMode();

            }
            if (inFightMode)
            {
                // Valitse / ota esiin ase numeroilla 1 & 2
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    EquipKatana();
 
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    EquipNinjaStar();
                }

                // Jos ase esillä, hyökkää
                if (currentWeapon != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Melee hyökkäys katanalla
                        if (currentWeapon == katana)
                        {
                            MeleeAttack();
                        }
                        // Ranged hyökkäys heittotähdellä
                        else if (currentWeapon == ninjaStar)
                        {
                            RangedAttack();
                        }
                    }
                }
            }

        }

        void EquipKatana()
        {
            // Deactivate the current weapon (if any)
            DeactivateCurrentWeapon();

            // Activate the Katana
            katana.SetActive(true);

            // Set the current weapon to the Katana
            currentWeapon = katana;
        }

        void EquipNinjaStar()
        {
            // Deactivate the current weapon (if any)
            DeactivateCurrentWeapon();

            // Activate the NinjaStar
            ninjaStar.SetActive(true);

            // Set the current weapon to the NinjaStar
            currentWeapon = ninjaStar;
        }

        void DeactivateCurrentWeapon()
        {
            // Deactivate the current weapon (if any)
            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
            }
        }

        void DestroyWeapon()
        {
            Destroy(currentWeapon);
        }


        void MeleeAttack()
        {
            Debug.Log("Melee attacked");
        }

        void RangedAttack()
        {
            Debug.Log("Range attacked");
        }
        void FightMode()
        {
            inFightMode = !inFightMode;
            animator.SetBool("FightMode", inFightMode);
            Debug.Log("FIGHT MODE ENGAGED!");

            // Jos poistut tappelutilasta, ase häviää
            if (!inFightMode)
            {
                DestroyWeapon();
                Debug.Log("FIGHT MODE OFF...");
            }
        }
    }
}