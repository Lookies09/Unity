using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Demon02 : EnemyController
{
    // 악마 1 참조
    [SerializeField] private GameObject demon01;

    public GameObject Demon01 { get => demon01; set => demon01 = value; }

    // 악마02 상태들
    public enum STATE { IDLE, JUMPDOWN, DETECT, SIDEWALK, ATTACK, DEATH }

    // 목소리 오디오 소스
    [SerializeField] private AudioSource voiceAudio;

    // 움직임 오디오 소스
    [SerializeField] private AudioSource[] movementAudios;

    // 목소리 오디오 클립
    [SerializeField] private AudioClip[] voiceClips;

    // 죽는 목소리 오디오 클립
    [SerializeField] private AudioClip deadSound;

    //===============================================================================
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

    // 히트 이펙트 위치
    [SerializeField] private Transform hitEffectPos;

    // 히트 이펙트 
    [SerializeField] private GameObject hitEffect;

    // 히트 사운드
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
