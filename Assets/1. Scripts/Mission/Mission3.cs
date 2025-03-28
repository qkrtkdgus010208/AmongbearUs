using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission3 : MonoBehaviour
{
    public Text inputTest, keyCode;
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

        // Ű���� �ʱ�ȭ
        inputTest.text = "";
        keyCode.text = "";
            
        // Ű�ڵ� ����
        for (int i = 0; i < 5; i++)
        {
            keyCode.text += Random.Range(0, 10); // 0 ~ 9
        }
    }

    // ���� ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    // ���� ��ư ������ ȣ��
    public void ClickNumber()
    {
        if (inputTest.text.Length <= 4)
            inputTest.text += EventSystem.current.currentSelectedGameObject.name;
    }

    // ���� ��ư ������ ȣ��
    public void ClickDelete()
    {
        if(inputTest.text != "")
        {
            inputTest.text = inputTest.text.Substring(0, inputTest.text.Length - 1);
        }
    }

    // üũ ��ư ������ ȣ��
    public void ClickCheck()
    {
        if(inputTest.text == keyCode.text)
        {
            MissionSuccess();
        }
    }

    // �̼� �����ϸ� ȣ��
    public void MissionSuccess()
    {
        ClickCancle();
        missionCtrl_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
