using UnityEngine;

public class ChangeAlphaOnCollision : MonoBehaviour
{
    public GameObject planeObject;
    private void Start()
    {
        planeObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            planeObject.SetActive(false);
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            planeObject.SetActive(true);
            Debug.Log("Exited");
           
        }
    }

    //private void ChangeAlpha()
    //{
    //    Renderer planeRenderer = planeObject.GetComponent<Renderer>();

    //    if (planeRenderer != null)
    //    {
    //        Material material = planeRenderer.material;

    //        // Current color
    //        Color currentColor = material.color;

    //        // Set new alpha
    //        float newAlpha = 55f / 255f;

    //        // Set new color with updated alpha value
    //        material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
    //    }
    //    else
    //    {
    //        Debug.Log("Plane Renderer not found");
    //    }
    //}
}
