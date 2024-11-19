using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalTeleport : MonoBehaviour
{
    // 플레이어 접촉 여부
    private bool isContact;

    // UI 표시 위치
    [SerializeField] private Transform uiPos;

    // 상호작용 UI 이미지
    [SerializeField] private Image ui_Img;

    // 포탈 상호작용 UI
    [SerializeField] private GameObject portalInfoUI;

    // 캔버스 렉트 트렌스폼
    [SerializeField] private RectTransform canvasRectTransform;

    // Update is called once per frame
    void Update()
    {
        if (isContact)
        {
            portalInfoUI.SetActive(true);

            Vector3 targetPosition = Camera.main.WorldToViewportPoint(uiPos.position);

            // 화면 좌표를 Canvas 영역으로 변환
            Vector2 canvasPosition = new Vector2(
                ((targetPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
                ((targetPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));

            ui_Img.rectTransform.anchoredPosition = canvasPosition;

            // F 키를 누르면
            if (Input.GetKeyDown(KeyCode.F))
            {
                // 다음 씬 로드
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            portalInfoUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // 콜라이더에 들어온 오브젝트 테그가 플레이어라면
        if (collider.tag == "Player")
        {
            isContact = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // 콜라이더에서 빠져나간 오브젝트 테그가 플레이어라면
        if (collider.tag == "Player")
        {
            isContact = false;
        }
    }
}
