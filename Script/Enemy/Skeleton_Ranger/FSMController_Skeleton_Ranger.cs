using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Skeleton_Ranger : EnemyController
{
    // 레인저 스켈레톤 상태들
    public enum STATE { IDLE, WANDER, DETECT, ATTACK, HIT, DEATH }

    // 목소리 오디오
    [SerializeField] private AudioSource voiceAudio;

    

    // 목소리 오디오 클립
    [SerializeField] private AudioClip[] voiceAudioClips;



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
