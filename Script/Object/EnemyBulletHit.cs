using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHit : BulletHit
{
    // 히트 사운드
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
        // 환경요소에 맞으면 
        if (collision.gameObject.tag == "Enviroment")
        {
            // 충돌 위치 확인
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;

            // 충돌 위치에 히트 이펙트 생성
            Instantiate(hitEffect, pos, Quaternion.identity);

            // 총알 파괴
            Destroy(gameObject);
            return;
        }

        // 플레이어와 맞으면
        if (collision.gameObject.tag == "Player" && enterCount == 0)
        {
            enterCount++;

            collision.gameObject.GetComponent<ObjectHealth>().Hit(bulletDMG);

            // 충돌 위치 확인
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;

            // 충돌 위치에 히트 이펙트 생성
            GameObject effect = Instantiate(hitEffect, pos, Quaternion.identity);
            effect.GetComponent<AudioSource>().spatialBlend = 0;
            effect.GetComponent<AudioSource>().volume = 0.9f;
            effect.GetComponent<AudioSource>().clip = hitSound;
            effect.GetComponent<AudioSource>().Play();

            // 총알 파괴
            Destroy(gameObject);
            return;
        }
    }
}
