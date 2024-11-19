using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalTeleport : MonoBehaviour
{
    // �÷��̾� ���� ����
    private bool isContact;

    // UI ǥ�� ��ġ
    [SerializeField] private Transform uiPos;

    // ��ȣ�ۿ� UI �̹���
    [SerializeField] private Image ui_Img;

    // ��Ż ��ȣ�ۿ� UI
    [SerializeField] private GameObject portalInfoUI;

    // ĵ���� ��Ʈ Ʈ������
    [SerializeField] private RectTransform canvasRectTransform;

    // Update is called once per frame
    void Update()
    {
        if (isContact)
        {
            portalInfoUI.SetActive(true);

            Vector3 targetPosition = Camera.main.WorldToViewportPoint(uiPos.position);

            // ȭ�� ��ǥ�� Canvas �������� ��ȯ
            Vector2 canvasPosition = new Vector2(
                ((targetPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
                ((targetPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));

            ui_Img.rectTransform.anchoredPosition = canvasPosition;

            // F Ű�� ������
            if (Input.GetKeyDown(KeyCode.F))
            {
                // ���� �� �ε�
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
        // �ݶ��̴��� ���� ������Ʈ �ױװ� �÷��̾���
        if (collider.tag == "Player")
        {
            isContact = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // �ݶ��̴����� �������� ������Ʈ �ױװ� �÷��̾���
        if (collider.tag == "Player")
        {
            isContact = false;
        }
    }
}
