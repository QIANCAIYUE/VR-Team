using UnityEngine;
using UnityEngine.Events; // 新增这一行

public class TouchToggleLight : MonoBehaviour
{
    [SerializeField] private Light targetLight;
    [SerializeField] private float touchCooldown = 0.35f;

    // ========== 新增：两个事件（用来绑定音效，和课上Grabbable事件一样）==========
    public UnityEvent OnLightTurnOn;
    public UnityEvent OnLightTurnOff;
    // ==========================================================================

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
        {
            // 先记录灯现在的状态
            bool beforeState = targetLight.enabled;
            targetLight.enabled = !targetLight.enabled;

            // ========== 新增：判断开灯/关灯，触发对应事件 ==========
            if (!beforeState)
            {
                // 之前是关闭 → 现在开灯，触发开灯事件
                OnLightTurnOn?.Invoke();
            }
            else
            {
                // 之前是打开 → 现在关灯，触发关灯事件
                OnLightTurnOff?.Invoke();
            }
            // =====================================================
        }
        else
        {
            Debug.LogWarning("TouchToggleLight: Target Light is not assigned.", this);
        }
    }
}