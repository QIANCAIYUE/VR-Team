using UnityEngine;

public class LightSensor : MonoBehaviour
{
    // 保存灯组件
    private Light myLight;

    void Start()
    {
        // 获取当前物体上的Light组件
        myLight = GetComponent<Light>();
        // 默认灯关闭
        myLight.enabled = false;
    }

    // 当玩家进入触发器范围
    private void OnTriggerEnter(Collider other)
    {
        // 检测进来的物体标签是Player（玩家）
        if (other.CompareTag("Player"))
        {
            myLight.enabled = true;
        }
    }

    // 玩家离开触发器范围
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myLight.enabled = false;
        }
    }
}