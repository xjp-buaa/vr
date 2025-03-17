using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class ToolInteraction : MonoBehaviour
{
    public GameObject screwdriverModel; // 螺丝刀模型
    public GameObject nailModel; // 钉子模型
    public GameObject uiPanel; // 显示UI提示的面板
    public Text uiText; // UI文本提示
    private bool isNearNail = false; // 判断是否靠近钉子
    private bool isScrewdriverEquipped = false; // 判断是否装备了螺丝刀模型
    private bool isScrewing = false; // 判断是否正在拧动螺丝
    private float screwRotationSpeed = 30f; // 螺丝旋转速度

    void Start()
    {
        // 初始时UI面板不可见
        uiPanel.SetActive(false);
    }

    void Update()
    {
        // 如果螺丝刀靠近钉子且已经装备了螺丝刀
        if (isNearNail && isScrewdriverEquipped)
        {
            ShowUIPrompt("螺丝刀和钉子匹配，点击以拧动螺丝！");

            // 检测玩家是否按下按钮来开始拧螺丝
            if (isScrewing)
            {
                Screw();
            }
        }
        else
        {
            HideUIPrompt();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 当螺丝刀模型进入钉子的碰撞区域时
        if (other.CompareTag("Nail"))
        {
            isNearNail = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 当螺丝刀模型离开钉子的碰撞区域时
        if (other.CompareTag("Nail"))
        {
            isNearNail = false;
        }
    }

    public void EquipScrewdriver()
    {
        // 如果手上是螺丝刀模型
        isScrewdriverEquipped = true;
    }

    public void UnequipScrewdriver()
    {
        // 如果手上不是螺丝刀模型
        isScrewdriverEquipped = false;
    }

    private void ShowUIPrompt(string message)
    {
        // 显示UI提示
        uiPanel.SetActive(true);
        uiText.text = message;
    }

    private void HideUIPrompt()
    {
        // 隐藏UI提示
        uiPanel.SetActive(false);
    }

    // 按下按钮时开始拧动螺丝
    public void OnScrewButtonPressed()
    {
        if (isNearNail && isScrewdriverEquipped)
        {
            isScrewing = true;
        }
    }

    // 按钮松开时停止拧动螺丝
    public void OnScrewButtonReleased()
    {
        isScrewing = false;
    }

    private void Screw()
    {
        // 执行螺丝的旋转
        nailModel.transform.Rotate(Vector3.forward, screwRotationSpeed * Time.deltaTime);
    }
}
