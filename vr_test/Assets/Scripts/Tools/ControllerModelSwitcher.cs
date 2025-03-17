using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ControllerModelSwitcher : MonoBehaviour
{
    public XRRayInteractor rayInteractor;  // �����߽�����
    public InputActionProperty switchButton; // �����밴��
    public GameObject defaultControllerModel; // Ĭ���ֱ�ģ��
    private GameObject currentModel; // ��¼��ǰ�ֱ�ģ��

    private ActionBasedController actionBasedController; // ������ ActionBasedController ������ XRBaseController

    void Start()
    {
        // ��ȡ ActionBasedController ���
        actionBasedController = GetComponent<ActionBasedController>();
        if (actionBasedController == null)
        {
            Debug.LogError("ActionBasedController ���δ�ҵ�����ȷ���˽ű����ӵ����� ActionBasedController �� GameObject �ϡ�");
            return;
        }

        // ��ʼ��Ĭ���ֱ�ģ��
        if (defaultControllerModel != null)
        {
            SetControllerModel(defaultControllerModel);
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

            if (targetObject != null && targetObject.CompareTag("skrewdriver"))
            {
                SetControllerModel(targetObject);
            }
            else
            {
                Debug.Log("δ��⵽");
            }
        }
    }

    void SetControllerModel(GameObject newModelPrefab)
    {
        if (actionBasedController == null)
            return;

        // ���� ActionBasedController���̳��� XRBaseController���� modelPrefab ����
        actionBasedController.modelPrefab = newModelPrefab.transform;

        // �����ٵ�ǰģ��
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // ʵ������ģ��
        currentModel = Instantiate(newModelPrefab, transform);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;
    }
}
