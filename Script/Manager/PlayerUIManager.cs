using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    // �÷��̾� ���� ü�� �̹���
    [SerializeField] private Image hp_Red_Img;
    // �÷��̾� ȸ�� ü�� �̹���
    [SerializeField] private Image hp_Gray_Img;
    // �÷��̾� �ʷ� ���׹̳� �̹���
    [SerializeField] private Image stmi_Green_Img;
    // �÷��̾� ȸ�� ���¹̳� �̹���
    [SerializeField] private Image stmi_Gray_Img;

    // ��� UI
    [SerializeField] private GameObject DeadInfo_UI;


    // �÷��̾� ü���� 1�� ����� ����
    private float hpTo1;

    // �÷��̾� ���׹̳ʸ� 1�� ����� ����
    private float staminaTo1;

    // ü�� �ð�
    private float hp_Time;

    // �÷��̾� ����
    private Player_Health playerState;


    private void Start()
    {
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
        float hp = playerState.Health;
        float stamina = playerState.Stamina;
        hpTo1 = 1 / hp;
        staminaTo1 = 1 / stamina;
    }

    private void Update()
    {
        // �÷��̾ ������
        if (playerState.IsDead)
        {
            // ��� UI ���� ����
            DeadInfo_UI.SetActive(true);
            return;
        }

        float hp = playerState.Health;
        float stamina = playerState.Stamina;
        hp_Red_Img.fillAmount = hp * hpTo1;
        stmi_Gray_Img.fillAmount = stamina * staminaTo1;

        // ���� ü���� �� ���� �� �� ����
        if (hp_Red_Img.fillAmount < hp_Gray_Img.fillAmount)
        {
            hp_Time += Time.deltaTime;

            // 1�� �Ŀ� ȸ�� ü�� ����
            if (hp_Time > 1f)
            {
                hp_Gray_Img.fillAmount -= 0.8f * Time.deltaTime;
            }
        }
        else
        {
            hp_Time = 0;
        }
        
        // ȸ�� ���¹̳ʰ� �� ������ ����
        if (stmi_Gray_Img.fillAmount < stmi_Green_Img.fillAmount)
        {
            stmi_Green_Img.fillAmount -= 0.5f * Time.deltaTime;
        }
        else // ȸ�� ���¹̳ʰ� ���ų� ũ�� ���� ��ġ
        {
            stmi_Green_Img.fillAmount = stmi_Gray_Img.fillAmount;
        }

    }

}
