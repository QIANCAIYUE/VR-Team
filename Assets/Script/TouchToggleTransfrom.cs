using System.Collections;
using UnityEngine;

public class TouchToggleTransform : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 openLocalOffset;
    [SerializeField] private Vector3 openLocalRotationOffset;
    [SerializeField] private float moveDuration = 0.45f;
    [SerializeField] private float touchCooldown = 0.35f;

    private Vector3 closedLocalPosition;
    private Quaternion closedLocalRotation;
    private bool isOpen;
    private float nextTouchTime;
    private Coroutine movement;

    private void Awake()
    {
        if (target == null)
            target = transform;

        closedLocalPosition = target.localPosition;
        closedLocalRotation = target.localRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<HandTouchProbe>() == null)
            return;

        if (Time.time < nextTouchTime)
            return;

        nextTouchTime = Time.time + touchCooldown;
        isOpen = !isOpen;

        Vector3 destinationPosition = closedLocalPosition;
        Quaternion destinationRotation = closedLocalRotation;

        if (isOpen)
        {
            destinationPosition += openLocalOffset;
            destinationRotation = closedLocalRotation
                * Quaternion.Euler(openLocalRotationOffset);
        }

        if (movement != null)
            StopCoroutine(movement);

        movement = StartCoroutine(MoveTarget(destinationPosition, destinationRotation));
    }

    private IEnumerator MoveTarget(Vector3 destinationPosition,
        Quaternion destinationRotation)
    {
        Vector3 startPosition = target.localPosition;
        Quaternion startRotation = target.localRotation;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            target.localPosition = Vector3.Lerp(
                startPosition, destinationPosition, t);
            target.localRotation = Quaternion.Slerp(
                startRotation, destinationRotation, t);

            yield return null;
        }

        target.localPosition = destinationPosition;
        target.localRotation = destinationRotation;
        movement = null;
    }
}