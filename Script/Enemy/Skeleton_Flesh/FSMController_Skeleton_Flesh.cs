using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Skeleton_Flesh : EnemyController
{
    // ���� ���̷��� ���µ�
    public enum STATE { IDLE, DETECT, DEATH }

    // ��Ҹ� ����� �ҽ�
    [SerializeField] private AudioSource voiceAudio;

    // ������ �����
    [SerializeField] private AudioSource movementAudio;


    public void PlaySound()
    {
        voiceAudio.Play();
        movementAudio.Play();
    }
}
