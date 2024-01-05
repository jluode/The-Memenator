using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas canvas; // Assign the Canvas in the Unity Editor

    void Start()
    {
        // Disable the canvas at the start
        if (canvas != null)
        {
            canvas.enabled = false;
        }
        else
        {
            Debug.LogWarning("Canvas not assigned to the script.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            EnableCanvas();
        }
    }

    void Update()
    {
        // Check for the 'E' key press to disable the canvas
        if (Input.GetKeyDown(KeyCode.E))
        {
            DisableCanvas();
        }
    }

    void EnableCanvas()
    {
        if (canvas != null)
        {
            canvas.enabled = true;
        }
        else
        {
            Debug.LogWarning("Canvas not assigned to the script.");
        }
    }

    void DisableCanvas()
    {
        if (canvas != null)
        {
            canvas.enabled = false;
        }
        else
        {
            Debug.LogWarning("Canvas not assigned to the script.");
        }
    }
}
