using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipState_Darklord : EnemyAttackAbleState
{
    // ���� �ð�
    private float time;

    // �� ���� ���� ����Ʈ
    [SerializeField] private ParticleSystem swordGenSmoke;

    // �� ������Ʈ
    [SerializeField] private GameObject sword;

    [SerializeField] private BossUIManager bossUIManager;

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", state);
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        // 4�ʰ� ���������� ����
        if (time < 4f) { return; }

        // �÷��̰� �߰� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            if (controller.GetPlayerDistance() <= attackDistance)
            {
                // ���� ����
                controller.TransactionToState(3);
                return;
            }

            // �߰� ����
            controller.TransactionToState(2);
        }
        
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
        bossUIManager.SetBossInfo(gameObject, "��� ����");
    }

    public void EquipSmokeEvent()
    {
        swordGenSmoke.Play();
    }

    public void SetSwordEvent()
    {
        sword.SetActive(true);
    }


}
