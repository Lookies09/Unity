using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Darklord : EnemyAttackAbleState
{
    // �ð�
    private float time;

    // ���� ���� �ð�
    private float ADelaytime = 0;

    // ���� ��Ʈ�ѷ�
    private AttackController attackController;

    // �����ٵ�
    private Rigidbody rb;

    // ���� ���� Ÿ��
    private int ranAttackInt;

    // ���� ���� Ÿ�� ���
    private int beforeAtInt = -1;

    // ���� �浹 Ȯ��
    private bool onGround = true;

    // �߻�ü
    [SerializeField] private GameObject bulletPrefeb;

    // ū �߻�ü
    [SerializeField] private GameObject bigBulletPrefeb;

    // �� Į
    [SerializeField] private GameObject longSword;

    // �߻� ��ġ �迭
    [SerializeField] private Transform[] shootPos;

    // �׶��� ��Ʈ ����Ʈ
    [SerializeField] private ParticleSystem groundHit;
    // �����̵� ���� ����Ʈ
    [SerializeField] private ParticleSystem[] teleportSmokeEffects;

    public override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        attackController = GetComponent<AttackController>();
    }

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ������ ���� �̵� ����
        navMeshAgent.isStopped = true;

        while (true)
        {
            // ���� ���� ���� ��÷
            ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length);

            if (beforeAtInt != ranAttackInt)
            {
                beforeAtInt = ranAttackInt;
                break;
            }

        }

        // ���� Ÿ�Ժ� �ð� �Է�
        if (ranAttackInt == 0) { ADelaytime = 1.5f; } // �Ѽ� ����
        else if (ranAttackInt == 1) { ADelaytime = 3f; } // �޺� ����
        else if (ranAttackInt == 2) { ADelaytime = 2.6f; } // ���� ����
        else if (ranAttackInt == 3) { ADelaytime = 4f; } // �ڷ���Ʈ ����
        else if (ranAttackInt == 4) { ADelaytime = 6f; } // 1�� ���� ����
        else if (ranAttackInt == 5) { ADelaytime = 4.5f; } // 2�� ���� ����
        else { ADelaytime = 6f; } // �� Į ����


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

        animator.SetFloat("ATTime", time);
        animator.SetBool("OnGround", onGround);
    }

    

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
        ADelaytime = 0;
        ranAttackInt = 0;
        onGround = false;
    }

    // ���� ���� �̺�Ʈ,
    public void Shoot(int num)
    {
        Quaternion rota = shootPos[0].rotation;
        Vector3 rotate = rota.eulerAngles;

        // ���� ���ֺ��� ��� ���� ����
        if (num == 0)
        {
            Instantiate(bulletPrefeb, shootPos[0].position, shootPos[0].rotation);
        }
        else if (num == 1) // �� ��� ���� �ð�������� 45��
        {
            Instantiate(bulletPrefeb, shootPos[0].position, Quaternion.Euler(rotate.x, rotate.y, 45));
        }
        else if (num == 2) // �� ��� ���� �ݽð�������� 45��
        {
            Instantiate(bulletPrefeb, shootPos[0].position, Quaternion.Euler(rotate.x, rotate.y, -45));
        }
        else // �� ��� ���� 
        {
            Instantiate(bulletPrefeb, shootPos[0].position, Quaternion.Euler(rotate.x, rotate.y, 90));
        }
    }

    public void BigShoot()
    {
        Quaternion rota = shootPos[0].rotation;
        Vector3 rotate = rota.eulerAngles;

        GetComponent<FSMController_DarkLord>().BigMagicSound();
        Instantiate(bigBulletPrefeb, shootPos[0].position, Quaternion.Euler(rotate.x, rotate.y, 90));
    }

    // ��Ƽ ���� �̺�Ʈ
    public void MultiShoot()
    {
        GetComponent<FSMController_DarkLord>().JumpAttackMagicSound();
        for (int i = 0; i < shootPos.Length; i++)
        {
            Instantiate(bulletPrefeb, shootPos[i].position, shootPos[i].rotation);
        }
    }

    public void GroundHitEffectEvent()
    {
        groundHit.Play();
    }

    public void TeleportSmokeEvent(int num)
    {
        if (num == 0)
        {
            // ������
            teleportSmokeEffects[0].Play();
        }
        else
        {
            // ������
            teleportSmokeEffects[1].Play();
        }
        
    }

    public void TeleportStart()
    {
        onGround = false;

        // �����̵��� �׺�޽�, ��Ʈ��� ����
        navMeshAgent.enabled = false;
        animator.applyRootMotion = false;
        
        float rangeX = Random.Range(-2f, 2f);
        float rangeZ = Random.Range(-2f, 2f);

        Vector3 pos = new Vector3(
            controller.Player.transform.position.x + rangeX,
            controller.Player.transform.position.y + 3,
            controller.Player.transform.position.z + rangeZ
            );

        GetComponent<FSMController_DarkLord>().TeleportSound();
        gameObject.transform.position = pos;

        TeleportSmokeEvent(1);
    }

    public void TeleportEnd()
    {
        rb.velocity = Vector3.down * 80;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enviroment" && onGround == false && navMeshAgent.enabled == false)
        {
            GetComponent<FSMController_DarkLord>().GroundHitSound();
            GroundHitEffectEvent();
            onGround = true;
            navMeshAgent.enabled = true;
            animator.applyRootMotion = true;
        }
    }

    public void LongSwordGenEventStart()
    {
        GetComponent<FSMController_DarkLord>().BigSwordGenSound();
        longSword.SetActive(true);
        
    }

    public void LongSwordGenEventEnd()
    {
        longSword.SetActive(false);
    }
}
