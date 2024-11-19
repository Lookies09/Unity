using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Skeleton_Ranger : EnemyController
{
    // ������ ���̷��� ���µ�
    public enum STATE { IDLE, WANDER, DETECT, ATTACK, HIT, DEATH }

    // ��Ҹ� �����
    [SerializeField] private AudioSource voiceAudio;

    

    // ��Ҹ� ����� Ŭ��
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
