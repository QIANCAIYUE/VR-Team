using UnityEngine;

public class DrawerSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip openClip;
    public AudioClip closeClip;

    public void PlayOpenSound()
    {
        if(audioSource != null && openClip != null)
        {
            audioSource.clip = openClip;
            audioSource.Play();
        }
    }

    public void PlayCloseSound()
    {
        if(audioSource != null && closeClip != null)
        {
            audioSource.clip = closeClip;
            audioSource.Play();
        }
    }
}
