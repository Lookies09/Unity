using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Skeleton_Boss : EnemyController
{
    // ���̷��� ���� ���µ�
    public enum STATE { IDLE, STANDUP, DETECT, ATTACK, DEATH }

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

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudio;

    // ��Ҹ� ����� Ŭ��
    [SerializeField] private AudioClip[] voiceClips;

    protected override void Update()
    {
        base.Update();
        if (!GetComponent<ObjectHealth>().IsDead) 
        {
            VoiceSound();
        }
        
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
                    // Instantiate(hitEffect, hitEffectPos.position, Quaternion.identity);
                    hit.GetComponent<ObjectHealth>().Hit(dmg);
                    return;
                }

            }
        }
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
}
