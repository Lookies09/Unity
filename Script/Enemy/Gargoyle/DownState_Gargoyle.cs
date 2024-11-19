using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class DownState_Gargoyle : EnemyAttackAbleState
{
    private float dropTime;

    // �����ٵ�
    private Rigidbody rb;

    // �� ���� Ȯ��
    private bool onGround;

    // �� ���� ����Ʈ
    [SerializeField] private ParticleSystem groundHitEffect;

    // ī�޶� ����ũ
    [SerializeField] private GameObject cameraShake;

    // �տ� �ִ� ����
    [SerializeField] private GameObject polearmOnHand;

    // �� �ƿ��
    [SerializeField] private ParticleSystem bodyAura;

    [SerializeField] private BossUIManager bossUIManager;


    public override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
    }

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �ִϸ��̼� ���
        animator.SetInteger("State", state);

        // ������ ������
        DropDown();
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        dropTime += Time.deltaTime;


        // �����ð��� 5�� ������ ���̶��
        if (dropTime > 5 && onGround)
        {           

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

        animator.SetBool("OnGround", onGround);
        animator.SetFloat("DropTime", dropTime);
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        animator.SetBool("OnGround", false);
        bossUIManager.SetBossInfo(gameObject, "������");

    }

    public void DropDown()
    {
        rb.velocity = Vector3.down * 50;
    }


    // �����浹 Ȯ��
    private void OnCollisionEnter(Collision collision)
    {
        // ���� �浹��
        if (collision.gameObject.tag == "Enviroment" && controller.CurrentState == controller.EnemyStates1[1])
        {
            GetComponent<FSMController_Gargoyle>().DownSound();


            groundHitEffect.Play();
            Instantiate(cameraShake, transform.position, Quaternion.identity);
            bodyAura.Play();
            onGround = true;
            navMeshAgent.enabled = true;
            dropTime = 0;

            if (navMeshAgent.enabled == true)
            {
                animator.applyRootMotion = true;
            }
        }
    }

    public void PolearmGenEvent()
    {
        polearmOnHand.SetActive(true);
    }
}
