using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    // ��Ʈ �� �߻� ����Ʈ
    [SerializeField] protected GameObject hitEffect;

    // �Ѿ� ���� �ð�
    protected float durantionTime;

    // �浹 Ƚ��
    protected int enterCount = 0;

    // �Ѿ� ���ݷ�
    [SerializeField] protected int bulletDMG;

    protected virtual void Start()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), bullet.GetComponent<Collider>());
        }        
    }

    private void Update()
    {
        durantionTime += Time.deltaTime;

        // ��Ʈ�� �ȵŵ� 5�ʰ� ������
        if (durantionTime >= 5f)
        {
            // �Ѿ� �ı�
            Destroy(gameObject);
        }
    }
}
