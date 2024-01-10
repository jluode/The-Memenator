using UnityEngine;

namespace Memenator
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public float jumpForce = 10f;

        public LayerMask groundLayer;

        private Rigidbody playerRigidbody;

        private bool isGrounded;

        private SpriteRenderer sr;

        private Transform katana;
        private Transform ninjaStar;

        public bool facingRight;
      
        private void Start()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            sr = GetComponent<SpriteRenderer>();

            katana = transform.Find("Katana");
            ninjaStar = transform.Find("NinjaStar");
        }

        private void Update()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 2.5f, groundLayer);

            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);

            // Pelaaja voi hyp‰t‰ vain ollessaan maassa
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            // K‰‰nt‰‰ pelaajan spriten liikkumissuunnan mukaisesti ja muuttaa aseen transformia niin, ett‰ se pysyy oikeassa k‰dess‰
            if (horizontalInput > 0f)
            {
                sr.flipX = false;
                FlipWeapon(false);  // P‰ivitt‰‰ aseen transformin oikealle p‰in kuljettaessa
                facingRight = true;
                //Debug.Log(facingRight);
            }
            else if (horizontalInput < 0f)
            {
                sr.flipX = true;
                FlipWeapon(true);   // P‰ivitt‰‰ aseen transformin vasemmalle p‰in kuljettaessa
                facingRight = false;
                //Debug.Log(facingRight);
            }
            

            void FlipWeapon(bool facingLeft)
            {
                if (katana != null)
                {
                    katana.localPosition = new Vector3(facingLeft ? -3f : -3f, -1f, 0f);
                    katana.localRotation = Quaternion.Euler(0f, 0f, facingLeft ? 70f : -70f);
                }
                if (ninjaStar != null)
                {
                    ninjaStar.localPosition = new Vector3(facingLeft ? 3f : 3f, -1.15f, 0f);
                    //ninjaStar.localRotation = Quaternion.Euler(0f, 0f, facingLeft ? 30f : -30f);
                }
            }
        }
    }
}


