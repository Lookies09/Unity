using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : MonoBehaviour
{
    // ���� ���ѻ��±�� ��Ʈ�ѷ�
    protected EnemyController controller;

    // �ִϸ����� ������Ʈ
    protected Animator animator;

    // �׺���̼� ������Ʈ
    protected NavMeshAgent navMeshAgent;


    public virtual void Awake()
    {
        controller = gameObject.GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    // ���� ���� ���� �������̽�(�����������̽��ƴ�) �޼ҵ� ����

    // ���� ���� ����(�ٸ����¿��� ���̵�) �޼ҵ�
    public abstract void EnterState(int state);

    // ���� ���� ������Ʈ �߻� �޼ҵ� (���� ���� ����)
    public abstract void UpdateState();

    // ���� ���� ����(�ٸ����·� ���̵�) �޼ҵ�
    public abstract void ExitState();

}
