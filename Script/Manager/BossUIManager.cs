using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    // 보스 클리어 UI
    [SerializeField] private GameObject clearUI;

    // =======================================================
    // 보스 상태 UI 전체
    [SerializeField] private GameObject bossStateUI;

    // 보스 붉은 체력
    [SerializeField] private Image bossRed_HP;
    // 보스 노란색 체력 ( 붉은색 따라가는 체력)
    [SerializeField] private Image bossYellow_HP;
    // 보스 이름
    [SerializeField] private Text bossNameText;

    // 보스 참조
    private GameObject boss;

    // 체력을 1로 만드는 숫자
    private float hpTo1;

    // 시간
    private float time;

    //============================================================

    // 보스 2 상태 UI 전체
    [SerializeField] private GameObject bossStateUI_2;

    // 보스 2 붉은 체력
    [SerializeField] private Image bossRed_HP_2;
    // 보스 2 노란색 체력 ( 붉은색 따라가는 체력)
    [SerializeField] private Image bossYellow_HP_2;
    // 보스 2 이름
    [SerializeField] private Text bossNameText_2;

    // 보스 2 참조
    private GameObject boss_2;

    // 보스 2의 체력을 1로 만드는 숫자
    private float hpTo1_2;

    // 보스 2 시간
    private float time_2;


    private void Update()
    {

        // 보스1이 존재하고
        if (boss != null)
        {
            // 보스2는 없고 보스 1이 죽었을때
            if (boss.GetComponent<ObjectHealth>().IsDead && boss_2 == null)
            {
                // 클리어 껐다 켜기
                Invoke("ActiveClearUI", 3f);

                Invoke("InActiveClearUI", 13f);
            }
        }

        // 보스2가 존재하고
        if (boss_2 != null)
        {
            // 보스1이 없고 보스 2가 죽었을때
            if (boss_2.GetComponent<ObjectHealth>().IsDead && boss == null)
            {
                // 클리어 껐다 켜기
                Invoke("ActiveClearUI", 3f);

                Invoke("InActiveClearUI", 13f);
            }
        }

            

        // ====================================================================
        // 보스가 죽거나 없으면 UI 비활성화
        if (boss == null)
        {
            bossStateUI.SetActive(false);
        }
        if (boss_2 == null)
        {
            bossStateUI_2.SetActive(false);
        }


        // 보스 상태 UI가 켜져있을때 동작
        if (bossStateUI.activeSelf)
        {
            float hp = boss.GetComponent<ObjectHealth>().Health;
            bossRed_HP.fillAmount = hp * hpTo1;

            if (bossRed_HP.fillAmount < bossYellow_HP.fillAmount)
            {
                time += Time.deltaTime;

                if (time > 1f)
                {
                    bossYellow_HP.fillAmount -= 0.8f * Time.deltaTime;
                }
            }
            else
            {
                time = 0;
            }
        }

        // ==================================================================

        // 보스 2 상태 UI가 켜져있을때 동작
        if (bossStateUI_2.activeSelf)
        {
            float hp = boss_2.GetComponent<ObjectHealth>().Health;
            bossRed_HP_2.fillAmount = hp * hpTo1_2;

            if (bossRed_HP_2.fillAmount < bossYellow_HP_2.fillAmount)
            {
                time_2 += Time.deltaTime;

                if (time_2 > 1f)
                {
                    bossYellow_HP_2.fillAmount -= 0.8f * Time.deltaTime;
                }
            }
            else
            {
                time_2 = 0;
            }
        }
    }

    public void SetBossInfo(GameObject boss, string name)
    {
        
        this.boss = boss;

        // 보스 이름 입력
        bossNameText.text = name;

        float hp = boss.GetComponent<ObjectHealth>().Health;
        hpTo1 = 1 / hp;

        // 보스 UI 활성화
        bossStateUI.SetActive(true);
    }

    public void SetBossInfo(GameObject boss, string name, int num)
    {

        boss_2 = boss;

        // 보스 2 이름 입력
        bossNameText_2.text = name;

        float hp = boss.GetComponent<ObjectHealth>().Health;
        hpTo1_2 = 1 / hp;

        // 보스 2 UI 활성화
        bossStateUI_2.SetActive(true);
    }

    public void ActiveClearUI()
    {
        clearUI.SetActive(true);
    }

    public void InActiveClearUI()
    {
        clearUI.SetActive(false);
    }
}
