using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Witch : EnemyController
{
    // ���� ���µ�
    public enum STATE { IDLE, JUMPDOWN, DETECT, ATTACK, DEFENSE, HIT, DEATH }

    // ������ ����� �ҽ�
    [SerializeField] private AudioSource movementAudio;

    // ����Ʈ ����� �ҽ�
    [SerializeField] private AudioSource effectAudio;

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudio;

    // ���� �Ҹ�
    [SerializeField] private AudioClip laughSound;

    // �⺻���� ��Ҹ���
    [SerializeField] private AudioClip[] normalAttackVoices;

    // ��ų���� ��Ҹ���
    [SerializeField] private AudioClip[] skillAttackVoices;

    // ��Ʈ�� ��Ҹ�
    [SerializeField] private AudioClip onHitVoice;

    //����� ��Ҹ�
    [SerializeField] private AudioClip deadVoice;

    // ���� ������Ʈ ���Խ� ��Ҹ�
    [SerializeField] private AudioClip onShieldVoice;

    // 0�� - �����Ҹ�, 1�� - �ȴ� �Ҹ�
    [SerializeField] private AudioClip[] movementSound;

    public void MovementSound(int num)
    {
        movementAudio.clip = movementSound[num];
        movementAudio.Play();
    }

    // �⺻���� ��Ҹ� ���
    public void NormalAttackVoice()
    {
        int num = Random.Range(0, normalAttackVoices.Length);
        voiceAudio.clip = normalAttackVoices[num];
        voiceAudio.Play();
    }

    // ��ų���� ��Ҹ� ���
    public void SKillAttackVoice()
    {
        int num = Random.Range(0, skillAttackVoices.Length);
        voiceAudio.clip = skillAttackVoices[num];
        voiceAudio.Play();
    }

    // ��Ʈ�� ��Ҹ�
    public void OnHitVoice()
    {
        voiceAudio.clip = onHitVoice;
        voiceAudio.Play();
    }

    // ����� ��Ҹ�
    public void OnDeadVoice()
    {
        voiceAudio.Stop();
        voiceAudio.clip = deadVoice;
        voiceAudio.Play();
    }

    // ���� ��Ҹ�
    public void LaughVoice()
    {
        voiceAudio.clip = laughSound;
        voiceAudio.Play();
    }

    // ���� ��Ҹ�
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
