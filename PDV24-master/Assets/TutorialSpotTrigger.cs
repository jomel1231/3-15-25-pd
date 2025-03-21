using UnityEngine;

public class TutorialSpotTrigger : MonoBehaviour
{
    public AudioSource welcomeAudio;
    private bool hasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            welcomeAudio.Play();
            hasPlayed = true;
            Debug.Log("Welcome audio played!");
        }
    }
}
