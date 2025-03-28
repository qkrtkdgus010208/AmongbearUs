using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject missionView, killView;

    // ���� ���� ��ư ������ ȣ��
    public void ClickQuit()
    {
        // ����Ƽ ������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // �ȵ���̵�
#else
Application.Quit();
#endif
    }

    // �̼� ��ư ������ ȣ��
    public void ClickMission()
    {
        gameObject.SetActive(false);
        missionView.SetActive(true);

        GameObject player =  Instantiate(Resources.Load("Character"), new Vector3(0, -5, 0), Quaternion.identity) as GameObject;
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().playView = missionView;
        player.GetComponent<PlayerCtrl>().isMission = true;

        missionView.SendMessage("MissionReset");
    }

    // ų ��ư ������ ȣ��
    public void ClickKill()
    {
        gameObject.SetActive(false);
        killView.SetActive(true);

        GameObject player = Instantiate(Resources.Load("Character"), new Vector3(0, -5, 0), Quaternion.identity) as GameObject;
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().playView = killView;
        player.GetComponent<PlayerCtrl>().isMission = false;

        killView.SendMessage("KillReset");
    }
}