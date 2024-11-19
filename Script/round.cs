using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class round : MonoBehaviour
{
    public int numberOfObjects = 12; // ������ �� ���ӿ�����Ʈ�� ��

    public GameObject wall;
    public float radius = 5f; // ���� ������
    public float height = 0f; // ���� ���� (������Ʈ�� �� ��� �� ��ġ�� ���)

    void Start()
    {
        CreateObjectsInCircle();
    }

    void CreateObjectsInCircle()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), height, Mathf.Sin(angle)) * radius;
            pos += gameObject.transform.position; // ���� �߽����� �̵�

            GameObject newObject = Instantiate(wall);
            newObject.transform.position = pos;
            newObject.transform.SetParent(gameObject.transform); // �θ� ����

            // ���� �߽��� �ٶ󺸵��� ȸ�� ����
            Vector3 lookAtPos = gameObject.transform.position;
            lookAtPos.y = newObject.transform.position.y; // ���̴� ���� ����

            newObject.transform.LookAt(lookAtPos);
        }
    }
}
