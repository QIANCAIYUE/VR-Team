using UnityEngine;

public class LightonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundOn;

    // 公开函数，事件下拉框就能找到 PlayLightOnSound（和课上PlayGrabSound一样）
    public void PlayLightOnSound()
    {
        audioSource.PlayOneShot(soundOn);
    }
}