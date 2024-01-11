using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public string weaponTag = "Weapon";
    public int hitsToDisableConstraints = 5;

    private int hitCount = 0;
    private Rigidbody enemyRigidbody;

    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        if (enemyRigidbody == null)
        {
            Debug.LogError("Rigidbody not found on the enemy!");
            enabled = false; // Disable the script if Rigidbody is missing
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(weaponTag))
        {
            hitCount++;
            Debug.Log(hitCount);

            if (hitCount >= hitsToDisableConstraints)
            {
                // Disable rigidbody constraints
                enemyRigidbody.constraints = RigidbodyConstraints.None;
                hitCount = 0;
                // Optionally, you can disable the collider to prevent further hits
                // GetComponent<Collider>().enabled = false;
            }
        }
    }
}
