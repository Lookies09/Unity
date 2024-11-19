using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StandUpState_Skeleton_Boss : EnemyAttackAbleState
{
    // ��� �ð�
    private float standupTime;

    // Į ���ӿ�����Ʈ ����
    [SerializeField] private GameObject swordSitState; // �ɾ������� Į
    [SerializeField] private GameObject swordBattleState; // ������ Į

    // ���۽� ����
    [SerializeField] private ParticleSystem startSmoke;

    // ���� ���� UI �Ŵ��� ����
    [SerializeField] private BossUIManager bossUIManager;



    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);        
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        standupTime += Time.deltaTime;

        // 3�ʰ� ����������
        if (standupTime < 3f)
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
            controller.TransactionToState(3);
        }
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        standupTime = 0;

        // ���� UI�� ���� �Է�
        bossUIManager.SetBossInfo(gameObject, "���̷��� ����");
    }

    // �� ik ��ȯ
    public void StandUpEvent(int num)
    {
        if (num == 0)
        {
            GetComponent<RigBuilder>().layers[1].active = false;
        }
        else
        {            
            GetComponent<RigBuilder>().layers[0].active = false;
        }

        
    }

    public void ChangeSwordEvent()
    {
        // Į ��ü
        swordSitState.SetActive(false);
        swordBattleState.SetActive(true);
    }

    public void ChangeSwordSmokeEvent()
    {
        startSmoke.Play();
    }


}
