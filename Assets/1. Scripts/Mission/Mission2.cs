using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission2 : MonoBehaviour
{
    public Transform trash, handle;
    public GameObject bottom;
    public Animator anim_shake;

    Animator anim;
    PlayerCtrl playerCtrl_script;
    RectTransform rect_handle;
    MissionCtrl missionCtrl_script;

    bool isDrag, isPlay;
    Vector2 originPos;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rect_handle = handle.GetComponent<RectTransform>();
        originPos = rect_handle.anchoredPosition;
        missionCtrl_script = FindAnyObjectByType<MissionCtrl>();
    }

    private void Update()
    {
        if(isPlay)
        {
            // 드래그
            if (isDrag)
            {
                handle.position = Input.mousePosition;
                rect_handle.anchoredPosition = new Vector2(originPos.x, Mathf.Clamp(rect_handle.anchoredPosition.y, -135, -47));

                anim_shake.enabled = true;

                // 드래그 끝
                if (Input.GetMouseButtonUp(0))
                {
                    rect_handle.anchoredPosition = originPos;
                    isDrag = false;
                    anim_shake.enabled = false;
                }
            }

            // 쓰레기 배출
            if (rect_handle.anchoredPosition.y <= -130)
            {
                bottom.SetActive(false);
            }
            else
            {
                bottom.SetActive(true);
            }

            // 쓰레기 삭제
            for (int i = 0; i < trash.childCount; i++)
            {
                if (trash.GetChild(i).GetComponent<RectTransform>().anchoredPosition.y <= -600)
                {
                    Destroy(trash.GetChild(i).gameObject);
                }
            }

            // 성공 여부 체크
            if (trash.childCount == 0)
            {
                Invoke("MissionSuccess", 0.2f);
                isPlay = false;

                rect_handle.anchoredPosition = originPos;
                isDrag = false;
                anim_shake.enabled = false;
            }
        }
    }

    // 미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindAnyObjectByType<PlayerCtrl>();

        // 초기화
        for (int i = 0; i < trash.childCount; i++)
        {
            Destroy(trash.GetChild(i).gameObject);
        }

        // 쓰레기 스폰
        for (int i = 0; i < 5; i++)
        {
            // 사과
            GameObject trash4 = Instantiate(Resources.Load("Trash/Trash4"), trash) as GameObject;
            trash4.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash4.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));

            // 캔
            GameObject trash5 = Instantiate(Resources.Load("Trash/Trash5"), trash) as GameObject;
            trash5.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash5.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
        }

        // 쓰레기 스폰
        for (int i = 0; i < 5; i++)
        {
            // 병
            GameObject trash1 = Instantiate(Resources.Load("Trash/Trash1"), trash) as GameObject;
            trash1.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash1.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));

            // 생선
            GameObject trash2 = Instantiate(Resources.Load("Trash/Trash2"), trash) as GameObject;
            trash2.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash2.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));

            // 비닐
            GameObject trash3 = Instantiate(Resources.Load("Trash/Trash3"), trash) as GameObject;
            trash3.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
            trash3.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
        }

        isPlay = true;
    }

    // 엑스 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    // 손잡이 누르면 호출
    public void ClickHandle()
    {
        isDrag = true;
    }

    // 미션 성공하면 호출
    public void MissionSuccess()
    {
        ClickCancle();
        missionCtrl_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
