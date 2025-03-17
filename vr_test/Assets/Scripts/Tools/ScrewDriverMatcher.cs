using UnityEngine;
using UnityEngine.UI;

public class ScrewDriverMatcher : MonoBehaviour
{
    public GameObject screwdriver; // ��˿��ģ��
    public GameObject screw; // ��˿ģ��
    public GameObject uiPanel; // UI��ʾ���
    public Text uiText; // UI�ı�
    public float uiOffset = 0.3f; // UI������˿�ĸ߶�ƫ����

    private bool isNearScrew = false; // �Ƿ񿿽���˿
    private Camera mainCamera; // �����

    void Start()
    {
        // ��ȡ�������
        mainCamera = Camera.main;
        // ȷ��UIһ��ʼ�����ص�
        uiPanel.SetActive(false);
    }

    void Update()
    {
        // ���UI��ʾ����ʹ����������ͷ
        if (uiPanel.activeSelf)
        {
            FaceCamera();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ����˿��������˿�Ĵ�������
        if (other.gameObject == screw)
        {
            isNearScrew = true;
            ShowUIPrompt("��˿��ƥ��ɹ����Ƿ����š����");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // ����˿���뿪��˿����
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

        // ����UIλ�ã�ʹ������˿�Ϸ�
        Vector3 newPosition = screw.transform.position + new Vector3(0, uiOffset, 0);
        uiPanel.transform.position = newPosition;

        // ��UI��������ͷ
        FaceCamera();
    }

    private void HideUIPrompt()
    {
        uiPanel.SetActive(false);
    }

    private void FaceCamera()
    {
        // ��UIʼ����������ͷ
        if (mainCamera != null)
        {
            uiPanel.transform.LookAt(mainCamera.transform);
            uiPanel.transform.Rotate(0, 180, 0); // ��ת180�ȣ�ʹUI���ᷴ��
        }
    }
}
