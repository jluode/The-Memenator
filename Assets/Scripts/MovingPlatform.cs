using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Jaakon hissiscripti rakennustelineille

    public float moveSpeed = 5f; // Adjust the speed of the platform
    public float minY = 0f;      // Minimum Y position
    public float maxY = 10f;     // Maximum Y position

    private bool playerOnPlatform = false;
    private Rigidbody platformRb;

    void Start()
    {
        platformRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerOnPlatform)
        {
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(0f, verticalInput, 0f);
            MovePlatform(moveDirection);
        }
    }

    void MovePlatform(Vector3 moveDirection)
    {
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        platformRb.MovePosition(newPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
            Debug.Log("Player is on the platform");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
}
