using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Demon01 : EnemyController
{
    // 악마 2 참조
    [SerializeField] private GameObject demon02;

    // 목소리 오디오 소스
    [SerializeField] private AudioSource voiceAudio;

    // 움직임 오디오 소스
    [SerializeField] private AudioSource[] movementAudios;

    // 기상 오디오 클립들
    [SerializeField] private AudioClip[] wakeupVoices;

    // 목소리 오디오 클립
    [SerializeField] private AudioClip[] voiceClips;

    // 죽는 목소리 오디오 클립
    [SerializeField] private AudioClip deadSound;

    //===============================================================================
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

    // 히트 이펙트 위치
    [SerializeField] private Transform hitEffectPos;

    // 히트 이펙트 
    [SerializeField] private GameObject hitEffect;

    // 히트 사운드
    [SerializeField] private AudioClip[] hitSounds;
    //===============================================================================

    public GameObject Demon02 { get => demon02; set => demon02 = value; }

    // 악마01 상태들
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
