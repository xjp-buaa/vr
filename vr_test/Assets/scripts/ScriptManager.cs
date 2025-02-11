using UnityEngine;
using System;

public class ScriptManager : MonoBehaviour
{
    /// <summary>
    /// 动态为目标对象添加指定脚本
    /// </summary>
    /// <param name="target">需要添加脚本的目标 GameObject</param>
    /// <param name="scriptName">脚本的完整名称（包括命名空间，如果有的话）</param>
    public void AddScript(GameObject target, string scriptName)
    {
        if (target == null || string.IsNullOrEmpty(scriptName))
        {
            Debug.LogError("目标对象或脚本名称不能为空！");
            return;
        }

        // 获取脚本类型
        Type scriptType = Type.GetType(scriptName);
        if (scriptType == null)
        {
            Debug.LogError($"无法找到脚本 {scriptName}，请确保脚本名称正确且存在于项目中！");
            return;
        }

        // 检查目标对象是否已包含该脚本
        if (target.GetComponent(scriptType) != null)
        {
            Debug.Log($"目标对象 {target.name} 已经包含脚本 {scriptName}，跳过添加。");
            return;
        }

        // 添加脚本到目标对象
        target.AddComponent(scriptType);
        Debug.Log($"脚本 {scriptName} 已成功添加到对象 {target.name}。");
    }

    /// <summary>
    /// 设置目标脚本的属性
    /// </summary>
    /// <param name="target">目标 GameObject</param>
    /// <param name="scriptName">脚本的完整名称</param>
    /// <param name="propertyName">需要设置的属性名称</param>
    /// <param name="value">属性的新值</param>
    public void SetScriptProperty(GameObject target, string scriptName, string propertyName, object value)
    {
        if (target == null || string.IsNullOrEmpty(scriptName) || string.IsNullOrEmpty(propertyName))
        {
            Debug.LogError("目标对象、脚本名称或属性名称不能为空！");
            return;
        }

        // 获取脚本类型
        Type scriptType = Type.GetType(scriptName);
        if (scriptType == null)
        {
            Debug.LogError($"无法找到脚本 {scriptName}，请确保脚本名称正确且存在于项目中！");
            return;
        }

        // 获取目标对象上的脚本实例
        Component component = target.GetComponent(scriptType);
        if (component == null)
        {
            Debug.LogError($"目标对象 {target.name} 未包含脚本 {scriptName}！");
            return;
        }

        // 通过反射获取属性信息
        var property = scriptType.GetProperty(propertyName);
        if (property == null)
        {
            Debug.LogError($"脚本 {scriptName} 中不存在属性 {propertyName}！");
            return;
        }

        // 设置属性值
        try
        {
            property.SetValue(component, value);
            Debug.Log($"成功将脚本 {scriptName} 的属性 {propertyName} 设置为 {value}。");
        }
        catch (Exception e)
        {
            Debug.LogError($"无法设置属性 {propertyName} 的值：{e.Message}");
        }
    }
}
