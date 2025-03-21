using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioOnTrigger : XRBaseInteractable
{
    private AudioSource audioSource;
    private Renderer characterRenderer;
    private Color originalColor;
    
    // Choose your highlight color here.
    public Color highlightColor = Color.yellow;

    protected override void Awake()
    {
        base.Awake();
        // Cache the AudioSource component.
        audioSource = GetComponent<AudioSource>();
        
        // Cache the Renderer component (assumes the Renderer is on the same GameObject).
        characterRenderer = GetComponent<Renderer>();
        if (characterRenderer != null)
        {
            originalColor = characterRenderer.material.color;
        }
    }

    // Called when the trigger (activate action) is pressed while this object is hit.
    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        // Restart the audio: if already playing, stop it first.
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.Play();

        // Set the highlight on by changing the material color.
        if (characterRenderer != null)
        {
            characterRenderer.material.color = highlightColor;
        }
    }

    // Optionally, you can override OnDeactivated if you want to react to the trigger release,
    // but here we monitor the audio in Update.

    private void Update()
    {
        // When the audio stops playing, revert the character's color to the original.
        if (!audioSource.isPlaying && characterRenderer != null && characterRenderer.material.color == highlightColor)
        {
            characterRenderer.material.color = originalColor;
        }
    }
}
