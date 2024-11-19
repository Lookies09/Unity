using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : MonoBehaviour
{
    // 몬스터 유한상태기계 컨트롤러
    protected EnemyController controller;

    // 애니메이터 컴포넌트
    protected Animator animator;

    // 네비게이션 컴포넌트
    protected NavMeshAgent navMeshAgent;


    public virtual void Awake()
    {
        controller = gameObject.GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    // 몬스터 상태 관련 인터페이스(문법인터페이스아님) 메소드 선언

    // 몬스터 상태 시작(다른상태에서 전이됨) 메소드
    public abstract void EnterState(int state);

    // 몬스터 상태 업데이트 추상 메소드 (상태 동작 수행)
    public abstract void UpdateState();

    // 몬스터 상태 종료(다른상태로 전이됨) 메소드
    public abstract void ExitState();

}
