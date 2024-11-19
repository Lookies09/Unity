using UnityEngine;
using UnityEngine.UI;

public class Demon_Male_Controller : MonoBehaviour
{
    // �ڼ� �����
    [SerializeField] private AudioSource effectAudio;
    // �ڼ� �Ҹ�
    [SerializeField] private AudioClip[] clapSounds;

    // �ִϸ�����
    private Animator animator;

    // �ִϸ��̼� ����
    private int state;

    // �ð�
    private float time;

    // ��ȭ ��ȣ
    private int talkNum;

    // ��ȭ ��� �Ŵ���
    [SerializeField] private TalkManager talkManager;

    // ��ȭ �ؽ�Ʈ ��ũ���ͺ� ������Ʈ 
    [SerializeField] private Dialog_Data dialog;

    [SerializeField] private Collider sphereCollider;

    // ��ȭ ���ۿ���
    private bool onTalk = false;

    // ��ȭâ ĵ����
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

        // �� �̻� ��ȭ�� ������ ��� ����
        if (talkNum > dialog.Talk.Length) { return; }
                  

        // ZŰ�� ������ ���� ��ȭ��
        if (Input.GetKeyDown(KeyCode.Z) && onTalk && talkManager.CanSkip)
        {
            StopCoroutine("ShowTextWithDelay");
            // ��ȭ��� �޴������� ��ȭ �ؽ�Ʈ �ѱ��
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
