using UnityEngine;
using System.Collections.Generic;

public class BatchAddMultipleScriptsToChildren : MonoBehaviour
{
    // Ҫ��ӵĽű������б�ע�⣺�ű��������Ŀ��ű�������һ�£�
    public List<string> scriptNamesToAdd = new List<string>();

    // ָ��Ŀ�길����
    public GameObject targetParent;

    // �Ƿ����Ѵ��ڵĽű��������ظ����
    public bool avoidDuplicate = true;

    void Start()
    {
        if (targetParent == null)
        {
            Debug.LogError("������Ŀ�길���壡");
            return;
        }

        if (scriptNamesToAdd == null || scriptNamesToAdd.Count == 0)
        {
            Debug.LogError("����������һ��Ҫ��ӵĽű���");
            return;
        }

        // ��ȡĿ�����������������
        Transform[] children = targetParent.GetComponentsInChildren<Transform>();

        int addedCount = 0;

        foreach (Transform child in children)
        {
            // ����Ŀ�길���屾��ֻ����������
            if (child == targetParent.transform)
            {
                continue;
            }

            foreach (string scriptName in scriptNamesToAdd)
            {
                // ����Ƿ��Ѵ��ڸýű�
                if (avoidDuplicate && child.gameObject.GetComponent(scriptName) != null)
                {
                    continue;
                }

                // ��̬��ӽű�
                System.Type scriptType = System.Type.GetType(scriptName);
                if (scriptType != null)
                {
                    child.gameObject.AddComponent(scriptType);
                    addedCount++;
                    Debug.Log($"��Ϊ������ {child.gameObject.name} ��ӽű� {scriptName}��");
                }
                else
                {
                    Debug.LogError($"�޷��ҵ���Ϊ {scriptName} �Ľű�����ȷ���ű�����ȷ��");
                }
            }
        }

        Debug.Log($"������ӽű���ɣ���Ϊ {addedCount} ���ű���ӵ��������С�");
    }
}
