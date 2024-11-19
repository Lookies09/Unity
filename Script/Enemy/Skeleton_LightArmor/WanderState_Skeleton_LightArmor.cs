using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WanderState_Skeleton_LightArmor : EnemyAttackAbleState
{
    // 배회 선정 범위
    [SerializeField] protected float radius;

    // 배회 위치 참조
    protected Transform targetTransform = null;

    // 배회 위치 (기본 : 무한 위치값)
    public Vector3 targetPosition = Vector3.positiveInfinity;

    // 배회시 이동 속도
    [SerializeField] protected float moveSpeed;

    // 배회 위치와의 거리
    public float targetDistance = Mathf.Infinity;

    // 배회 이동 네비게이션 체크 영역 거리
    [SerializeField] protected float wanderNavCheckRadius;

    // 배회 기준 위치 포인트들
    [SerializeField] protected Transform[] wanderPoints;

    // 배회 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        if (wanderPoints.Length == 0)
        {
            // 대기 상태로 전환
            controller.TransactionToState(0);
            return;
        }

        // 배회 이동 속도를 네비게이션 에이전트에 설정
        navMeshAgent.speed = moveSpeed;

        // 배회 애니메이션 재생
        animator.SetInteger("State", (int)state);

        // 새로운 배회 위치를 탐색
        NewRandomDestination();
    }

    // 배회 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        // 이동 방향으로 캐릭터 회전
        RotateCharacter(navMeshAgent.velocity.normalized);

        // 플레이어가 공격 가능 거리안에 들어오면
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // 공격 상태로 전환
            controller.TransactionToState(2);
            return;
        }
        

        // 배회할 이동 위치가 존재한다면
        if (targetTransform != null)
        {
            // 배회할 위치 근처에 도달했다면
            targetPosition.y = gameObject.transform.position.y;
            targetDistance = Vector3.Distance(transform.position, targetPosition);
            if (targetDistance < 0.5f)
            {
                // 대기 상태로 전환
                controller.TransactionToState(0);

            }
        }
    }

    // 배회 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {

        // 네비게이션 이동 종료
        navMeshAgent.isStopped = true;

        // 배회 관련 위치 정보들 초기화
        targetTransform = null;
        targetPosition = Vector3.positiveInfinity;
        targetDistance = Mathf.Infinity;

    }

    // 새로운 배회 위치를 탐색함
    protected void NewRandomDestination()
    {
        // 배회 위치 인덱스 추첨
        int index = Random.Range(0, wanderPoints.Length);

        // 같은 배회 위치를 탐색 했다면 다시 탐색
        float distance = Vector3.Distance(wanderPoints[index].position, transform.position);
        if (distance < radius)
        {
            // 배회할 위치를 다시 추첨함
            NewRandomDestination();
            return;
        }

        // 배회 위치로 선정
        targetTransform = wanderPoints[index];

        // 배회 위치를 기준으로한 일정 범위안의 랜덤한 위치를 재선정
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += wanderPoints[index].position;
        //randomDirection.y = gameObject.transform.position.y;

        // 랜덤 추첨한 배회 위치를 네비게이션 에이전트 이동 속도로 설정
        targetPosition = randomDirection;

        Debug.Log($"배회 이동할 위치 : {targetPosition}");

        // 네비게이션 이동이 유효하다면
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderNavCheckRadius, 1))
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.SetDestination(targetPosition);
        }
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
}
