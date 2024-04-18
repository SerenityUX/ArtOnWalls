using UnityEngine;
using System.Collections;

public class PlaySoundAndDeactivate : MonoBehaviour
{
    public AudioSource audioSource; // Drag your AudioSource component here through the Unity Inspector
    public AudioClip[] audioClips; // Array of audio clips to choose from

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateAndDeactivate());
    }

    private IEnumerator ActivateAndDeactivate()
    {
        yield return new WaitForSeconds(3); // Wait for 5 seconds
        if (audioClips.Length > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Length); // Get a random index
            audioSource.clip = audioClips[randomIndex]; // Set the AudioSource clip to the randomly selected AudioClip
            audioSource.Play(); // Play the audio clip
            yield return new WaitForSeconds(audioSource.clip.length); // Wait for the clip to finish playing
            gameObject.SetActive(false); // Deactivate the GameObject
        }
    }
}
