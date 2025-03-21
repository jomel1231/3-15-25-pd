using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HeatGunSound : MonoBehaviour
{
    private AudioSource heatGunAudio;
    private XRGrabInteractable grabInteractable;
    public InputActionProperty triggerPressAction; // Assign this in the Inspector

    private bool isHeld = false;

    void Start()
    {
        heatGunAudio = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Subscribe to Grab Events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void Update()
    {
        if (isHeld)
        {
            float triggerValue = triggerPressAction.action.ReadValue<float>(); // Get trigger press value
            if (triggerValue > 0.1f) // If trigger is pressed slightly
            {
                if (!heatGunAudio.isPlaying)
                    heatGunAudio.Play();
            }
            else
            {
                if (heatGunAudio.isPlaying)
                    heatGunAudio.Stop();
            }
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true; // Player is holding the heat gun
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        heatGunAudio.Stop(); // Stop sound when the tool is dropped
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
