using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
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
            StartCoroutine(ReturnToMinYAfterDelay(3f));
        }
    }

    private IEnumerator ReturnToMinYAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        float distanceToMinY = Mathf.Abs(transform.position.y - minY);
        float timeToMoveBack = distanceToMinY / moveSpeed;

        // Move the platform back to minY with defined moveSpeed
        Vector3 targetPosition = new Vector3(transform.position.x, minY, transform.position.z);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
