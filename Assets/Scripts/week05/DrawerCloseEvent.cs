using UnityEngine;
using UnityEngine.Events;

public class DrawerCloseEvent : MonoBehaviour
{
    [Header("抽屉关闭事件")]
    public UnityEvent OnClose;

    public void TriggerClose()
    {
        OnClose?.Invoke();
    }
}

