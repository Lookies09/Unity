using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Skeleton_Ranger : EnemyAttackAbleState
{
    // 시간
    private float time;

    // 총알 프리펩
    [SerializeField] private GameObject bulletPrefab;

    // 발사 위치들
    [SerializeField] private Transform[] shootPoses;

    // 공격 오디오
    [SerializeField] private AudioSource attackAudio;

    

    // 공격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 공격 애니메이션 재생
        animator.SetInteger("State", (int)state);

        // 공격을 위해 이동 중지
        navMeshAgent.isStopped = true;
    }

    // 공격 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        LookAtTarget(true);

        // 공격 모션에 들어가고 3초가 지나면
        if (time > 3f)
        {
            // 공격 대상이 공격 가능 거리보다 멀어졌다면
            if (controller.GetPlayerDistance() > attackDistance)
            {

                // 추격
                controller.TransactionToState(2);
                return;

            }
            // 공격 대상이 공격 가능 거리에 있다면
            else if (controller.GetPlayerDistance() <= attackDistance)
            {
                // 대기
                controller.TransactionToState(0);
            }
        }
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
    }

    // 발포 이벤트,
    public void Shoot()
    {
        for (int i = 0; i < shootPoses.Length; i++)
        {
            Instantiate(bulletPrefab, shootPoses[i].position, shootPoses[i].rotation);
        }
    }

    public void AttackAudioPlay()
    {
        attackAudio.Play();
    }

}
