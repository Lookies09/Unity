using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpState_Demon : EnemyAttackAbleState
{
    // ��� �ð�
    private float standupTime;

    // Į ���ӿ�����Ʈ ����
    [SerializeField] private GameObject axeOnBack; // �� �ڿ� ����
    [SerializeField] private GameObject axeOnBattle; // ������ ����

    // ���۽� ����
    [SerializeField] private ParticleSystem startSmoke;

    // ���� ����Ʈ
    [SerializeField] private ParticleSystem battleCryEffect;

    // �ƿ�� ����Ʈ
    [SerializeField] private ParticleSystem auraEffect;

    [SerializeField] private BossUIManager bossUIManager;

    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // �ִϸ��̼� ���
        animator.SetInteger("State", state);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        standupTime += Time.deltaTime;
        animator.SetFloat("StandupTime", standupTime);

        // 1�ʰ� ����������
        if (standupTime < 13f)
        {
            return;
        }

        // �÷��̰� �߰� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            // ���� ����
            controller.TransactionToState(2);
            return;
        }

        // ���ݰ��� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ����
            controller.TransactionToState(4);
        }
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        standupTime = 0;
        bossUIManager.SetBossInfo(gameObject, "�Ǹ� 1");
    }
   

    public void ChangeAxeEvent()
    {
        axeOnBack.SetActive(false);
        axeOnBattle.SetActive(true);
    }

    public void BattleCryEffect()
    {
        startSmoke.Play();

        battleCryEffect.Play();
        auraEffect.Play();
    }
}
