using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Witch : EnemyController
{
    // 마녀 상태들
    public enum STATE { IDLE, JUMPDOWN, DETECT, ATTACK, DEFENSE, HIT, DEATH }

    // 움직임 오디오 소스
    [SerializeField] private AudioSource movementAudio;

    // 이펙트 오디오 소스
    [SerializeField] private AudioSource effectAudio;

    // 목소리 오디오 소스
    [SerializeField] private AudioSource voiceAudio;

    // 웃는 소리
    [SerializeField] private AudioClip laughSound;

    // 기본공격 목소리들
    [SerializeField] private AudioClip[] normalAttackVoices;

    // 스킬공격 목소리들
    [SerializeField] private AudioClip[] skillAttackVoices;

    // 히트시 목소리
    [SerializeField] private AudioClip onHitVoice;

    //사망시 목소리
    [SerializeField] private AudioClip deadVoice;

    // 쉴드 스테이트 진입시 목소리
    [SerializeField] private AudioClip onShieldVoice;

    // 0번 - 착지소리, 1번 - 걷는 소리
    [SerializeField] private AudioClip[] movementSound;

    public void MovementSound(int num)
    {
        movementAudio.clip = movementSound[num];
        movementAudio.Play();
    }

    // 기본공격 목소리 재생
    public void NormalAttackVoice()
    {
        int num = Random.Range(0, normalAttackVoices.Length);
        voiceAudio.clip = normalAttackVoices[num];
        voiceAudio.Play();
    }

    // 스킬공격 목소리 재생
    public void SKillAttackVoice()
    {
        int num = Random.Range(0, skillAttackVoices.Length);
        voiceAudio.clip = skillAttackVoices[num];
        voiceAudio.Play();
    }

    // 히트시 목소리
    public void OnHitVoice()
    {
        voiceAudio.clip = onHitVoice;
        voiceAudio.Play();
    }

    // 사망시 목소리
    public void OnDeadVoice()
    {
        voiceAudio.Stop();
        voiceAudio.clip = deadVoice;
        voiceAudio.Play();
    }

    // 웃는 목소리
    public void LaughVoice()
    {
        voiceAudio.clip = laughSound;
        voiceAudio.Play();
    }

    // 방어시 목소리
    public void OnShieldVoice()
    {
        voiceAudio.clip = onShieldVoice;
        voiceAudio.Play();
    }

    public void EffectSound()
    {
        effectAudio.Play();
    }
}
