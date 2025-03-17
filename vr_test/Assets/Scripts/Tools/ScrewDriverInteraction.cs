using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewDriverInteraction : MonoBehaviour
{
    public GameObject screwdriver; // 螺丝刀模型
    public GameObject screw; // 螺丝模型
    public GameObject uiPanel; // UI面板
    public Text uiText; // UI文本
    public Button confirmButton; // "是" 按钮
    public Button refuseButton; // "否" 按钮

    public float uiOffset = 0.3f; // UI 位置偏移量
    public float screwRotationSpeed = 50f; // 螺丝旋转速度
    public float maxScrewRotation = 360f; // 最大拧紧角度（模拟螺丝完全拧紧）

    private bool isNearScrew = false; // 是否靠近螺丝
    private bool isScrewing = false; // 是否正在拧螺丝
    private Camera mainCamera; // 摄像机
    private XRGrabInteractable grabInteractable; // XR 交互组件
    private float lastScrewdriverAngle; // 记录螺丝刀上一帧的旋转角度
    private float totalScrewRotation = 0f; // 记录螺丝旋转的角度

    void Start()
    {
        // 获取主摄像机
        mainCamera = Camera.main;

        // 确保UI一开始是隐藏的
        uiPanel.SetActive(false);

        // 获取XR交互组件
        grabInteractable = screwdriver.GetComponent<XRGrabInteractable>();

        // 监听 UI 按钮点击事件
        confirmButton.onClick.AddListener(StartScrewing);
        refuseButton.onClick.AddListener(RefuseScrewing);
    }

    void Update()
    {
        // 让 UI 始终面向摄像头
        if (uiPanel.activeSelf)
        {
            FaceCamera();
        }

        // 处理拧螺丝逻辑
        if (isScrewing)
        {
            RotateScrew();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 当螺丝刀进入螺丝的触发区域
        if (other.gameObject == screw)
        {
            isNearScrew = true;
            ShowUIPrompt("螺丝刀匹配成功，是否进行拧动？");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 当螺丝刀离开螺丝区域
        if (other.gameObject == screw)
        {
            isNearScrew = false;
            HideUIPrompt();
        }
    }

    private void ShowUIPrompt(string message)
    {
        uiPanel.SetActive(true);
        uiText.text = message;

        // 设置UI位置，使其在螺丝上方
        Vector3 newPosition = screw.transform.position + new Vector3(0, uiOffset, 0);
        uiPanel.transform.position = newPosition;

        // 让UI朝向摄像头
        FaceCamera();
    }

    private void HideUIPrompt()
    {
        uiPanel.SetActive(false);
    }

    private void FaceCamera()
    {
        // 让UI始终面向摄像头
        if (mainCamera != null)
        {
            uiPanel.transform.LookAt(mainCamera.transform);
            uiPanel.transform.Rotate(0, 180, 0); // 旋转180度，使UI不会反向
        }
    }

    /// <summary>
    /// 点击 "是" 按钮后，进入拧螺丝模式 并关闭UI
    /// </summary>
    public void StartScrewing()
    {
        if (isNearScrew)
        {
            uiPanel.SetActive(false);
            isScrewing = true;
            HideUIPrompt();
            lastScrewdriverAngle = screwdriver.transform.eulerAngles.y; // 记录当前螺丝刀的初始角度
            totalScrewRotation = 0f; // 归零螺丝旋转角度
        }
    }

    /// <summary>
    /// 点击 "否" 按钮后，进入拧螺丝模式 并关闭UI
    /// </summary>
    public void RefuseScrewing()
    {
        uiPanel.SetActive(false);
    }
    /// <summary>
    /// 旋转螺丝逻辑
    /// </summary>
    private void RotateScrew()
    {
        // 获取螺丝刀当前角度
        float currentAngle = screwdriver.transform.eulerAngles.y;

        // 计算旋转方向（顺时针为正，逆时针为负）
        float rotationDelta = currentAngle - lastScrewdriverAngle;

        // 处理角度跳变问题（防止旋转跳变）
        if (rotationDelta > 180) rotationDelta -= 360;
        if (rotationDelta < -180) rotationDelta += 360;

        // 只有顺时针旋转才让螺丝跟着转
        if (rotationDelta > 0)
        {
            float rotationAmount = rotationDelta * Time.deltaTime * screwRotationSpeed;
            screw.transform.Rotate(Vector3.forward, rotationAmount);
            totalScrewRotation += Mathf.Abs(rotationAmount);

            // 判断是否拧紧到最大角度
            if (totalScrewRotation >= maxScrewRotation)
            {
                isScrewing = false;
                Debug.Log("螺丝拧紧完成！");
            }
        }

        // 更新上一帧的角度
        lastScrewdriverAngle = currentAngle;
    }
}
