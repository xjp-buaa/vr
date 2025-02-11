using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class HighlightOnHover : MonoBehaviour
{
    private Material originalMaterial;
    public Material highlightMaterial; // 高亮材质

    private MeshRenderer meshRenderer;

    void Start()
    {
        // 获取 MeshRenderer 和原始材质
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            originalMaterial = meshRenderer.material;
        }

        // 获取 XRGrabInteractable 组件
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();

        // 注册事件
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        // 更改为高亮材质
        if (meshRenderer != null && highlightMaterial != null)
        {
            meshRenderer.material = highlightMaterial;
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        // 恢复原始材质
        if (meshRenderer != null)
        {
            meshRenderer.material = originalMaterial;
        }
    }

    void OnDestroy()
    {
        // 确保在销毁时取消注册事件
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnHoverEnter);
            interactable.hoverExited.RemoveListener(OnHoverExit);
        }
    }
}
