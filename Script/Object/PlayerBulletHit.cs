using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletHit : BulletHit
{
    [SerializeField] private AudioClip enviromentHitSound;

    [SerializeField] private AudioClip enemyHitSound;
    

    private void OnCollisionEnter(Collision collision)
    {
        // ȯ���ҿ� ������ 
        if (collision.gameObject.tag == "Enviroment")
        {   
            // �浹 ��ġ Ȯ��
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;

            // �浹 ��ġ�� ��Ʈ ����Ʈ ����
            GameObject effect = Instantiate(hitEffect, pos, Quaternion.identity);

            effect.GetComponent<AudioSource>().clip = enviromentHitSound;
            effect.GetComponent<AudioSource>().Play();

            // �Ѿ� �ı�
            Destroy(gameObject);
            return;
        }

        // ������ ������
        if (collision.gameObject.tag == "Enemy" && enterCount == 0)
        {
            enterCount++;

            collision.gameObject.GetComponent<ObjectHealth>().Hit(bulletDMG);

            // �浹 ��ġ Ȯ��
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;

            // �浹 ��ġ�� ��Ʈ ����Ʈ ����
            GameObject effect = Instantiate(hitEffect, pos, Quaternion.identity);

            effect.GetComponent<AudioSource>().clip = enemyHitSound;
            effect.GetComponent<AudioSource>().Play();

            // �Ѿ� �ı�
            Destroy(gameObject);
            return;
        }
    }
    
}
