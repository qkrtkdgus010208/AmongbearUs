using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject missionView, killView;

    // 게임 종료 버튼 누르면 호출
    public void ClickQuit()
    {
        // 유니티 에디터
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // 안드로이드
#else
Application.Quit();
#endif
    }

    // 미션 버튼 누르면 호출
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

    // 킬 버튼 누르면 호출
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