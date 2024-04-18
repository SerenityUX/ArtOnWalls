using UnityEngine;

public class TriggerMaterialChange : MonoBehaviour
{
    // Material to apply when triggering the change
    public Material materialToApply;

    // Reference to the MaterialSwapper component on the cube
    public MaterialSwapper materialSwapper;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spray"))
        {
            if (materialSwapper != null && materialToApply != null)
            {
                materialSwapper.ChangeMaterial(materialToApply);
            }
            else
            {
                Debug.Log("MaterialSwapper component or material to apply not set.");
            }
        }
    }
}
