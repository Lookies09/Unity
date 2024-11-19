using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalkState_Demon02 : EnemyAttackAbleState
{
    // �Ǹ� 1
    private GameObject demon01;

    // ���� ��ȯ ���� ����
    private bool canAttack = false;

    // ������ �ð�
    private float time;

    // ������ ���� [��:0/��:1]
    private int walkDirc;

    public bool CanAttack { get => canAttack; set => canAttack = value; }

    public void Start()
    {
        // �Ǹ� 1 ����
        demon01 = GetComponent<FSMController_Demon02>().Demon01;
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

        if (GetComponent<ObjectHealth>().Phase < 1)
        {
            // �ٸ� �Ǹ��� ���� ����
            demon01.GetComponent<SideWalkState_Demon>().CanAttack = true;
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
        if (CanAttack || GetComponent<ObjectHealth>().Phase == 1)
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
