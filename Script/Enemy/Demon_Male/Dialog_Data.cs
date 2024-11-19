using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 대화 텍스트 스크립터블 오브젝트
[CreateAssetMenu(fileName = "Dialog_Data", menuName = "Dialog/Text")]
public class Dialog_Data : ScriptableObject
{
    [SerializeField] private string[] talk;

    public string[] Talk { get => talk; set => talk = value; }
}
