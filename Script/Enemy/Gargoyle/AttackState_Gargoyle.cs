using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Gargoyle : EnemyAttackAbleState
{
    // �ð�
    private float time;

    // ���� ���� �ð�
    private float ADelaytime = 0;

    // ���� ��Ʈ�ѷ�
    private AttackController attackController;

    // ���� ���� Ÿ��
    private int ranAttackInt;

    // ���� ���� Ÿ�� ���
    private int beforeAtInt = -1;

    // źȯ �߻� ��ġ
    [SerializeField] private Transform shootPos;

    // źȯ ������
    [SerializeField] private GameObject[] bulletPrefabs;

    public override void Awake()
    {
        base.Awake();

        attackController = GetComponent<AttackController>();
    }

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ������ ���� �̵� ����
        navMeshAgent.isStopped = true;

        
        while (true)
        {
            // ���� ���ݰ� �Ȱ�ġ�� ���� ���� ���� ��÷
            ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length+1);

            if (beforeAtInt != ranAttackInt)
            {
                beforeAtInt = ranAttackInt;
                break;
            }

        }

        // ���� Ÿ�Ժ� �ð� �Է�
        if (ranAttackInt == 0) { ADelaytime = 1.5f; } // �⺻ ����
        else if (ranAttackInt == 1) { ADelaytime = 2.7f; } // ȸ�� �޺� ����
        else if (ranAttackInt == 2) { ADelaytime = 1f; } // �ѹ� ����
        else { controller.TransactionToState(4); return; }


        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", state);

        // ���� ��Ʈ�ѷ����� ���� ���� ����
        attackController.TransactionToState(ranAttackInt);
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        LookAtTarget(true);

        if (ADelaytime < time)
        {
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

        animator.SetFloat("ATime", time);
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
        ADelaytime = 0;
        ranAttackInt = 0;
    }

    // ���� �̺�Ʈ,
    public void Shoot(int type)
    {
        if (type == 0)
        {
            Instantiate(bulletPrefabs[0], shootPos.position, shootPos.rotation);

            GetComponent<FSMController_Gargoyle>().MagicSound(0);
        }
        else
        {
            Instantiate(bulletPrefabs[1], shootPos.position, shootPos.rotation);
            GetComponent<FSMController_Gargoyle>().MagicSound(1);
        }

    }
}
