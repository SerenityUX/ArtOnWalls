using UnityEngine;

public class ProximityBasedMaterialChanger : MonoBehaviour
{
    [System.Serializable]
    public class ProximityPair
    {
        public GameObject objectToCheck; // The first object to monitor
        public GameObject targetObject; // The second object to monitor
        public Material proximityMaterial; // Material to apply when these two are in proximity
        public float proximityThreshold = 5f; // Threshold distance to trigger the material change
    }

    public ProximityPair[] proximityPairs; // Array of proximity pairs
    public GameObject targetPrefab; // The prefab that will have its material changed

    private Renderer prefabRenderer; // Renderer of the target prefab

    void Start()
    {
        if (targetPrefab != null)
        {
            prefabRenderer = targetPrefab.GetComponent<Renderer>();
            if (prefabRenderer == null)
            {
                Debug.LogError("Renderer component is not found on the target prefab.");
            }
        }
        else
        {
            Debug.LogError("Target prefab is not assigned.");
        }
    }

    void Update()
    {
        bool materialChanged = false;

        foreach (var pair in proximityPairs)
        {
            if (pair.objectToCheck != null && pair.targetObject != null && prefabRenderer != null)
            {
                // Check proximity between the two specified objects, not the prefab
                if (Vector3.Distance(pair.objectToCheck.transform.position, pair.targetObject.transform.position) <= pair.proximityThreshold)
                {
                    prefabRenderer.material = pair.proximityMaterial;
                    materialChanged = true;
                    break; // Change material and exit loop once the first proximity condition is met
                }
            }
        }

        // Optional: Revert to some original material if no condition is met (if needed)
        if (!materialChanged && prefabRenderer != null)
        {
            // You can uncomment the next line if you want to revert to an original material
            // prefabRenderer.material = originalMaterial;
        }
    }
}
