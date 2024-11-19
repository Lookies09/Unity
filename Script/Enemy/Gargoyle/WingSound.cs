using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingSound : MonoBehaviour
{
    [SerializeField] private AudioSource wingAudio;
    // ������ �Ҹ�
    [SerializeField] private AudioClip[] wingflapSounds;
    public void WingSoundEffect()
    {
        int num = Random.Range(0, wingflapSounds.Length);

        wingAudio.clip = wingflapSounds[num];
        wingAudio.Play();

    }
}
