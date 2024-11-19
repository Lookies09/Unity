using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Gargoyle : EnemyAttackAbleState
{
    [SerializeField] private GameObject[] demons;

    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ��� �ִϸ��̼� ���
        animator.SetInteger("State", state);

        // �׺���̼� ������Ʈ ����
        navMeshAgent.enabled = false;

        // �ִϸ����� ��Ʈ��� ����
        animator.applyRootMotion = false;

        GetComponent<Collider>().isTrigger = true;
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        if (demons[0] == null && demons[1] == null)
        {
            // ���� �շ�
            controller.TransactionToState(1);
            return;
        }        
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {

        GetComponent<Collider>().isTrigger = false;
    }


}
