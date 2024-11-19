using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sound : MonoBehaviour
{
    // ������ ������ҽ�
    [SerializeField] private AudioSource movement_Audio;

    // �޸��� ����� Ŭ��
    [SerializeField] private AudioClip[] runSoundClips;

    // �뽬 ����� Ŭ��
    [SerializeField] private AudioClip dashSoundClip;

    // ����Ʈ ������ҽ�
    [SerializeField] private AudioSource effect_Audio;
    // ����Ʈ ������ҽ�
    [SerializeField] private AudioSource effect_Audio_2;

    // �뽬 ����Ʈ ����� Ŭ��
    [SerializeField] private AudioClip dashEffectClip;

    // ��ų ����Ʈ ���� Ŭ��
    [SerializeField] private AudioClip skillEffectClip;

    // ���� �Ҹ� ���� Ŭ��
    [SerializeField] private AudioClip reloadSound;


    // �޸��� ���� �ִϸ��̼� �̺�Ʈ
    public void RunningSound()
    {
        int num = Random.Range(0, runSoundClips.Length);
        movement_Audio.clip = runSoundClips[num];
        movement_Audio.Play();
    }

    public void DashSound()
    {
        movement_Audio.clip = dashSoundClip;
        movement_Audio.Play();

        effect_Audio.volume = 1;
        effect_Audio.clip = dashEffectClip;
        effect_Audio.Play();
    }

    public void SkillSound()
    {
        effect_Audio.volume = 0.7f;
        effect_Audio.clip = skillEffectClip;
        
        effect_Audio.Play();
        
    }

    public void SkillSound2()
    {
        effect_Audio.volume = 0.7f;
        effect_Audio_2.clip = skillEffectClip;
        effect_Audio_2.Play();
    }

    public void ReloadSound()
    {
        effect_Audio.volume = 0.7f;
        effect_Audio.clip = reloadSound;
        effect_Audio.Play();
    }

}
