using UnityEngine;

public class ShakeDetector : MonoBehaviour
{
    public AudioSource[] audioSources; // An array to hold multiple AudioSource components
    public float shakeThreshold = 2.0f; // Threshold of shake intensity to trigger sound
    public float resetTime = 0.5f; // Time after which shake can be re-triggered
    private float lastShakeTime = 0;

    void Update()
    {
        // Calculate shake intensity as an example
        float shakeIntensity = (Input.acceleration - Physics.gravity).magnitude;

        // Check if the shake is hard enough and if enough time has passed since the last shake
        if (shakeIntensity > shakeThreshold && Time.time - lastShakeTime > resetTime)
        {
            PlayRandomSound(); // Play a random sound
            lastShakeTime = Time.time; // Reset the last shake time
        }
    }

    private void PlayRandomSound()
    {
        if (audioSources.Length > 0)
        {
            int randomIndex = Random.Range(0, audioSources.Length); // Get a random index
            AudioSource selectedAudioSource = audioSources[randomIndex]; // Select an AudioSource at random index
            if (selectedAudioSource != null && !selectedAudioSource.isPlaying)
            {
                selectedAudioSource.Play(); // Play the selected AudioSource
            }
        }
    }
}
