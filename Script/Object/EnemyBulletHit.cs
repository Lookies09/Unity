using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHit : BulletHit
{
    // ��Ʈ ����
    [SerializeField] private AudioClip hitSound;

    protected override void Start()
    {
        base.Start();


        GameObject[] Enemys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject Enemy in Enemys)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), Enemy.GetComponent<Collider>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ȯ���ҿ� ������ 
        if (collision.gameObject.tag == "Enviroment")
        {
            // �浹 ��ġ Ȯ��
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;

            // �浹 ��ġ�� ��Ʈ ����Ʈ ����
            Instantiate(hitEffect, pos, Quaternion.identity);

            // �Ѿ� �ı�
            Destroy(gameObject);
            return;
        }

        // �÷��̾�� ������
        if (collision.gameObject.tag == "Player" && enterCount == 0)
        {
            enterCount++;

            collision.gameObject.GetComponent<ObjectHealth>().Hit(bulletDMG);

            // �浹 ��ġ Ȯ��
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;

            // �浹 ��ġ�� ��Ʈ ����Ʈ ����
            GameObject effect = Instantiate(hitEffect, pos, Quaternion.identity);
            effect.GetComponent<AudioSource>().spatialBlend = 0;
            effect.GetComponent<AudioSource>().volume = 0.9f;
            effect.GetComponent<AudioSource>().clip = hitSound;
            effect.GetComponent<AudioSource>().Play();

            // �Ѿ� �ı�
            Destroy(gameObject);
            return;
        }
    }
}
