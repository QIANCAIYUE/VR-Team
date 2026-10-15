using UnityEngine;

public class VRTouchDrawer : MonoBehaviour
{
    [Header("抽屉移动参数")]
    public Vector3 openOffset = new Vector3(0, 0, 0.4f);
    public float moveSpeed = 2.2f;

    private Vector3 closeLocalPos;
    private Vector3 openLocalPos;
    private bool isOpened = false;
    private bool handOnDrawer = false;

    void Start()
    {
        closeLocalPos = transform.localPosition;
        openLocalPos = closeLocalPos + openOffset;
    }

    // VR手（触发器）碰到抽屉
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VRHand") && !handOnDrawer)
        {
            handOnDrawer = true;
            isOpened = !isOpened; // 触摸切换开/关
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("VRHand"))
        {
            handOnDrawer = false;
        }
    }

    void Update()
    {
        Vector3 target = isOpened ? openLocalPos : closeLocalPos;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * moveSpeed);
    }
}
