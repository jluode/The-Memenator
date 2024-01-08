using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memenator
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] GameObject katana;
        [SerializeField] GameObject ninjaStar;
        [SerializeField] GameObject katanaAttackCollider;
        [SerializeField] GameObject currentWeapon;
        [SerializeField] GameObject ninjaStarPrefab;
        
        Animator animator;

        bool inFightMode = false;

        public PlayerMovement pm;


        void Start()
        {
        katana.SetActive(false);
        ninjaStar.SetActive(false);
        animator = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        katanaAttackCollider.SetActive(false);
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
            DeactivateCurrentWeapon();
            katana.SetActive(true);
            katanaAttackCollider.SetActive(false);
            currentWeapon = katana;
        }

        void EquipNinjaStar()
        {
            DeactivateCurrentWeapon();
            ninjaStar.SetActive(true);
            currentWeapon = ninjaStar;
        }
        
        // Piilottaa tämänhetkisen aseen
        void DeactivateCurrentWeapon()
        {
            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
            }
        }

        //void DestroyWeapon()
        //{
        //    Destroy(currentWeapon);
        //}

        // Melee hyökkäykselle on eri animaatiot, riippuen siitä kumpaan suuntaan pelaaja on menossa. Lisäksi tässä määritellään aseen colliderin aktivoitumisajaksi 1 sekunti
        void MeleeAttack()
        {
            if (pm.facingRight)
            {
                animator.SetTrigger("Melee");
                StartCoroutine(ActivateColliderForDuration(1f));
            }
            else
            {
                animator.SetTrigger("MeleeLeft");
                StartCoroutine(ActivateColliderForDuration(1f));
            }

            
        }
        void RangedAttack()
        {
            // Get the mouse click position in the world space
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f;

            // Calculate the direction to the target position relative to the player's position
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Instantiate a clone of the NinjaStar prefab
            GameObject ninjaStarClone = Instantiate(ninjaStarPrefab, currentWeapon.transform.position, Quaternion.identity);

            // Apply force to launch the NinjaStar towards the target position
            float launchForce = 20f;  // You can adjust this value
            ninjaStarClone.GetComponent<Rigidbody2D>().AddForce(direction * launchForce, ForceMode2D.Impulse);
        }

        // Fight mode päällä pelaaja muuttuu "suuttuneeksi" ja silloin voi valita aseen ja hyökätä
        void FightMode()
        {
            inFightMode = !inFightMode;
            animator.SetBool("FightMode", inFightMode);
            Debug.Log("FIGHT MODE ENGAGED!");

            // Jos poistut tappelutilasta, ase häviää
            if (!inFightMode)
            {
                DeactivateCurrentWeapon();  
                Debug.Log("FIGHT MODE OFF...");
            }
        }
        // Tämä aktivoi katanassa olevan colliderin hyökkäyksen ajaksi, jotta katanan collider ei ole aina aktiivinen ja näin ollen katana kädessä voisi esim. vain hypätä tai juosta vihollista päin
        private IEnumerator ActivateColliderForDuration(float duration)
        {
            katanaAttackCollider.SetActive(true);

            yield return new WaitForSeconds(duration);

            katanaAttackCollider.SetActive(false);
        }
    }
}