using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_NormalSkeleton : EnemyController
{
    // 일반 스켈레톤 상태들
    private enum STATE { IDLE, STANDUP, DETECT, ATTACK, HIT, DEATH }

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

    // 목소리 오디오
    [SerializeField] private AudioSource voiceAudio;

    // 목소리 오디오 클립
    [SerializeField] private AudioClip[] voiceAudioClips;

    // 공격 목소리 오디오 클립
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
                    Debug.Log("플레이어가 맞음");
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
