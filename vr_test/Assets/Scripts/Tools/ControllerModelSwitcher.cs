using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ControllerModelSwitcher : MonoBehaviour
{
    public XRRayInteractor rayInteractor;  // 绑定射线交互器
    public InputActionProperty switchButton; // 绑定输入按键
    public GameObject defaultControllerModel; // 默认手柄模型
    private GameObject currentModel; // 记录当前手柄模型

    private ActionBasedController actionBasedController; // 这里用 ActionBasedController 而不是 XRBaseController

    void Start()
    {
        // 获取 ActionBasedController 组件
        actionBasedController = GetComponent<ActionBasedController>();
        if (actionBasedController == null)
        {
            Debug.LogError("ActionBasedController 组件未找到，请确保此脚本附加到包含 ActionBasedController 的 GameObject 上。");
            return;
        }

        // 初始化默认手柄模型
        if (defaultControllerModel != null)
        {
            SetControllerModel(defaultControllerModel);
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

            if (targetObject != null && targetObject.CompareTag("skrewdriver"))
            {
                SetControllerModel(targetObject);
            }
            else
            {
                Debug.Log("未检测到");
            }
        }
    }

    void SetControllerModel(GameObject newModelPrefab)
    {
        if (actionBasedController == null)
            return;

        // 更新 ActionBasedController（继承自 XRBaseController）的 modelPrefab 变量
        actionBasedController.modelPrefab = newModelPrefab.transform;

        // 先销毁当前模型
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // 实例化新模型
        currentModel = Instantiate(newModelPrefab, transform);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;
    }
}
