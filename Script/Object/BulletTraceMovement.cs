using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѿ� �߰� ������ 
public class BulletTraceMovement : MonoBehaviour
{
    // �߰ݽð�
    [SerializeField] private float traceTime;

    // �Ѿ� �ӵ�
    [SerializeField] private float speed;

    // ��� �ð�
    private float time;

    // �߰��� ���
    private GameObject target;

    // �߰��� ����� �ױ�
    [SerializeField] private string targetTag;

    // Ÿ�� ��ġ
    private Vector3 targetPos;

    // ����
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag(targetTag);
        
    }

    // Update is called once per frame
    void Update()
    {
        // �ð� �帧
        time += Time.deltaTime;

        targetPos = target.transform.position;

        // Ÿ���� �� ������ �����Ǿ��־ ���� �÷���
        targetPos.y += 1.6f;
                

        // traceTime ��ŭ �߰��ϰ�
        if (time < traceTime)
        {
            // ���ư� ���� ������
            direction = targetPos - transform.position;
            direction = direction.normalized;

            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * (speed + 6) * Time.deltaTime, Space.World);
        }
        

    }
}
