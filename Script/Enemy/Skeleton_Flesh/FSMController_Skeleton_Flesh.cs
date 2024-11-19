using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Skeleton_Flesh : EnemyController
{
    // 살점 스켈레톤 상태들
    public enum STATE { IDLE, DETECT, DEATH }

    // 목소리 오디오 소스
    [SerializeField] private AudioSource voiceAudio;

    // 움직임 오디오
    [SerializeField] private AudioSource movementAudio;


    public void PlaySound()
    {
        voiceAudio.Play();
        movementAudio.Play();
    }
}
