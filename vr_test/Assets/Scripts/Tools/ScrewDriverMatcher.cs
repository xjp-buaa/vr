using UnityEngine;
using UnityEngine.UI;

public class ScrewDriverMatcher : MonoBehaviour
{
    public GameObject screwdriver; // 螺丝刀模型
    public GameObject screw; // 螺丝模型
    public GameObject uiPanel; // UI提示面板
    public Text uiText; // UI文本
    public float uiOffset = 0.3f; // UI距离螺丝的高度偏移量

    private bool isNearScrew = false; // 是否靠近螺丝
    private Camera mainCamera; // 摄像机

    void Start()
    {
        // 获取主摄像机
        mainCamera = Camera.main;
        // 确保UI一开始是隐藏的
        uiPanel.SetActive(false);
    }

    void Update()
    {
        // 如果UI显示，则使其面向摄像头
        if (uiPanel.activeSelf)
        {
            FaceCamera();
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
}
