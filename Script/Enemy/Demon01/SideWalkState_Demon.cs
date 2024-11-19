using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalkState_Demon : EnemyAttackAbleState
{
    // �Ǹ� 2
    private GameObject demon02;

    // ���� ��ȯ ���� ����
    private bool canAttack = false;

    // ������ �ð�
    private float time;

    // ������ ���� [��:0/��:1]
    private int walkDirc;

    public bool CanAttack { get => canAttack; set => canAttack = value; }

    public void Start()
    {
        // �Ǹ� 2 ����
        demon02 = GetComponent<FSMController_Demon01>().Demon02;
    }

    // ������ ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ������ �ִϸ��̼� ���
        animator.SetInteger("State", state);

        walkDirc = Random.Range(0, 2);
        animator.SetInteger("WalkDirc", walkDirc);

        // ����
        navMeshAgent.isStopped = true;        

        // ���� ���� ����
        canAttack = false;

        if (GetComponent<ObjectHealth>().Phase < 2)
        {
            // �ٸ� �Ǹ��� ���� ����
            demon02.GetComponent<SideWalkState_Demon02>().CanAttack = true;
        }
        
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        animator.SetFloat("SideWalkTime", time);

        // �÷��̾� �ٶ󺸱�
        LookAtTarget(true);


        // ���� ���� ���� ���¶��
        if (CanAttack || GetComponent<ObjectHealth>().Phase == 2)
        {
            // �߰�
            controller.TransactionToState(2);
        }
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
    }
}
