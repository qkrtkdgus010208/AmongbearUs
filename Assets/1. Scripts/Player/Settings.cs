using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool isJoyStick;
    public Image touchBtn, joyStickBtn;
    public Color blue;
    public PlayerCtrl playerCtrl_script;

    GameObject mainView, playView;

    private void Start()
    {
        mainView = playerCtrl_script.mainView;
        playView = playerCtrl_script.playView;    
    }

    // ���� ��ư�� ������ ȣ��
    public void Clicksetting()
    {
        gameObject.SetActive(true);
        playerCtrl_script.isCantMove = true;
    }

    // �������� ���ư��� ��ư�� ������ ȣ��
    public void ClickBack()
    {
        gameObject.SetActive(false);
        playerCtrl_script.isCantMove = false;
    }

    // ��ġ ��ư�� ������ ȣ��
    public void ClickTouch()
    {
        isJoyStick = false;
        touchBtn.color = blue;
        joyStickBtn.color = Color.white;
    }

    // ���̽�ƽ ��ư�� ������ ȣ��
    public void ClickJoystick()
    {
        isJoyStick = true;
        touchBtn.color = Color.white;
        joyStickBtn.color = blue;
    }

    // ���� ������ ��ư�� ������ ȣ��
    public void ClickQuit()
    {
        mainView.SetActive(true);
        playView.SetActive(false);

        // ĳ���� ���� 
        playerCtrl_script.DestroyPlayer();
    }
}
