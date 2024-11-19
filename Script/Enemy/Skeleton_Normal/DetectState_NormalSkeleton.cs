using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectState_NormalSkeleton : EnemyAttackAbleState
{
    // 추적 이동 속도
    [SerializeField] protected float detectSpeed;

    // 움직임 오디오 소스
    [SerializeField] private AudioSource movementAudio;

    // 추격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 추적 애니메이션 재생
        animator.SetInteger("State", (int)state);

        // 공격 대상 추적 처리
        navMeshAgent.isStopped = false;
        navMeshAgent.updateRotation = false;

        // 추적 속도 설정
        navMeshAgent.speed = detectSpeed;
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        navMeshAgent.SetDestination(controller.Player.transform.position);

        // 이동 방향으로 캐릭터 회전
        RotateCharacter(navMeshAgent.velocity.normalized);


        // 플레이거 공격 가능 거리안에 들어왔다면
        if (controller.GetPlayerDistance() < attackDistance)
        {
            // 공격 상태
            controller.TransactionToState(3);
            return;
        }
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
    }

    // 캐릭터 이동 방향으로 회전
    private void RotateCharacter(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // 회전 속도를 조절할 수 있음
        }
    }

    public void MovementSound()
    {
        movementAudio.Play();
    }
}
