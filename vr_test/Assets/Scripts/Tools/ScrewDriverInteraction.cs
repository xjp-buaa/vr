using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewDriverInteraction : MonoBehaviour
{
    public GameObject screwdriver; // ��˿��ģ��
    public GameObject screw; // ��˿ģ��
    public GameObject uiPanel; // UI���
    public Text uiText; // UI�ı�
    public Button confirmButton; // "��" ��ť
    public Button refuseButton; // "��" ��ť

    public float uiOffset = 0.3f; // UI λ��ƫ����
    public float screwRotationSpeed = 50f; // ��˿��ת�ٶ�
    public float maxScrewRotation = 360f; // ���š���Ƕȣ�ģ����˿��ȫš����

    private bool isNearScrew = false; // �Ƿ񿿽���˿
    private bool isScrewing = false; // �Ƿ�����š��˿
    private Camera mainCamera; // �����
    private XRGrabInteractable grabInteractable; // XR �������
    private float lastScrewdriverAngle; // ��¼��˿����һ֡����ת�Ƕ�
    private float totalScrewRotation = 0f; // ��¼��˿��ת�ĽǶ�

    void Start()
    {
        // ��ȡ�������
        mainCamera = Camera.main;

        // ȷ��UIһ��ʼ�����ص�
        uiPanel.SetActive(false);

        // ��ȡXR�������
        grabInteractable = screwdriver.GetComponent<XRGrabInteractable>();

        // ���� UI ��ť����¼�
        confirmButton.onClick.AddListener(StartScrewing);
        refuseButton.onClick.AddListener(RefuseScrewing);
    }

    void Update()
    {
        // �� UI ʼ����������ͷ
        if (uiPanel.activeSelf)
        {
            FaceCamera();
        }

        // ����š��˿�߼�
        if (isScrewing)
        {
            RotateScrew();
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

    /// <summary>
    /// ��� "��" ��ť�󣬽���š��˿ģʽ ���ر�UI
    /// </summary>
    public void StartScrewing()
    {
        if (isNearScrew)
        {
            uiPanel.SetActive(false);
            isScrewing = true;
            HideUIPrompt();
            lastScrewdriverAngle = screwdriver.transform.eulerAngles.y; // ��¼��ǰ��˿���ĳ�ʼ�Ƕ�
            totalScrewRotation = 0f; // ������˿��ת�Ƕ�
        }
    }

    /// <summary>
    /// ��� "��" ��ť�󣬽���š��˿ģʽ ���ر�UI
    /// </summary>
    public void RefuseScrewing()
    {
        uiPanel.SetActive(false);
    }
    /// <summary>
    /// ��ת��˿�߼�
    /// </summary>
    private void RotateScrew()
    {
        // ��ȡ��˿����ǰ�Ƕ�
        float currentAngle = screwdriver.transform.eulerAngles.y;

        // ������ת����˳ʱ��Ϊ������ʱ��Ϊ����
        float rotationDelta = currentAngle - lastScrewdriverAngle;

        // ����Ƕ��������⣨��ֹ��ת���䣩
        if (rotationDelta > 180) rotationDelta -= 360;
        if (rotationDelta < -180) rotationDelta += 360;

        // ֻ��˳ʱ����ת������˿����ת
        if (rotationDelta > 0)
        {
            float rotationAmount = rotationDelta * Time.deltaTime * screwRotationSpeed;
            screw.transform.Rotate(Vector3.forward, rotationAmount);
            totalScrewRotation += Mathf.Abs(rotationAmount);

            // �ж��Ƿ�š�������Ƕ�
            if (totalScrewRotation >= maxScrewRotation)
            {
                isScrewing = false;
                Debug.Log("��˿š����ɣ�");
            }
        }

        // ������һ֡�ĽǶ�
        lastScrewdriverAngle = currentAngle;
    }
}
