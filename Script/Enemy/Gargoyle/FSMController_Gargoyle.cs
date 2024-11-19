using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Gargoyle : EnemyController
{
    // 가고일 상태들
    public enum STATE { IDLE, DOWN, DETECT, ATTACK, JUMPDOWN, DEATH }

    [SerializeField] private AudioSource voiceAudio;

    [SerializeField] private AudioSource[] battleAudios;

    [SerializeField] private AudioSource effectAudio;

    // 떨어질때 사운드
    [SerializeField] private AudioClip firstDownSound;

    // 가고일 으음 목소리
    [SerializeField] private AudioClip ummVoiceSound;

    // 가고일 마법 소리
    [SerializeField] private AudioClip magicSound;

    // 가고일 점프 소리
    [SerializeField] private AudioClip jumpSound;

    // 창 휘두르는 소리
    [SerializeField] private AudioClip spearSound;

    // 가고일 공격 목소리
    [SerializeField] private AudioClip[] attackVoices;

    // 가고일 착지공격 목소리
    [SerializeField] private AudioClip landingAttackVoice;

    // 사망 목소리
    [SerializeField] private AudioClip deadVoice;

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
