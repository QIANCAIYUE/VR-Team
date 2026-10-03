using UnityEngine;

public class TouchToggleLight : MonoBehaviour
{
    [SerializeField] private Light targetLight;
    [SerializeField] private float touchCooldown = 0.35f;

    private float nextTouchTime;

    private void Awake()
    {
        if (targetLight == null)
            targetLight = GetComponentInChildren<Light>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<HandTouchProbe>() == null)
            return;

        if (Time.time < nextTouchTime)
            return;

        nextTouchTime = Time.time + touchCooldown;

        if (targetLight != null)
            targetLight.enabled = !targetLight.enabled;
        else
            Debug.LogWarning("TouchToggleLight: Target Light is not assigned.", this);
    }
}