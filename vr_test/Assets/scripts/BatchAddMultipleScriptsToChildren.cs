using UnityEngine;
using System.Collections.Generic;

public class BatchAddMultipleScriptsToChildren : MonoBehaviour
{
    // 要添加的脚本类型列表（注意：脚本名必须和目标脚本的类名一致）
    public List<string> scriptNamesToAdd = new List<string>();

    // 指定目标父物体
    public GameObject targetParent;

    // 是否检查已存在的脚本，避免重复添加
    public bool avoidDuplicate = true;

    void Start()
    {
        if (targetParent == null)
        {
            Debug.LogError("请设置目标父物体！");
            return;
        }

        if (scriptNamesToAdd == null || scriptNamesToAdd.Count == 0)
        {
            Debug.LogError("请至少设置一个要添加的脚本！");
            return;
        }

        // 获取目标物体的所有子物体
        Transform[] children = targetParent.GetComponentsInChildren<Transform>();

        int addedCount = 0;

        foreach (Transform child in children)
        {
            // 跳过目标父物体本身，只处理子物体
            if (child == targetParent.transform)
            {
                continue;
            }

            foreach (string scriptName in scriptNamesToAdd)
            {
                // 检查是否已存在该脚本
                if (avoidDuplicate && child.gameObject.GetComponent(scriptName) != null)
                {
                    continue;
                }

                // 动态添加脚本
                System.Type scriptType = System.Type.GetType(scriptName);
                if (scriptType != null)
                {
                    child.gameObject.AddComponent(scriptType);
                    addedCount++;
                    Debug.Log($"已为子物体 {child.gameObject.name} 添加脚本 {scriptName}。");
                }
                else
                {
                    Debug.LogError($"无法找到名为 {scriptName} 的脚本，请确保脚本名正确！");
                }
            }
        }

        Debug.Log($"批量添加脚本完成，共为 {addedCount} 个脚本添加到子物体中。");
    }
}
