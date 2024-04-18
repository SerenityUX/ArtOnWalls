using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    // Method to change the material of the cube's Renderer
    public void ChangeMaterial(Material newMaterial)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
            Debug.Log("Material changed to: " + newMaterial.name);
        }
        else
        {
            Debug.Log("No renderer found on the object.");
        }
    }
}
