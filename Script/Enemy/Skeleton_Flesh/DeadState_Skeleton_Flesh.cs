using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_Skeleton_Flesh : EnemyState
{
    // 폭발 이펙트
    [SerializeField] private GameObject explosionEffect;
    // 폭발 위치
    [SerializeField] private Transform explosionPos;

    // 공격 대상 레이어
    [SerializeField] private LayerMask targetLayer;

    // 폭발 범위
    [SerializeField] private float explosionRadius;

    // 사망 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 이동 중지
        navMeshAgent.isStopped = true;

        // 여기서 이펙트 터트리면서 피해
        Instantiate(explosionEffect, explosionPos.position, Quaternion.identity);

        ExplosionAttack();
    }

    // 사망 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        // 몬스터가 소멸됨
        Destroy(gameObject, 0.1f);
    }

    // 사망 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {

    }

    public void ExplosionAttack()
    {
        Collider[] hits = Physics.OverlapSphere(explosionPos.position, explosionRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            if (hit.tag == "Player")
            {
                Debug.Log("폭발 데미지 입음");

                hit.gameObject.GetComponent<ObjectHealth>().Hit(10);
                break;
            }

        }
    }

    public void OnDrawGizmosSelected()
    {
        // 검출 반경을 시각적으로 표시
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(explosionPos.position, explosionRadius);
    }
}
