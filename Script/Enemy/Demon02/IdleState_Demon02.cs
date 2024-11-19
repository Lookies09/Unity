using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Demon02 : EnemyAttackAbleState
{
    // �Ǹ� 1
    private GameObject demon01;

    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // ��� �ִϸ��̼� ���
        animator.SetInteger("State", state);

        demon01 = GetComponent<FSMController_Demon02>().Demon01;
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // 2��������
        if (demon01.GetComponent<Health_Demon01>().Phase == 1)
        {
            // ���� �շ�
            controller.TransactionToState(1);
            return;
        }
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
    }
}
