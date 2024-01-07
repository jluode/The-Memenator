using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    
    
    
    public int maxHealth;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth < 0) 
        {
            gameObject.SetActive(false);


        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) 
        {
            Destroy(gameObject);
        }
    }




}