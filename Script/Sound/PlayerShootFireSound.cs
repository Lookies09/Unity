using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootFireSound : MonoBehaviour
{
    // ����� �ҽ�
    [SerializeField] private AudioSource audioSource;

    // �ѼҸ� Ŭ����
    [SerializeField] private AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(0, audioClips.Length);

        audioSource.clip = audioClips[num];
        audioSource.Play();
    }
}
