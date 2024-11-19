using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController_Skeleton_LightArmor : EnemyController
{
    // ���尩 Ÿ��1 ���̷��� ���µ�
    public enum STATE { IDLE, WANDER, DETECT, ATTACK, HIT, DEATH }

    // Ÿ�� ��ġ
    [SerializeField] public Transform attackTransform;

    // Ÿ�� ���̾�
    [SerializeField] public LayerMask targetLayer;

    // ���� ����
    [SerializeField] public float attackRadius;

    // ���� ���� ����
    [SerializeField] private float hitAngle;

    // �������� ������
    [SerializeField] public int dmg;

    // ��Ҹ� �����
    [SerializeField] private AudioSource voiceAudio;

    // ��Ҹ� ����� Ŭ��
    [SerializeField] private AudioClip[] voiceAudioClips;



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
                    Debug.Log("�÷��̾ ����");
                    // Instantiate(hitEffect, hitEffectPos.position, Quaternion.identity);
                    hit.GetComponent<ObjectHealth>().Hit(dmg);
                    return;
                }

            }
        }
    }

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
