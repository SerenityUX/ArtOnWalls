using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    // Public field to assign the material in the inspector
    public Material newMaterial;

    // Public field to assign the spray texture prefab in the inspector
    public GameObject sprayTexturePrefab;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collider belongs to a spray can
        if (collision.gameObject.CompareTag("SprayCan"))
        {
            // Instantiate the spray texture prefab at the cube's position with default rotation
            if (sprayTexturePrefab != null)
            {
                GameObject instantiatedPrefab = Instantiate(sprayTexturePrefab, transform.position, Quaternion.identity, transform);

                // Check if the instantiated prefab has a Renderer component and apply the new material
                Renderer renderer = instantiatedPrefab.GetComponent<Renderer>();
                if (renderer != null && newMaterial != null)
                {
                    renderer.material = newMaterial;
                }
            }
        }
    }
}
