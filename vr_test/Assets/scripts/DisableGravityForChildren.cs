using UnityEngine;

public class DisableGravityForChildren : MonoBehaviour
{
    // 目标父物体
    public GameObject targetParent;

    void Start()
    {
        if (targetParent == null)
        {
            Debug.LogError("请设置目标父物体！");
            return;
        }

        // 获取所有子物体
        Rigidbody[] childRigidbodies = targetParent.GetComponentsInChildren<Rigidbody>();

        int updatedCount = 0;

        foreach (Rigidbody rb in childRigidbodies)
        {
            if (rb.useGravity)
            {
                rb.useGravity = false;
                updatedCount++;
                Debug.Log($"已取消子物体 {rb.gameObject.name} 的 Use Gravity 勾选。");
            }
        }

        Debug.Log($"批量更新完成，共取消了 {updatedCount} 个 Rigidbody 的 Use Gravity 选项。");
    }
}
