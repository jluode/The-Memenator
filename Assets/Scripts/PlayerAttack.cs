using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memenator
{
    public class PlayerAttack : MonoBehaviour
    {
        // Muuttujat aseille
        [SerializeField] GameObject katana;
        [SerializeField] GameObject ninjaStar;
        [SerializeField] GameObject katanaAttackCollider;
        public GameObject currentWeapon;
        [SerializeField] GameObject ninjaStarPrefab;

        Animator animator;

        public bool inFightMode = false;

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
            //Debug.Log(Input.mousePosition);
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
            // Säde (raycast) kameran läpi näytöllä näkyvään sijaintiin peliobjektissa. Kaikki mihin ei haluta klikkauksen reagoivan, pitää laittaa IgnoreRaycast-layeriin
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Jos säde osuu objektiin ruudulla:
            if (Physics.Raycast(ray, out hit))
            {
                // Luo heittotähti clone
                GameObject ninjaStarClone = Instantiate(ninjaStarPrefab, currentWeapon.transform.position, Quaternion.identity);

                // Laske suunta pelaajasta säteen osumaan
                Vector3 direction = (hit.point - currentWeapon.transform.position).normalized;

                // Voima jolla klooni ammutaan kohdetta kohti
                float launchForce = 20f;
                ninjaStarClone.GetComponent<Rigidbody>().AddForce(direction * launchForce, ForceMode.Impulse);

                // Tuhoa ammuttu heittotähti määritetyn viiveen jälkeen
                StartCoroutine(DestroyCloneAfterDelay(ninjaStarClone, 2f));
            }
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
        private IEnumerator DestroyCloneAfterDelay(GameObject clone, float delay)
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(delay);

            // Check if the clone still exists (it might have been destroyed by other means)
            if (clone != null)
            {
                // Destroy the clone
                Destroy(clone);
            }
        }
    }
}