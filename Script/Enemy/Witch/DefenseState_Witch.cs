using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseState_Witch : EnemyAttackAbleState
{
    // ��� ���� �ð�
    [SerializeField] private float defenseTime;

    // 2������ ����
    [SerializeField] private ParticleSystem phase2Smoke;

    // ���� ����Ʈ
    [SerializeField] private ParticleSystem areaEffect;

    // �Ӹ�ī�� ���ӿ�����Ʈ
    [SerializeField] private GameObject hair;

    // ��Ʈ���� ���ӿ�����Ʈ
    [SerializeField] private Material whiteMatrial;    

    // �ð�
    private float time;

    // ��� ����Ʈ
    [SerializeField] private ParticleSystem defenseEffect;

    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // ��� �ִϸ��̼� ���
        animator.SetInteger("State", state);

        // ��Ʈ ���·� �ȳѾ� ������
        GetComponent<ObjectHealth>().IsHit = true;

        defenseEffect.Play();
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        animator.SetFloat("DefenseTime", time);

        // ��� �ð��� ���������� ����
        if (defenseTime > time) { return; }

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
        time = 0;       

        // ��Ʈ ���·� �ȳѾ� ������
        GetComponent<ObjectHealth>().IsHit = false;        
    }

    public void Phase2Event()
    {
        // ��� ��Ҹ�
        GetComponent<FSMController_Witch>().OnShieldVoice();
        phase2Smoke.Play();
        areaEffect.Play();
        hair.GetComponent<SkinnedMeshRenderer>().material = whiteMatrial;
    }
}
