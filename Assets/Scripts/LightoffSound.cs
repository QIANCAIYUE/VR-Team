using UnityEngine;

public class LightoffSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundOff;

    // 公开函数，事件下拉框就能找到 PlayLightOffSound
    public void PlayLightOffSound()
    {
        audioSource.PlayOneShot(soundOff);
    }
}