using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Gargoyle : EnemyController
{
    // ������ ���µ�
    public enum STATE { IDLE, DOWN, DETECT, ATTACK, JUMPDOWN, DEATH }

    [SerializeField] private AudioSource voiceAudio;

    [SerializeField] private AudioSource[] battleAudios;

    [SerializeField] private AudioSource effectAudio;

    // �������� ����
    [SerializeField] private AudioClip firstDownSound;

    // ������ ���� ��Ҹ�
    [SerializeField] private AudioClip ummVoiceSound;

    // ������ ���� �Ҹ�
    [SerializeField] private AudioClip magicSound;

    // ������ ���� �Ҹ�
    [SerializeField] private AudioClip jumpSound;

    // â �ֵθ��� �Ҹ�
    [SerializeField] private AudioClip spearSound;

    // ������ ���� ��Ҹ�
    [SerializeField] private AudioClip[] attackVoices;

    // ������ �������� ��Ҹ�
    [SerializeField] private AudioClip landingAttackVoice;

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
    [SerializeField] private AudioClip hitSound;
    //===============================================================================

    public void DownSound()
    {
        effectAudio.clip = firstDownSound;
        effectAudio.Play();
    }

    public void UmmSound()
    {
        voiceAudio.clip = ummVoiceSound;
        voiceAudio.Play();
    }

    public void MagicSound(int num)
    {
        battleAudios[num].clip = magicSound;
        battleAudios[num].Play();
    }

    public void SpearSound()
    {
        battleAudios[0].clip = spearSound;
        battleAudios[0].Play();
    }

    public void JumpSound() 
    {
        effectAudio.clip = jumpSound;
        effectAudio.Play();
    }

    public void AttackVoice(int num)
    {        
        voiceAudio.clip = attackVoices[num];
        voiceAudio.Play();
    }

    public void LandingAttackVoice() 
    {
        voiceAudio.clip = landingAttackVoice;
        voiceAudio.Play();
    }

    public void DeadVoice() 
    {
        voiceAudio.clip = deadVoice;
        voiceAudio.Play();
    }

    public void EnemyAtteckAnimationEvent()
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

                    effect.GetComponent<AudioSource>().clip = hitSound;
                    effect.GetComponent<AudioSource>().Play();
                    hit.GetComponent<ObjectHealth>().Hit(dmg);
                    return;
                }

            }
        }
    }

}
