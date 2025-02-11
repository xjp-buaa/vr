using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class HighlightOnHover : MonoBehaviour
{
    private Material originalMaterial;
    public Material highlightMaterial; // ��������

    private MeshRenderer meshRenderer;

    void Start()
    {
        // ��ȡ MeshRenderer ��ԭʼ����
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            originalMaterial = meshRenderer.material;
        }

        // ��ȡ XRGrabInteractable ���
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();

        // ע���¼�
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        // ����Ϊ��������
        if (meshRenderer != null && highlightMaterial != null)
        {
            meshRenderer.material = highlightMaterial;
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        // �ָ�ԭʼ����
        if (meshRenderer != null)
        {
            meshRenderer.material = originalMaterial;
        }
    }

    void OnDestroy()
    {
        // ȷ��������ʱȡ��ע���¼�
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnHoverEnter);
            interactable.hoverExited.RemoveListener(OnHoverExit);
        }
    }
}
