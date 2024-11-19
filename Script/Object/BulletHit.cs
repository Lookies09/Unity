using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    // 히트 시 발생 이펙트
    [SerializeField] protected GameObject hitEffect;

    // 총알 지속 시간
    protected float durantionTime;

    // 충돌 횟수
    protected int enterCount = 0;

    // 총알 공격력
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

        // 히트가 안돼도 5초가 지나면
        if (durantionTime >= 5f)
        {
            // 총알 파괴
            Destroy(gameObject);
        }
    }
}
