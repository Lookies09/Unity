using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sound : MonoBehaviour
{
    // 움직임 오디오소스
    [SerializeField] private AudioSource movement_Audio;

    // 달리는 오디오 클립
    [SerializeField] private AudioClip[] runSoundClips;

    // 대쉬 오디오 클립
    [SerializeField] private AudioClip dashSoundClip;

    // 이펙트 오디오소스
    [SerializeField] private AudioSource effect_Audio;
    // 이펙트 오디오소스
    [SerializeField] private AudioSource effect_Audio_2;

    // 대쉬 이펙트 오디오 클립
    [SerializeField] private AudioClip dashEffectClip;

    // 스킬 이펙트 사운드 클립
    [SerializeField] private AudioClip skillEffectClip;

    // 장전 소리 사운드 클립
    [SerializeField] private AudioClip reloadSound;


    // 달리는 사운드 애니메이션 이벤트
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
