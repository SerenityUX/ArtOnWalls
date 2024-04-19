using System.Collections.Generic;
using UnityEngine;

public class ProximityColorChanger : MonoBehaviour
{
    [System.Serializable]
    public struct ObjectColorPair
    {
        public GameObject targetObject;
        public string hexColor;
        public AudioClip audioClip; // Audio clip to play for each target object
    }

    public ObjectColorPair[] targetObjectColors; // Array to hold objects and their associated colors and audio clips
    public ParticleSystem particleSystem; // Reference to the particle system
    public float proximityThreshold = 5f; // Proximity distance threshold
    public AudioSource audioSource; // Audio source to play clips

    private Dictionary<GameObject, Color> colorMap;
    private Dictionary<GameObject, AudioClip> audioMap;

    void Start()
    {
        // Initialize the dictionaries
        colorMap = new Dictionary<GameObject, Color>();
        audioMap = new Dictionary<GameObject, AudioClip>();

        // Assign colors and audio clips to objects
        foreach (var pair in targetObjectColors)
        {
            if (ColorUtility.TryParseHtmlString(pair.hexColor, out Color color))
            {
                colorMap[pair.targetObject] = color;
            }
            else
            {
                Debug.LogError("Invalid Hex Color for object " + pair.targetObject.name + ". Reverting to white.");
                colorMap[pair.targetObject] = Color.white;
            }

            // Map audio clips
            if (pair.audioClip != null)
            {
                audioMap[pair.targetObject] = pair.audioClip;
            }
            else
            {
                Debug.LogError("No audio clip provided for " + pair.targetObject.name);
            }
        }

        // Ensure the particle system is assigned
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
            if (particleSystem == null)
            {
                Debug.LogError("Particle System is not attached to the GameObject.");
            }
        }

        // Ensure the AudioSource is assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource is not attached to the GameObject.");
            }
        }
    }

    void Update()
    {
        foreach (var pair in targetObjectColors)
        {
            if (Vector3.Distance(transform.position, pair.targetObject.transform.position) <= proximityThreshold)
            {
                ChangeParticleSystemColor(colorMap[pair.targetObject]);
                PlayAudioClip(audioMap[pair.targetObject]);
                break; // Exit loop after color change and audio play to avoid unnecessary checks
            }
        }
    }

    // Function to change the particle system color
    void ChangeParticleSystemColor(Color newColor)
    {
        var mainModule = particleSystem.main;
        mainModule.startColor = newColor;
    }

    // Function to play the audio clip
    void PlayAudioClip(AudioClip clip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
