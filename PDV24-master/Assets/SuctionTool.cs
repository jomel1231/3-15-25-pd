using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SuctionTool : MonoBehaviour
{
    public InputActionProperty triggerAction; // Assign this in the Inspector
    private bool isAttached = false;
    private XRGrabInteractable screenInteractable;
    private Transform attachedScreen;
    private Rigidbody screenRb;

    void OnTriggerEnter(Collider other)
    {
        // Check if the suction tool touches the screen
        if (other.CompareTag("Screen") && !isAttached)
        {
            attachedScreen = other.transform;
            screenRb = attachedScreen.GetComponent<Rigidbody>();
            screenInteractable = attachedScreen.GetComponent<XRGrabInteractable>();
        }
    }

    void Update()
    {
        if (attachedScreen == null) return;

        // Attach the screen when the trigger is pressed
        if (!isAttached && triggerAction.action.WasPressedThisFrame())
        {
            AttachToScreen();
        }

        // Detach the screen when the trigger is released
        if (isAttached && triggerAction.action.WasReleasedThisFrame())
        {
            DetachFromScreen();
        }
    }

    void AttachToScreen()
    {
        if (attachedScreen == null) return;

        isAttached = true;

        // Disable screen grabbing while attached to the suction tool
        if (screenInteractable != null)
        {
            screenInteractable.enabled = false;
        }

        // Disable physics so it follows the suction tool smoothly
        if (screenRb != null)
        {
            screenRb.isKinematic = true;
            screenRb.useGravity = false;
        }

        // Parent screen to suction tool & align it
        attachedScreen.SetParent(transform);
        attachedScreen.localPosition = new Vector3(0, 0.01f, 0);
        attachedScreen.localRotation = Quaternion.identity;
    }

    void DetachFromScreen()
    {
        if (attachedScreen == null) return;

        isAttached = false;

        // Re-enable screen grabbing after detaching
        if (screenInteractable != null)
        {
            screenInteractable.enabled = true;
        }

        // Enable physics again so the screen behaves normally
        if (screenRb != null)
        {
            screenRb.isKinematic = false;
            screenRb.useGravity = true;
        }

        // Detach the screen
        attachedScreen.SetParent(null);
        attachedScreen = null;
    }
}