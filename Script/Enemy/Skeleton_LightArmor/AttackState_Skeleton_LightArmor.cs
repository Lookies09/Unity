using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Skeleton_LightArmor : EnemyAttackAbleState
{
    // 시간
    private float time;

    // 공격 오디오
    [SerializeField] private AudioSource attackAudio;

    [SerializeField] private AudioClip[] attackClips;

    

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
        if (time > 1.5f)
        {
            // 공격 대상이 공격 가능 거리보다 멀어졌다면
            if (controller.GetPlayerDistance() > attackDistance)
            {

                // 추격
                controller.TransactionToState(2);
                return;

            }
        }
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
    }

    public void AttackAudioPlay()
    {
        int num = Random.Range(0, attackClips.Length);

        attackAudio.clip = attackClips[num];

        attackAudio.Play();
    }
}
