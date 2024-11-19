using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Skeleton_LightArmor : EnemyController
{
    // 경장갑 타입1 스켈레톤 상태들
    public enum STATE { IDLE, WANDER, DETECT, ATTACK, HIT, DEATH }

    // 타격 위치
    [SerializeField] public Transform attackTransform;

    // 타겟 레이어
    [SerializeField] public LayerMask targetLayer;

    // 공격 범위
    [SerializeField] public float attackRadius;

    // 공격 범위 각도
    [SerializeField] private float hitAngle;

    // 근접공격 데미지
    [SerializeField] public int dmg;

    // 목소리 오디오
    [SerializeField] private AudioSource voiceAudio;

    // 목소리 오디오 클립
    [SerializeField] private AudioClip[] voiceAudioClips;



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
        if (voiceAudio.isPlaying)
        {
            return;
        }

        int num = Random.Range(0, voiceAudioClips.Length);

        voiceAudio.clip = voiceAudioClips[num];

        voiceAudio.Play();
    }
}
