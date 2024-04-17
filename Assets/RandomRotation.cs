using UnityEngine;

public class RandomYRotation : MonoBehaviour
{
    void Start()
    {
        // Generate a random rotation around the Y axis
        float randomYRotation = Random.Range(0f, 360f);

        // Apply the rotation to the object
        transform.rotation = Quaternion.Euler(0f, 0f, randomYRotation);
    }
}
