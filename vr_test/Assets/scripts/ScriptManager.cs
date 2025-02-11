using UnityEngine;
using System;

public class ScriptManager : MonoBehaviour
{
    /// <summary>
    /// ��̬ΪĿ��������ָ���ű�
    /// </summary>
    /// <param name="target">��Ҫ��ӽű���Ŀ�� GameObject</param>
    /// <param name="scriptName">�ű����������ƣ����������ռ䣬����еĻ���</param>
    public void AddScript(GameObject target, string scriptName)
    {
        if (target == null || string.IsNullOrEmpty(scriptName))
        {
            Debug.LogError("Ŀ������ű����Ʋ���Ϊ�գ�");
            return;
        }

        // ��ȡ�ű�����
        Type scriptType = Type.GetType(scriptName);
        if (scriptType == null)
        {
            Debug.LogError($"�޷��ҵ��ű� {scriptName}����ȷ���ű�������ȷ�Ҵ�������Ŀ�У�");
            return;
        }

        // ���Ŀ������Ƿ��Ѱ����ýű�
        if (target.GetComponent(scriptType) != null)
        {
            Debug.Log($"Ŀ����� {target.name} �Ѿ������ű� {scriptName}��������ӡ�");
            return;
        }

        // ��ӽű���Ŀ�����
        target.AddComponent(scriptType);
        Debug.Log($"�ű� {scriptName} �ѳɹ���ӵ����� {target.name}��");
    }

    /// <summary>
    /// ����Ŀ��ű�������
    /// </summary>
    /// <param name="target">Ŀ�� GameObject</param>
    /// <param name="scriptName">�ű�����������</param>
    /// <param name="propertyName">��Ҫ���õ���������</param>
    /// <param name="value">���Ե���ֵ</param>
    public void SetScriptProperty(GameObject target, string scriptName, string propertyName, object value)
    {
        if (target == null || string.IsNullOrEmpty(scriptName) || string.IsNullOrEmpty(propertyName))
        {
            Debug.LogError("Ŀ����󡢽ű����ƻ��������Ʋ���Ϊ�գ�");
            return;
        }

        // ��ȡ�ű�����
        Type scriptType = Type.GetType(scriptName);
        if (scriptType == null)
        {
            Debug.LogError($"�޷��ҵ��ű� {scriptName}����ȷ���ű�������ȷ�Ҵ�������Ŀ�У�");
            return;
        }

        // ��ȡĿ������ϵĽű�ʵ��
        Component component = target.GetComponent(scriptType);
        if (component == null)
        {
            Debug.LogError($"Ŀ����� {target.name} δ�����ű� {scriptName}��");
            return;
        }

        // ͨ�������ȡ������Ϣ
        var property = scriptType.GetProperty(propertyName);
        if (property == null)
        {
            Debug.LogError($"�ű� {scriptName} �в��������� {propertyName}��");
            return;
        }

        // ��������ֵ
        try
        {
            property.SetValue(component, value);
            Debug.Log($"�ɹ����ű� {scriptName} ������ {propertyName} ����Ϊ {value}��");
        }
        catch (Exception e)
        {
            Debug.LogError($"�޷��������� {propertyName} ��ֵ��{e.Message}");
        }
    }
}
