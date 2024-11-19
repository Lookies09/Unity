using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Demon_Male : ObjectHealth
{
    // 대화 출력 매니저
    [SerializeField] private TalkManager talkManager;

    // 악마 컨트롤러
    [SerializeField] private Demon_Male_Controller demon_Controller;

    // 대화창 캔버스
    [SerializeField] private GameObject chatBox;

    // 이벤트 한번 처리
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
            // 대화출력 메니저에게 대화 텍스트 넘기기
            talkManager.TextEffect("명을 재촉하지마, 난 너가 상대할 수 있는 상대가 아니야...");
            animator.SetInteger("State", demon_Controller.State++);
            num++;
        }
        
    }
}
