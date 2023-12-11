using UnityEngine;

public class ChangeAlphaOnCollision : MonoBehaviour
{
    public GameObject planeObject; // Assign the plane GameObject in the Unity Editor

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeAlpha();
        }
    }

    private void ChangeAlpha()
    {
        Renderer planeRenderer = planeObject.GetComponent<Renderer>();

        if (planeRenderer != null)
        {
            Material material = planeRenderer.material;

            // Get the current color
            Color currentColor = material.color;

            // Set the new alpha value (55 / 255)
            float newAlpha = 55f / 255f;

            // Set the new color with the updated alpha value
            material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        }
        else
        {
            Debug.LogWarning("Plane Renderer not found on the specified GameObject.");
        }
    }
}
