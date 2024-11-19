using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_NormalSkeleton : EnemyController
{
    // �Ϲ� ���̷��� ���µ�
    private enum STATE { IDLE, STANDUP, DETECT, ATTACK, HIT, DEATH }

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

    // ��Ҹ� �����
    [SerializeField] private AudioSource voiceAudio;

    // ��Ҹ� ����� Ŭ��
    [SerializeField] private AudioClip[] voiceAudioClips;

    // ���� ��Ҹ� ����� Ŭ��
    [SerializeField] private AudioClip[] attackVoiceClips;
 


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
                    Debug.Log("�÷��̾ ����");
                    // Instantiate(hitEffect, hitEffectPos.position, Quaternion.identity);
                    hit.GetComponent<ObjectHealth>().Hit(dmg);
                    return;
                }

            }
        }
    }

    public void VoiceAudioPlay()
    {
        if (voiceAudio.isPlaying || GetComponent<ObjectHealth>().IsDead)
        {
            return;
        }

        int num = Random.Range(0, voiceAudioClips.Length);

        voiceAudio.clip = voiceAudioClips[num];

        voiceAudio.Play();        
    }

    public void AttackVoicePlay()
    {
        if (voiceAudio.isPlaying)
        {
            voiceAudio.Stop();
        }

        int num = Random.Range(0, attackVoiceClips.Length);

        voiceAudio.clip = attackVoiceClips[num];

        voiceAudio.Play();
    }

}
