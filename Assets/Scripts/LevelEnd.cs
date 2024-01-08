using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] GameObject fireworks;

    void Start()
    {
        fireworks.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowFireworks();
        }
    }
    void ShowFireworks()
    {
        fireworks.SetActive(true);
    }
}
