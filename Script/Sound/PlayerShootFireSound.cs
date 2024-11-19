using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootFireSound : MonoBehaviour
{
    // 오디오 소스
    [SerializeField] private AudioSource audioSource;

    // 총소리 클립들
    [SerializeField] private AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(0, audioClips.Length);

        audioSource.clip = audioClips[num];
        audioSource.Play();
    }
}
