using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] GameObject fireworks;
    [SerializeField] Canvas canvas;

    void Start()
    {
        fireworks.SetActive(false);
        canvas.enabled = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowFireworks();
            canvas.enabled = true;
        }
    }
    void ShowFireworks()
    {
        fireworks.SetActive(true);
    }
}
