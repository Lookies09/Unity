using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    [SerializeField] private Text displayText; // UI Text ������Ʈ ����

    [SerializeField] private float letterDelay = 0.2f; // �� ���ڰ� ��µ� ������ �ð�

    private float time;

    // ��ŵ ���� ����
    private bool canSkip = false;

    private string currentText = ""; // ������� ��µ� �ؽ�Ʈ

    public bool CanSkip { get => canSkip; set => canSkip = value; }

    private void Start()
    {     
        
    }

    private void Update()
    {
        
    }

    IEnumerator ShowTextWithDelay(string fullText)
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            displayText.text = currentText;
            yield return new WaitForSeconds(letterDelay);

            if (i == fullText.Length)
            {
                canSkip = true;
                StopCoroutine("ShowTextWithDelay");
            }
        }
    }

    public void TextEffect(string fullText)
    {
        canSkip = false;
        StartCoroutine("ShowTextWithDelay", fullText);
    }

}
