using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Demon01 : EnemyController
{
    // �Ǹ� 2 ����
    [SerializeField] private GameObject demon02;

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudio;

    // ������ ����� �ҽ�
    [SerializeField] private AudioSource[] movementAudios;

    // ��� ����� Ŭ����
    [SerializeField] private AudioClip[] wakeupVoices;

    // ��Ҹ� ����� Ŭ��
    [SerializeField] private AudioClip[] voiceClips;

    // �״� ��Ҹ� ����� Ŭ��
    [SerializeField] private AudioClip deadSound;

    //===============================================================================
    // Ÿ�� ��ġ
    [SerializeField] private Transform attackTransform;

    // Ÿ�� ���̾�
    [SerializeField] private LayerMask targetLayer;

    // ���� ����
    [SerializeField] private float attackRadius;

    // ���� ���� ����
    [SerializeField] private float hitAngle;

    // �������� ������
    [SerializeField] private int dmg;

    // ��Ʈ ����Ʈ ��ġ
    [SerializeField] private Transform hitEffectPos;

    // ��Ʈ ����Ʈ 
    [SerializeField] private GameObject hitEffect;

    // ��Ʈ ����
    [SerializeField] private AudioClip[] hitSounds;
    //===============================================================================

    public GameObject Demon02 { get => demon02; set => demon02 = value; }

    // �Ǹ�01 ���µ�
    public enum STATE { IDLE, STANDUP, DETECT, SIDEWALK, ATTACK, DEATH }

    public void StandUpVoice()
    {
        voiceAudio.loop = false;
        voiceAudio.clip = wakeupVoices[0];
        voiceAudio.Play();
    }

    public void StandUpVoice2()
    {
        voiceAudio.clip = wakeupVoices[1];
        voiceAudio.Play();
    }

    public void VoiceSound()
    {
        if (voiceAudio.isPlaying)
        {
            return;
        }

        int num = Random.Range(0, voiceClips.Length);

        voiceAudio.clip = voiceClips[num];

        voiceAudio.Play();

    }    

    public void DeadSound()
    {
        voiceAudio.clip = deadSound;
        voiceAudio.Play();
    }

    public void RunSound(int num)
    {
        movementAudios[num].Play();
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
                    int num = Random.Range(0, hitSounds.Length);
                    effect.GetComponent<AudioSource>().clip = hitSounds[num];
                    effect.GetComponent<AudioSource>().Play();
                    hit.GetComponent<ObjectHealth>().Hit(dmg);
                    return;
                }

            }
        }
    }
}
