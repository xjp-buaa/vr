using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ControllerModelSwitcher : MonoBehaviour
{
    public XRRayInteractor rayInteractor;  // 绑定射线交互器
    public InputActionProperty switchButton; // 绑定输入按键
    public GameObject defaultControllerModel; // 默认手柄模型
    private GameObject currentModel; // 记录当前手柄模型

    void Start()
    {
        if (defaultControllerModel != null)
        {
            currentModel = Instantiate(defaultControllerModel, transform);
        }
    }

    void Update()
    {
        // 监听按键按下
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
