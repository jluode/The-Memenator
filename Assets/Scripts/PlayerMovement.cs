using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Jaakon placeholder pelaajan liikkumis-scripti. Tätä toivon mukaan tullaan vielä reilusti parantamaan,
    //Esim. ainakin hyoyn osalta

    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    private Rigidbody playerRigidbody;
    private bool isGrounded;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2.5f, groundLayer);

        
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

