using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission1 : MonoBehaviour
{
    public Color red;
    public Image[] images;

    Animator anim;
    PlayerCtrl playerCtrl_script;
    MissionCtrl missionCtrl_script;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionCtrl_script = FindAnyObjectByType<MissionCtrl>();
    }

    // �̼� ����
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindAnyObjectByType<PlayerCtrl>();

        // �ʱ�ȭ
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }
        // ����
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, 7); // 0 ~ 6

            images[rand].color = red;
        }
    }

    // ���� ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    // ������ ��ư ������ ȣ��
    public void ClickButton()
    {
        Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();

        // �Ͼ��
        if(img.color == Color.white)
        {
            // ����������
            img.color = red;
        }
        // ������
        else
        {
            // �Ͼ��
            img.color = Color.white;
        }

        // ���� ���� üũ
        int count = 0;
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].color == Color.white)
            {
                count++;
            }
        }

        if(count == images.Length)
        {
            // ����
            Invoke("MissionSuccess", 0.2f);
        }
    }

    // �̼� �����ϸ� ȣ��
    public void MissionSuccess()
    {
        ClickCancle();
        missionCtrl_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
