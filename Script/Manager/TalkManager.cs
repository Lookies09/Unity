using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    [SerializeField] private Text displayText; // UI Text 오브젝트 연결

    [SerializeField] private float letterDelay = 0.2f; // 각 글자가 출력될 딜레이 시간

    private float time;

    // 스킵 가능 여부
    private bool canSkip = false;

    private string currentText = ""; // 현재까지 출력된 텍스트

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
