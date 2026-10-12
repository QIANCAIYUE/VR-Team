using UnityEngine;

public class DoorTriggerOpen : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 8f;
    [SerializeField] private AudioSource doorAudio;

    private Quaternion startRot;
    private Quaternion targetRot;
    private bool isOpened = false;

    void Start()
    {
        startRot = transform.localRotation;
        targetRot = Quaternion.Euler(0, openAngle, 0) * startRot;
    }

    //物体碰到触发器就开门
    private void OnTriggerEnter(Collider other)
    {
        if (isOpened) return;
        //XR VR手的碰撞体进入触发
        if(other.CompareTag("Player"))
        {
            isOpened = true;
            if(doorAudio != null)
            {
                doorAudio.Play();
            }
        }
    }

    void Update()
    {
        if(isOpened)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime * openSpeed);
        }
    }
}
