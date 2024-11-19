using UnityEngine;
using UnityEngine.UI;

public class Demon_Male_Controller : MonoBehaviour
{
    // 박수 오디오
    [SerializeField] private AudioSource effectAudio;
    // 박수 소리
    [SerializeField] private AudioClip[] clapSounds;

    // 애니메이터
    private Animator animator;

    // 애니메이션 상태
    private int state;

    // 시간
    private float time;

    // 대화 번호
    private int talkNum;

    // 대화 출력 매니저
    [SerializeField] private TalkManager talkManager;

    // 대화 텍스트 스크립터블 오브젝트 
    [SerializeField] private Dialog_Data dialog;

    [SerializeField] private Collider sphereCollider;

    // 대화 시작여부
    private bool onTalk = false;

    // 대화창 캔버스
    [SerializeField] private GameObject chatBox;

    [SerializeField] private GameObject fixedCam;

    public int State { get => state; set => state = value; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (State == 1 || State == 4)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        // 더 이상 대화가 없으면 출력 리턴
        if (talkNum > dialog.Talk.Length) { return; }
                  

        // Z키를 누르면 다음 대화로
        if (Input.GetKeyDown(KeyCode.Z) && onTalk && talkManager.CanSkip)
        {
            StopCoroutine("ShowTextWithDelay");
            // 대화출력 메니저에게 대화 텍스트 넘기기
            talkManager.TextEffect(dialog.Talk[talkNum]);
            talkNum++;
            State++;
        }

        animator.SetInteger("State", State);
        animator.SetFloat("ClapTime", time);
        animator.SetFloat("SittingTime", time);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && !onTalk)
        {
            fixedCam.SetActive(true);
            onTalk = true;
            chatBox.SetActive(true);

            talkManager.TextEffect(dialog.Talk[talkNum++]);            
            State++;
        }
    }

    public void SitEvent()
    {
        chatBox.SetActive(false);
        sphereCollider.enabled = false;
        fixedCam.SetActive(false);
    }

    public void WarningEvent()
    {
        State = 0;
        chatBox.SetActive(false);
    }

    public void ClapSound()
    {
        if (effectAudio.isPlaying)
        {
            return;
        }
        int num = Random.Range(0, clapSounds.Length);
        effectAudio.clip = clapSounds[num];
        effectAudio.Play();
    }
}
