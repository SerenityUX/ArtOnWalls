using UnityEngine;

public class SprayCan : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    public GameObject paintSplatPrefab; // Prefab to instantiate when the spray can hits the wall
    public LayerMask wallLayer; // Set this in the inspector to the layer your walls are on
    public Transform sprayNozzle; // The position where the spray originates
    public float maxRaycastDistance = 1f; // Maximum length of the raycast

    void Update()
    {
        // Check if the spray can is being used by checking if the ParticleSystem is active
        if (sprayParticles.gameObject.activeSelf && sprayParticles.isEmitting)
        {
            RaycastHit hit;
            // Cast a ray from the nozzle of the spray can
            if (Physics.Raycast(sprayNozzle.position, sprayNozzle.forward, out hit, maxRaycastDistance, wallLayer))
            {
                // Only place a paint splat if we hit the "Wall" layer
                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    // Instantiate the prefab at the hit point with the correct orientation
                    Instantiate(paintSplatPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                    Debug.Log("Painting on wall at " + hit.point);
                }
            }
        }
    }

    // Call this method to start/stop spraying by activating/deactivating the ParticleSystem GameObject
    public void SetSpraying(bool spraying)
    {
        sprayParticles.gameObject.SetActive(spraying);
    }
}
