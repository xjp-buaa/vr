using UnityEngine;

public class DisableGravityForChildren : MonoBehaviour
{
    // Ŀ�길����
    public GameObject targetParent;

    void Start()
    {
        if (targetParent == null)
        {
            Debug.LogError("������Ŀ�길���壡");
            return;
        }

        // ��ȡ����������
        Rigidbody[] childRigidbodies = targetParent.GetComponentsInChildren<Rigidbody>();

        int updatedCount = 0;

        foreach (Rigidbody rb in childRigidbodies)
        {
            if (rb.useGravity)
            {
                rb.useGravity = false;
                updatedCount++;
                Debug.Log($"��ȡ�������� {rb.gameObject.name} �� Use Gravity ��ѡ��");
            }
        }

        Debug.Log($"����������ɣ���ȡ���� {updatedCount} �� Rigidbody �� Use Gravity ѡ�");
    }
}
