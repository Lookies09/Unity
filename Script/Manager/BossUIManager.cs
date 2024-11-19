using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    // ���� Ŭ���� UI
    [SerializeField] private GameObject clearUI;

    // =======================================================
    // ���� ���� UI ��ü
    [SerializeField] private GameObject bossStateUI;

    // ���� ���� ü��
    [SerializeField] private Image bossRed_HP;
    // ���� ����� ü�� ( ������ ���󰡴� ü��)
    [SerializeField] private Image bossYellow_HP;
    // ���� �̸�
    [SerializeField] private Text bossNameText;

    // ���� ����
    private GameObject boss;

    // ü���� 1�� ����� ����
    private float hpTo1;

    // �ð�
    private float time;

    //============================================================

    // ���� 2 ���� UI ��ü
    [SerializeField] private GameObject bossStateUI_2;

    // ���� 2 ���� ü��
    [SerializeField] private Image bossRed_HP_2;
    // ���� 2 ����� ü�� ( ������ ���󰡴� ü��)
    [SerializeField] private Image bossYellow_HP_2;
    // ���� 2 �̸�
    [SerializeField] private Text bossNameText_2;

    // ���� 2 ����
    private GameObject boss_2;

    // ���� 2�� ü���� 1�� ����� ����
    private float hpTo1_2;

    // ���� 2 �ð�
    private float time_2;


    private void Update()
    {

        // ����1�� �����ϰ�
        if (boss != null)
        {
            // ����2�� ���� ���� 1�� �׾�����
            if (boss.GetComponent<ObjectHealth>().IsDead && boss_2 == null)
            {
                // Ŭ���� ���� �ѱ�
                Invoke("ActiveClearUI", 3f);

                Invoke("InActiveClearUI", 13f);
            }
        }

        // ����2�� �����ϰ�
        if (boss_2 != null)
        {
            // ����1�� ���� ���� 2�� �׾�����
            if (boss_2.GetComponent<ObjectHealth>().IsDead && boss == null)
            {
                // Ŭ���� ���� �ѱ�
                Invoke("ActiveClearUI", 3f);

                Invoke("InActiveClearUI", 13f);
            }
        }

            

        // ====================================================================
        // ������ �װų� ������ UI ��Ȱ��ȭ
        if (boss == null)
        {
            bossStateUI.SetActive(false);
        }
        if (boss_2 == null)
        {
            bossStateUI_2.SetActive(false);
        }


        // ���� ���� UI�� ���������� ����
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

        // ���� 2 ���� UI�� ���������� ����
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

        // ���� �̸� �Է�
        bossNameText.text = name;

        float hp = boss.GetComponent<ObjectHealth>().Health;
        hpTo1 = 1 / hp;

        // ���� UI Ȱ��ȭ
        bossStateUI.SetActive(true);
    }

    public void SetBossInfo(GameObject boss, string name, int num)
    {

        boss_2 = boss;

        // ���� 2 �̸� �Է�
        bossNameText_2.text = name;

        float hp = boss.GetComponent<ObjectHealth>().Health;
        hpTo1_2 = 1 / hp;

        // ���� 2 UI Ȱ��ȭ
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
