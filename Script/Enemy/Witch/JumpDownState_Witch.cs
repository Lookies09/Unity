using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class JumpDownState_Witch : EnemyAttackAbleState
{
    private float jumpTime;

    public float jumpForce = 10f; // �⺻ ��
    public float xAngle = 45f; // x�� ���� (����/������ ����)
    public float yAngle = 45f; // y�� ���� (���� ����)

    // �����ٵ�
    private Rigidbody rb;

    private bool onGround;

    // ���� ���� ��ƼŬ
    [SerializeField] private ParticleSystem startSmoke;

    // �ٴڿ� �򸮴� ������
    [SerializeField] private ParticleSystem magicRing;

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
        animator.applyRootMotion = false;
        GetComponent<ObjectHealth>().IsHit = true;
        // ���� ��Ҹ�
        GetComponent<FSMController_Witch>().LaughVoice();
        Jump();
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        jumpTime += Time.deltaTime;

        // �����ð��� 3�� ������ ���̶��
        if (jumpTime > 3 && onGround)
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
        animator.SetFloat("JumpTime", jumpTime);
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        GetComponent<ObjectHealth>().IsHit = false;

        bossUIManager.SetBossInfo(gameObject, "����");
    }

    void Jump()
    {
        // ������ �������� ��ȯ
        float xAngleRad = xAngle * Mathf.Deg2Rad;
        float yAngleRad = yAngle * Mathf.Deg2Rad;

        // x��� y�� �ӵ� ���
        float xVelocity = jumpForce * Mathf.Cos(yAngleRad) * Mathf.Cos(xAngleRad);
        float yVelocity = jumpForce * Mathf.Sin(yAngleRad);
        float zVelocity = jumpForce * Mathf.Cos(yAngleRad) * Mathf.Sin(xAngleRad);

        // ���� �ռ�
        Vector3 jumpDirection = new Vector3(xVelocity, yVelocity, zVelocity);

        // ���� �ӵ��� �ʱ�ȭ�ϰ� ���� �ӵ��� ����
        rb.velocity = jumpDirection;
    }


    // �����浹 Ȯ��
    private void OnCollisionEnter(Collision collision)
    {
        // ���� �浹��
        if (collision.gameObject.tag == "Enviroment")
        {
            // ���� �Ҹ� ���
            GetComponent<FSMController_Witch>().MovementSound(0);
            navMeshAgent.enabled = true;
            Debug.Log("���� �⵹");
            jumpTime = 0;
            onGround = true;            

            if (navMeshAgent.enabled == true)
            {
                onGround = true;
                Destroy(rb);
                animator.applyRootMotion = true;
            }
        }
    }

    public void StartSmokeEvent()
    {
        GetComponent<FSMController_Witch>().SKillAttackVoice();
        startSmoke.Play();
        magicRing.Play();
    }
}
