using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ��ȭ �ؽ�Ʈ ��ũ���ͺ� ������Ʈ
[CreateAssetMenu(fileName = "Dialog_Data", menuName = "Dialog/Text")]
public class Dialog_Data : ScriptableObject
{
    [SerializeField] private string[] talk;

    public string[] Talk { get => talk; set => talk = value; }
}
