using System.Collections.Generic;
using UnityEngine;

namespace Memenator
{
    public class BossReset : MonoBehaviour
    {
        public PlayerAttack pA;
        private List<string> expectedSequence = new List<string> { "Star", "Hashtag", "Seven", "Seven", "Eight", "Zero", "Hashtag" };
        private List<string> currentSequence = new List<string>();
        Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        void Update()
        {
            // Check for mouse clicks
            if (Input.GetMouseButtonDown(0) && pA.inFightMode && pA.currentWeapon != null)
            {
                // Shoot a ray from the camera through the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Perform a raycast to get the hit information
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit object has a collider and a tag
                    if (hit.collider != null && !string.IsNullOrEmpty(hit.collider.tag))
                    {
                        HandleSequence(hit.collider.tag);
                    }
                }
            }
        }

        private void HandleSequence(string tag)
        {
            // Log the clicked tag
            Debug.Log("Clicked: " + tag);

            // Check if the clicked tag matches the expected sequence
            if (expectedSequence.Count > 0 && tag == expectedSequence[0])
            {
                // Log the correct tag in the sequence
                Debug.Log("Correct!");

                // Remove the correct tag from the expected sequence
                expectedSequence.RemoveAt(0);

                // Add the tag to the current sequence
                currentSequence.Add(tag);

                // Check if the sequence is complete
                if (expectedSequence.Count == 0)
                {
                    // Log the complete sequence
                    Debug.Log("Sequence Complete!");
                    rb.constraints = RigidbodyConstraints.None;
                    // Clear the current sequence for the next round
                    currentSequence.Clear();
                }
            }
            else
            {
                // Log an incorrect tag
                Debug.Log("Incorrect!");
            }
        }

    }
}
