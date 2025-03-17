using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class ToolInteraction : MonoBehaviour
{
    public GameObject screwdriverModel; // ��˿��ģ��
    public GameObject nailModel; // ����ģ��
    public GameObject uiPanel; // ��ʾUI��ʾ�����
    public Text uiText; // UI�ı���ʾ
    private bool isNearNail = false; // �ж��Ƿ񿿽�����
    private bool isScrewdriverEquipped = false; // �ж��Ƿ�װ������˿��ģ��
    private bool isScrewing = false; // �ж��Ƿ�����š����˿
    private float screwRotationSpeed = 30f; // ��˿��ת�ٶ�

    void Start()
    {
        // ��ʼʱUI��岻�ɼ�
        uiPanel.SetActive(false);
    }

    void Update()
    {
        // �����˿�������������Ѿ�װ������˿��
        if (isNearNail && isScrewdriverEquipped)
        {
            ShowUIPrompt("��˿���Ͷ���ƥ�䣬�����š����˿��");

            // �������Ƿ��°�ť����ʼš��˿
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
        // ����˿��ģ�ͽ��붤�ӵ���ײ����ʱ
        if (other.CompareTag("Nail"))
        {
            isNearNail = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // ����˿��ģ���뿪���ӵ���ײ����ʱ
        if (other.CompareTag("Nail"))
        {
            isNearNail = false;
        }
    }

    public void EquipScrewdriver()
    {
        // �����������˿��ģ��
        isScrewdriverEquipped = true;
    }

    public void UnequipScrewdriver()
    {
        // ������ϲ�����˿��ģ��
        isScrewdriverEquipped = false;
    }

    private void ShowUIPrompt(string message)
    {
        // ��ʾUI��ʾ
        uiPanel.SetActive(true);
        uiText.text = message;
    }

    private void HideUIPrompt()
    {
        // ����UI��ʾ
        uiPanel.SetActive(false);
    }

    // ���°�ťʱ��ʼš����˿
    public void OnScrewButtonPressed()
    {
        if (isNearNail && isScrewdriverEquipped)
        {
            isScrewing = true;
        }
    }

    // ��ť�ɿ�ʱֹͣš����˿
    public void OnScrewButtonReleased()
    {
        isScrewing = false;
    }

    private void Screw()
    {
        // ִ����˿����ת
        nailModel.transform.Rotate(Vector3.forward, screwRotationSpeed * Time.deltaTime);
    }
}
