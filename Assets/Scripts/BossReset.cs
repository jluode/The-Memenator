using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

namespace Memenator
{
    public class BossReset : MonoBehaviour
    {
        public PlayerAttack pA;
        private Dictionary<string, char> sequenceDigitMapping = new Dictionary<string, char>
        {
            { "Star", '*' },
            { "Hashtag", '#' },
            { "Seven", '7' },
            { "Eight", '8' },
            { "Zero", '0' }
            // Add more mappings as needed
        };

        private List<string> correctSequence = new List<string> { "Star", "Hashtag", "Seven", "Seven", "Eight", "Zero", "Hashtag" };
        private List<string> currentSequence = new List<string>();
        private Rigidbody rb;
        [SerializeField] GameObject blackScreen;
        [SerializeField] TextMeshPro digits;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            blackScreen.SetActive(false);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && pA.inFightMode && pA.currentWeapon != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && !string.IsNullOrEmpty(hit.collider.tag))
                    {
                        CodeSequence(hit.collider.tag);
                    }
                }
            }
        }

        private void CodeSequence(string tag)
        {
            if (correctSequence.Count > 0 && tag == correctSequence[0])
            {
                correctSequence.RemoveAt(0);
                currentSequence.Add(tag);

                UpdateDigitsText();

                if (correctSequence.Count == 0)
                {
                    Debug.Log("You Fu**ing did it!");
                    StartCoroutine(ShowBlackScreenWithDelay(1f));
                    StartCoroutine(HideDigitsWithDelay(1f));
                    rb.constraints = RigidbodyConstraints.None;
                }
            }
        }

        private void UpdateDigitsText()
        {
            if (digits != null)
            {
                if (sequenceDigitMapping.TryGetValue(currentSequence[currentSequence.Count - 1], out char mappedDigit))
                {
                    digits.text += mappedDigit;
                }
            }
        }
        private IEnumerator ShowBlackScreenWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            blackScreen.SetActive(true);
        }

        private IEnumerator HideDigitsWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            digits.gameObject.SetActive(false);
        }
    }
}
