using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class AttackPatternState : MonoBehaviour
{
    protected EnemyController controller;
    protected AttackController aController;
        
    // �ִϸ����� ������Ʈ
    protected Animator animator;

    // �׺���̼� ������Ʈ
    protected NavMeshAgent navMeshAgent;



    protected virtual void Awake()
    {
        controller = GetComponent<EnemyController>();
        aController = GetComponent<AttackController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    
    public abstract void EnterState(int state);


    // ���� ���� ����(�ٸ����·� ���̵�) �޼ҵ�
    public abstract void ExitState();


}
