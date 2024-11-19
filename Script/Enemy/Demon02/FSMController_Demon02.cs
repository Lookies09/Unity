using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Demon02 : EnemyController
{
    // �Ǹ� 1 ����
    [SerializeField] private GameObject demon01;

    public GameObject Demon01 { get => demon01; set => demon01 = value; }

    // �Ǹ�02 ���µ�
    public enum STATE { IDLE, JUMPDOWN, DETECT, SIDEWALK, ATTACK, DEATH }

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudio;

    // ������ ����� �ҽ�
    [SerializeField] private AudioSource[] movementAudios;

    // ��Ҹ� ����� Ŭ��
    [SerializeField] private AudioClip[] voiceClips;

    // �״� ��Ҹ� ����� Ŭ��
    [SerializeField] private AudioClip deadSound;

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

    public void BattlecryVoice()
    {
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
