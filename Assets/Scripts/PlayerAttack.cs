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
            // Fight mode p��lle ja pois F napilla
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

                // Jos ase esill�, hy�kk��
                if (currentWeapon != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Melee hy�kk�ys katanalla
                        if (currentWeapon == katana)
                        {
                            MeleeAttack();
                        }
                        // Ranged hy�kk�ys heittot�hdell�
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
        
        // Piilottaa t�m�nhetkisen aseen
        void DeactivateCurrentWeapon()
        {
            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
            }
        }


        // Melee hy�kk�ykselle on eri animaatiot, riippuen siit� kumpaan suuntaan pelaaja on menossa. Lis�ksi t�ss� m��ritell��n aseen colliderin aktivoitumisajaksi 1 sekunti
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
            // S�de (raycast) kameran l�pi n�yt�ll� n�kyv��n sijaintiin peliobjektissa. Kaikki mihin ei haluta klikkauksen reagoivan, pit�� laittaa IgnoreRaycast-layeriin
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Jos s�de osuu objektiin ruudulla:
            if (Physics.Raycast(ray, out hit))
            {
                // Luo heittot�hti clone
                GameObject ninjaStarClone = Instantiate(ninjaStarPrefab, currentWeapon.transform.position, Quaternion.identity);

                // Laske suunta pelaajasta s�teen osumaan
                Vector3 direction = (hit.point - currentWeapon.transform.position).normalized;

                // Voima jolla klooni ammutaan kohdetta kohti
                float launchForce = 20f;
                ninjaStarClone.GetComponent<Rigidbody>().AddForce(direction * launchForce, ForceMode.Impulse);

                // Tuhoa ammuttu heittot�hti m��ritetyn viiveen j�lkeen
                StartCoroutine(DestroyCloneAfterDelay(ninjaStarClone, 2f));
            }
        }


        // Fight mode p��ll� pelaaja muuttuu "suuttuneeksi" ja silloin voi valita aseen ja hy�k�t�
        void FightMode()
        {
            inFightMode = !inFightMode;
            animator.SetBool("FightMode", inFightMode);
            Debug.Log("FIGHT MODE ENGAGED!");

            // Jos poistut tappelutilasta, ase h�vi��
            if (!inFightMode)
            {
                DeactivateCurrentWeapon();  
                Debug.Log("FIGHT MODE OFF...");
            }
        }
        // T�m� aktivoi katanassa olevan colliderin hy�kk�yksen ajaksi, jotta katanan collider ei ole aina aktiivinen ja n�in ollen katana k�dess� voisi esim. vain hyp�t� tai juosta vihollista p�in
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