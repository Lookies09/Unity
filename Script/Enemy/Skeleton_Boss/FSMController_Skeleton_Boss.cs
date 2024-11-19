using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Skeleton_Boss : EnemyController
{
    // 스켈레톤 보스 상태들
    public enum STATE { IDLE, STANDUP, DETECT, ATTACK, DEATH }

    // 타격 위치
    [SerializeField] private Transform attackTransform;

    // 타겟 레이어
    [SerializeField] private LayerMask targetLayer;

    // 공격 범위
    [SerializeField] private float attackRadius;

    // 공격 범위 각도
    [SerializeField] private float hitAngle;

    // 근접공격 데미지
    [SerializeField] private int dmg;

    // 목소리 오디오 소스
    [SerializeField] private AudioSource voiceAudio;

    // 목소리 오디오 클립
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
