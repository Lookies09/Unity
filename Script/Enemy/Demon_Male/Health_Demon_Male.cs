using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Demon_Male : ObjectHealth
{
    // ��ȭ ��� �Ŵ���
    [SerializeField] private TalkManager talkManager;

    // �Ǹ� ��Ʈ�ѷ�
    [SerializeField] private Demon_Male_Controller demon_Controller;

    // ��ȭâ ĵ����
    [SerializeField] private GameObject chatBox;

    // �̺�Ʈ �ѹ� ó��
    private int num = 0;

    public override void Death()
    {
        
    }

    public override void Hit(int DMG)
    {
        health -= DMG;

        if (health < 50 && num == 0)
        {
            chatBox.SetActive(true);
            StopCoroutine("ShowTextWithDelay");
            // ��ȭ��� �޴������� ��ȭ �ؽ�Ʈ �ѱ��
            talkManager.TextEffect("���� ����������, �� �ʰ� ����� �� �ִ� ��밡 �ƴϾ�...");
            animator.SetInteger("State", demon_Controller.State++);
            num++;
        }
        
    }
}
