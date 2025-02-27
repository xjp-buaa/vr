using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ControllerModelSwitcher : MonoBehaviour
{
    public XRRayInteractor rayInteractor;  // �����߽�����
    public InputActionProperty switchButton; // �����밴��
    public GameObject defaultControllerModel; // Ĭ���ֱ�ģ��
    private GameObject currentModel; // ��¼��ǰ�ֱ�ģ��

    void Start()
    {
        if (defaultControllerModel != null)
        {
            currentModel = Instantiate(defaultControllerModel, transform);
        }
    }

    void Update()
    {
        // ������������
        if (switchButton.action.WasPressedThisFrame())
        {
            TrySwitchModel();
        }
    }

    void TrySwitchModel()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            GameObject targetObject = hit.collider.gameObject;

            if (targetObject != null)
            {
                ReplaceControllerModel(targetObject);
            }
        }
    }

    void ReplaceControllerModel(GameObject newModelPrefab)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        currentModel = Instantiate(newModelPrefab, transform);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;
    }
}
