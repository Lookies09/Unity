using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvoidState_Darklord : EnemyAttackAbleState
{
    // �ð�
    private float time;

    // ȸ�� ����
    private int avoidDirec = -1;

    // ȸ�� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        avoidDirec = Random.Range(0, 2);


        animator.SetInteger("AvoidDirec", avoidDirec);
        // ȸ�� �ִϸ��̼� ���
        animator.SetInteger("State", state);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        // 0.5�� ���������� ����
        if (time < 0.3f) { return; }

        // ���� ����� ���� ���� �Ÿ����� �־����ٸ�
        if (controller.GetPlayerDistance() > attackDistance)
        {
            // �߰�
            controller.TransactionToState(2);
            return;
        }
        else
        {
            // ����
            controller.TransactionToState(3);
        }
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
    }
}
