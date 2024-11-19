using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_DarkLord : EnemyController
{
    // 다크로드 상태들
    public enum STATE { IDLE, EQUIP, DETECT, ATTACK, AVOID, DEAD }

    // 움직임 오디오
    [SerializeField] private AudioSource movementAudio;
    // 전투 오디오 (칼 휘두르는 소리)
    [SerializeField] private AudioSource battleAudio;
    // 전투 오디오2 (마법 소리)
    [SerializeField] private AudioSource battleAudio2;
    // 이펙트 오디오
    [SerializeField] private AudioSource effectAudio;
    // 목소리 오디오
    [SerializeField] private AudioSource voiceAudio;

    // 착검 소리
    [SerializeField] private AudioClip equipSound;

    // 걷는 소리
    [SerializeField] private AudioClip[] walkSounds;

    
    // 칼 휘두르는 소리
    [SerializeField] private AudioClip[] sowrdSwingSounds;

    // 큰 칼 소환 소리
    [SerializeField] private AudioClip bigSwordGenSound;

    // 큰 칼 휘두르는 소리 0 - 찌르기, 1 - 베기
    [SerializeField] private AudioClip[] bigSwordSwingSounds;

    // 회피 소리
    [SerializeField] private AudioClip avoidSound;

    // 점프 소리
    [SerializeField] private AudioClip jumpSound;

    // 텔레포트 소리 (순간 사라질때 소리)
    [SerializeField] private AudioClip teleportSound;

    // 땅에 충돌하는 소리
    [SerializeField] private AudioClip groundHitSound;

    // 구르는 소리
    [SerializeField] private AudioClip rollSound;

    // 일반 마법 소리
    [SerializeField] private AudioClip[] magicSounds;

    // 콤보 마법 소리
    [SerializeField] private AudioClip comboMagicSound;
    
    // 점프 착지 공격 마법 소리
    [SerializeField] private AudioClip jumpAttackMagicSound;

    // 대형 마법 소리
    [SerializeField] private AudioClip[] bigMagicSounds;

    // 일반 기합소리
    [SerializeField] private AudioClip[] normalVoices;

    // 큰 기합소리
    [SerializeField] private AudioClip[] bigVoices;

    // 경고 목소리
    [SerializeField] private AudioClip warningVoice;

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
    [SerializeField] private AudioClip[] hitSounds;
    //===============================================================================


    public void EquipSound()
    {
        effectAudio.clip = equipSound;
        effectAudio.Play();
    }

    public void WalkSound()
    {
        int num = Random.Range(0, walkSounds.Length);
        movementAudio.clip = walkSounds[num];
        movementAudio.Play();
    }

    public void SwordSwingSound()
    {
        int num = Random.Range(0, sowrdSwingSounds.Length);
        battleAudio.clip = sowrdSwingSounds[num];
        battleAudio.Play();
    }

    public void BigSwordGenSound()
    {
        effectAudio.clip = bigSwordGenSound;
        effectAudio.Play();
    }

    public void BigSwordSwingSound(int num)
    {
        battleAudio.clip = bigSwordSwingSounds[num];
        battleAudio.Play();
    }

    public void AvoicSound()
    {
        movementAudio.clip = avoidSound;
        movementAudio.Play();
    }

    public void JumpSound()
    {
        movementAudio.clip = jumpSound;
        movementAudio.Play();
    }

    public void TeleportSound()
    {
        effectAudio.clip = teleportSound;
        effectAudio.Play();
    }

    public void GroundHitSound()
    {
        effectAudio.clip = groundHitSound;
        effectAudio.Play();
    }

    public void RollSound()
    {
        movementAudio.clip = rollSound;
        movementAudio.Play();
    }

    public void NormalMagicSound()
    {
        int num = Random.Range(0, magicSounds.Length);

        battleAudio2.clip = magicSounds[num];
        battleAudio2.Play();
    }

    public void ComboMagicSound()
    {
        battleAudio2.clip = comboMagicSound;
        battleAudio2.Play();
    }

    public void JumpAttackMagicSound()
    {
        battleAudio2.clip = jumpAttackMagicSound;
        battleAudio2.Play();
    }

    public void BigMagicSound()
    {
        int num = Random.Range(0, bigMagicSounds.Length);

        battleAudio2.clip = bigMagicSounds[num];
        battleAudio2.Play();
    }

    public void NormalVoice()
    {
        int num = Random.Range(0, normalVoices.Length);

        voiceAudio.clip = normalVoices[num];
        voiceAudio.Play();
    }

    public void BigVoice()
    {
        int num = Random.Range(0, bigVoices.Length);

        voiceAudio.clip = bigVoices[num];
        voiceAudio.Play();
    }

    public void WarningVoice()
    {
        voiceAudio.clip = warningVoice;
        voiceAudio.Play();
    }

    public void DeadVoice()
    {
        voiceAudio.clip = deadVoice;
        voiceAudio.Play();
    }

    public void EnemyAtteckAnimationEvent(int a)
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

                    effect.GetComponent<AudioSource>().clip = hitSounds[a];
                    effect.GetComponent<AudioSource>().Play();
                    hit.GetComponent<ObjectHealth>().Hit(dmg);
                    return;
                }

            }
        }
    }
}
