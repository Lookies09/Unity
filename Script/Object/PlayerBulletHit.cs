using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletHit : BulletHit
{
    [SerializeField] private AudioClip enviromentHitSound;

    [SerializeField] private AudioClip enemyHitSound;
    

    private void OnCollisionEnter(Collision collision)
    {
        // 환경요소에 맞으면 
        if (collision.gameObject.tag == "Enviroment")
        {   
            // 충돌 위치 확인
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;

            // 충돌 위치에 히트 이펙트 생성
            GameObject effect = Instantiate(hitEffect, pos, Quaternion.identity);

            effect.GetComponent<AudioSource>().clip = enviromentHitSound;
            effect.GetComponent<AudioSource>().Play();

            // 총알 파괴
            Destroy(gameObject);
            return;
        }

        // 적군과 맞으면
        if (collision.gameObject.tag == "Enemy" && enterCount == 0)
        {
            enterCount++;

            collision.gameObject.GetComponent<ObjectHealth>().Hit(bulletDMG);

            // 충돌 위치 확인
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;

            // 충돌 위치에 히트 이펙트 생성
            GameObject effect = Instantiate(hitEffect, pos, Quaternion.identity);

            effect.GetComponent<AudioSource>().clip = enemyHitSound;
            effect.GetComponent<AudioSource>().Play();

            // 총알 파괴
            Destroy(gameObject);
            return;
        }
    }
    
}
