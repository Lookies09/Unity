using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class round : MonoBehaviour
{
    public int numberOfObjects = 12; // 생성할 빈 게임오브젝트의 수

    public GameObject wall;
    public float radius = 5f; // 원의 반지름
    public float height = 0f; // 원의 높이 (오브젝트를 원 평면 상에 배치할 경우)

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
            pos += gameObject.transform.position; // 원의 중심으로 이동

            GameObject newObject = Instantiate(wall);
            newObject.transform.position = pos;
            newObject.transform.SetParent(gameObject.transform); // 부모 설정

            // 원의 중심을 바라보도록 회전 설정
            Vector3 lookAtPos = gameObject.transform.position;
            lookAtPos.y = newObject.transform.position.y; // 높이는 같게 유지

            newObject.transform.LookAt(lookAtPos);
        }
    }
}
