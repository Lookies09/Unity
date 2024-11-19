using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_DarkLord : EnemyController
{
    // ��ũ�ε� ���µ�
    public enum STATE { IDLE, EQUIP, DETECT, ATTACK, AVOID, DEAD }

    // ������ �����
    [SerializeField] private AudioSource movementAudio;
    // ���� ����� (Į �ֵθ��� �Ҹ�)
    [SerializeField] private AudioSource battleAudio;
    // ���� �����2 (���� �Ҹ�)
    [SerializeField] private AudioSource battleAudio2;
    // ����Ʈ �����
    [SerializeField] private AudioSource effectAudio;
    // ��Ҹ� �����
    [SerializeField] private AudioSource voiceAudio;

    // ���� �Ҹ�
    [SerializeField] private AudioClip equipSound;

    // �ȴ� �Ҹ�
    [SerializeField] private AudioClip[] walkSounds;

    
    // Į �ֵθ��� �Ҹ�
    [SerializeField] private AudioClip[] sowrdSwingSounds;

    // ū Į ��ȯ �Ҹ�
    [SerializeField] private AudioClip bigSwordGenSound;

    // ū Į �ֵθ��� �Ҹ� 0 - ���, 1 - ����
    [SerializeField] private AudioClip[] bigSwordSwingSounds;

    // ȸ�� �Ҹ�
    [SerializeField] private AudioClip avoidSound;

    // ���� �Ҹ�
    [SerializeField] private AudioClip jumpSound;

    // �ڷ���Ʈ �Ҹ� (���� ������� �Ҹ�)
    [SerializeField] private AudioClip teleportSound;

    // ���� �浹�ϴ� �Ҹ�
    [SerializeField] private AudioClip groundHitSound;

    // ������ �Ҹ�
    [SerializeField] private AudioClip rollSound;

    // �Ϲ� ���� �Ҹ�
    [SerializeField] private AudioClip[] magicSounds;

    // �޺� ���� �Ҹ�
    [SerializeField] private AudioClip comboMagicSound;
    
    // ���� ���� ���� ���� �Ҹ�
    [SerializeField] private AudioClip jumpAttackMagicSound;

    // ���� ���� �Ҹ�
    [SerializeField] private AudioClip[] bigMagicSounds;

    // �Ϲ� ���ռҸ�
    [SerializeField] private AudioClip[] normalVoices;

    // ū ���ռҸ�
    [SerializeField] private AudioClip[] bigVoices;

    // ��� ��Ҹ�
    [SerializeField] private AudioClip warningVoice;

    // ��� ��Ҹ�
    [SerializeField] private AudioClip deadVoice;

    //===============================================================================
    // Ÿ�� ��ġ
    [SerializeField] public Transform attackTransform;

    // Ÿ�� ���̾�
    [SerializeField] public LayerMask targetLayer;

    // ���� ����
    [SerializeField] public float attackRadius;

    // ���� ���� ����
    [SerializeField] private float hitAngle;

    // �������� ������
    [SerializeField] public int dmg;

    // ��Ʈ ����Ʈ ��ġ
    [SerializeField] private Transform hitEffectPos;

    // ��Ʈ ����Ʈ 
    [SerializeField] private GameObject hitEffect;

    // ��Ʈ ����
    [SerializeField] private AudioClip[] hitSounds;
    //===============================================================================


    public void EquipSound()
    {
        effectAudio.clip = equipSound;
        effectAudio.Play();
    }

    public void WalkSound()
    {
        int num = Random.Range(0, walkSounds.Length);
        movementAudio.clip = walkSounds[num];
        movementAudio.Play();
    }

    public void SwordSwingSound()
    {
        int num = Random.Range(0, sowrdSwingSounds.Length);
        battleAudio.clip = sowrdSwingSounds[num];
        battleAudio.Play();
    }

    public void BigSwordGenSound()
    {
        effectAudio.clip = bigSwordGenSound;
        effectAudio.Play();
    }

    public void BigSwordSwingSound(int num)
    {
        battleAudio.clip = bigSwordSwingSounds[num];
        battleAudio.Play();
    }

    public void AvoicSound()
    {
        movementAudio.clip = avoidSound;
        movementAudio.Play();
    }

    public void JumpSound()
    {
        movementAudio.clip = jumpSound;
        movementAudio.Play();
    }

    public void TeleportSound()
    {
        effectAudio.clip = teleportSound;
        effectAudio.Play();
    }

    public void GroundHitSound()
    {
        effectAudio.clip = groundHitSound;
        effectAudio.Play();
    }

    public void RollSound()
    {
        movementAudio.clip = rollSound;
        movementAudio.Play();
    }

    public void NormalMagicSound()
    {
        int num = Random.Range(0, magicSounds.Length);

        battleAudio2.clip = magicSounds[num];
        battleAudio2.Play();
    }

    public void ComboMagicSound()
    {
        battleAudio2.clip = comboMagicSound;
        battleAudio2.Play();
    }

    public void JumpAttackMagicSound()
    {
        battleAudio2.clip = jumpAttackMagicSound;
        battleAudio2.Play();
    }

    public void BigMagicSound()
    {
        int num = Random.Range(0, bigMagicSounds.Length);

        battleAudio2.clip = bigMagicSounds[num];
        battleAudio2.Play();
    }

    public void NormalVoice()
    {
        int num = Random.Range(0, normalVoices.Length);

        voiceAudio.clip = normalVoices[num];
        voiceAudio.Play();
    }

    public void BigVoice()
    {
        int num = Random.Range(0, bigVoices.Length);

        voiceAudio.clip = bigVoices[num];
        voiceAudio.Play();
    }

    public void WarningVoice()
    {
        voiceAudio.clip = warningVoice;
        voiceAudio.Play();
    }

    public void DeadVoice()
    {
        voiceAudio.clip = deadVoice;
        voiceAudio.Play();
    }

    public void EnemyAtteckAnimationEvent(int a)
    {
        Collider[] hits = Physics.OverlapSphere(attackTransform.position, attackRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            Vector3 Direction = hit.transform.position - transform.position;

            float angleToTarget = Vector3.Angle(transform.forward, Direction);

            if (angleToTarget < hitAngle)
            {

                if (hit.GetComponent<ObjectHealth>().Health > 0)
                {
                    GameObject effect = Instantiate(hitEffect, hitEffectPos.position, Quaternion.identity);

                    effect.GetComponent<AudioSource>().clip = hitSounds[a];
                    effect.GetComponent<AudioSource>().Play();
                    hit.GetComponent<ObjectHealth>().Hit(dmg);
                    return;
                }

            }
        }
    }
}
