using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpDownState_Demon02 : EnemyAttackAbleState
{
    [SerializeField] private AudioSource effectAudio;

    private float jumpTime;

    // �����ٵ�
    private Rigidbody rb;

    [SerializeField] private float force; // �ʱ� ��
    [SerializeField] private float angle; // ����

    // �� ���� Ȯ��
    private bool onGround;

    // ��Ʋ ũ���� ����Ʈ
    [SerializeField] private ParticleSystem battleCryEffect;

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

        navMeshAgent.enabled = false;

        // ����
        Jump();
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        jumpTime += Time.deltaTime;

        // �߷� ����
        Vector3 customGravity = 1.5f * Physics.gravity;
        rb.AddForce(customGravity, ForceMode.Acceleration);

        // �����ð��� 5�� ������ ���̶��
        if (jumpTime > 5 && onGround)
        {
            Destroy(rb);

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

        animator.SetBool("OnGround", onGround);
        animator.SetFloat("JumpTime", jumpTime);
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        bossUIManager.SetBossInfo(gameObject, "�Ǹ� 2", 2);
    }

    public void Jump()
    {
        // ������ �������� ��ȯ
        float radianAngle = angle * Mathf.PI / 180f;

        // �ʱ� �ӵ� ���
        Vector3 initialVelocity = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0f) * force;

        // �ʱ� �ӵ� ����
        rb.velocity = initialVelocity;        
    }

    // �����浹 Ȯ��
    private void OnCollisionEnter(Collision collision)
    {
        // ���� �浹��
        if (collision.gameObject.tag == "Enviroment")
        {
            effectAudio.Play();

            onGround = true;
            navMeshAgent.enabled = true;
            Debug.Log("���� �⵹");
            jumpTime = 0;

            if (navMeshAgent.enabled == true)
            {
                animator.applyRootMotion = true;
            }
        }
    }

    public void StartBattleCryEvent()
    {
        battleCryEffect.Play();
    }
}
