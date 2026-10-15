using UnityEngine;
using UnityEngine.Events;

public class DrawerOpenEvent : MonoBehaviour
{
    [Header("抽屉打开事件")]
    public UnityEvent OnOpen;

    public void TriggerOpen()
    {
        OnOpen?.Invoke();
    }
}
