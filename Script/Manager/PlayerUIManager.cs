using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    // 플레이어 붉은 체력 이미지
    [SerializeField] private Image hp_Red_Img;
    // 플레이어 회색 체력 이미지
    [SerializeField] private Image hp_Gray_Img;
    // 플레이어 초록 스테미너 이미지
    [SerializeField] private Image stmi_Green_Img;
    // 플레이어 회색 스태미너 이미지
    [SerializeField] private Image stmi_Gray_Img;

    // 사망 UI
    [SerializeField] private GameObject DeadInfo_UI;


    // 플레이어 체력을 1로 만드는 숫자
    private float hpTo1;

    // 플레이어 스테미너를 1로 만드는 숫자
    private float staminaTo1;

    // 체력 시간
    private float hp_Time;

    // 플레이어 상태
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
        // 플레이어가 죽으면
        if (playerState.IsDead)
        {
            // 사망 UI 띄우고 리턴
            DeadInfo_UI.SetActive(true);
            return;
        }

        float hp = playerState.Health;
        float stamina = playerState.Stamina;
        hp_Red_Img.fillAmount = hp * hpTo1;
        stmi_Gray_Img.fillAmount = stamina * staminaTo1;

        // 붉은 체력이 더 작을 때 만 동작
        if (hp_Red_Img.fillAmount < hp_Gray_Img.fillAmount)
        {
            hp_Time += Time.deltaTime;

            // 1초 후에 회색 체력 감소
            if (hp_Time > 1f)
            {
                hp_Gray_Img.fillAmount -= 0.8f * Time.deltaTime;
            }
        }
        else
        {
            hp_Time = 0;
        }
        
        // 회색 스태미너가 더 작으면 동작
        if (stmi_Gray_Img.fillAmount < stmi_Green_Img.fillAmount)
        {
            stmi_Green_Img.fillAmount -= 0.5f * Time.deltaTime;
        }
        else // 회색 스태미너가 같거나 크면 값을 일치
        {
            stmi_Green_Img.fillAmount = stmi_Gray_Img.fillAmount;
        }

    }

}
