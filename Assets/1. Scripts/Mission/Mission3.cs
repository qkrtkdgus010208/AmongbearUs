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

    // 미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindAnyObjectByType<PlayerCtrl>();

        // 키보드 초기화
        inputTest.text = "";
        keyCode.text = "";
            
        // 키코드 랜덤
        for (int i = 0; i < 5; i++)
        {
            keyCode.text += Random.Range(0, 10); // 0 ~ 9
        }
    }

    // 엑스 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    // 숫자 버튼 누르면 호출
    public void ClickNumber()
    {
        if (inputTest.text.Length <= 4)
            inputTest.text += EventSystem.current.currentSelectedGameObject.name;
    }

    // 삭제 버튼 누르면 호출
    public void ClickDelete()
    {
        if(inputTest.text != "")
        {
            inputTest.text = inputTest.text.Substring(0, inputTest.text.Length - 1);
        }
    }

    // 체크 버튼 누르면 호출
    public void ClickCheck()
    {
        if(inputTest.text == keyCode.text)
        {
            MissionSuccess();
        }
    }

    // 미션 성공하면 호출
    public void MissionSuccess()
    {
        ClickCancle();
        missionCtrl_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
