using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission6 : MonoBehaviour
{
    public bool[] isColor = new bool[4];
    public RectTransform[] rights;
    public LineRenderer[] lines;
    MissionCtrl missionCtrl_script;

    Animator anim;
    PlayerCtrl playerCtrl_script;

    Vector2 clickpos;
    LineRenderer line;

    Color leftC, rightC;

    bool isDrag;
    float leftY, rightY;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionCtrl_script = FindAnyObjectByType<MissionCtrl>();
    }

    private void Update()
    {
        // 드래그
        if (isDrag)
        {
            line.SetPosition(1, new Vector3((Input.mousePosition.x - clickpos.x) * 1920f / Screen.width, (Input.mousePosition.y - clickpos.y) * 1080f / Screen.height, -10));

            // 드래그 끝
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                // 오른쪽 선에 닿았다면
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject rightLine = hit.transform.gameObject;

                    // 오른쪽 선 y값
                    rightY = rightLine.GetComponent<RectTransform>().anchoredPosition.y;

                    // 오른쪽 선 색상
                    rightC = rightLine.GetComponent<Image>().color;

                    line.SetPosition(1, new Vector3(500, rightY - leftY, -10));

                    // 색 비교
                    if (leftC == rightC)
                    {
                        switch (leftY)
                        {
                            case 225: isColor[0] = true; break;
                            case 75: isColor[1] = true; break;
                            case -75: isColor[2] = true; break;
                            case -225: isColor[3] = true; break;
                        }
                    }
                    else
                    {
                        switch (leftY)
                        {
                            case 225: isColor[0] = false; break;
                            case 75: isColor[1] = false; break;
                            case -75: isColor[2] = false; break;
                            case -225: isColor[3] = false; break;
                        }
                    }

                    // 성공 여부 체크
                    if (isColor[0]&& isColor[1] && isColor[2] && isColor[3])
                    {
                        Invoke("MissionSuccess", 0.2f);
                    }
                }
                // 닿지 않았다면
                else
                {
                    line.SetPosition(1, new Vector3(0, 0, -10));
                }
                
                isDrag = false;
            }
        }    
    }

    // 미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindAnyObjectByType<PlayerCtrl>();

        // 초기화
        for (int i = 0; i < 4; i++)
        {
            isColor[i] = false;
            lines[i].SetPosition(1, new Vector3(0, 0, -10));
        }

        // 랜덤
        for (int i = 0; i < rights.Length; i++)
        {
            Vector3 temp = rights[i].anchoredPosition;

            int rand = Random.Range(0, 4);
            rights[i].anchoredPosition = rights[rand].anchoredPosition;

            rights[rand].anchoredPosition = temp;
        }
    }

    // 엑스 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    // 선 누르면 호출
    public void ClickLine(LineRenderer click)
    {
        clickpos = Input.mousePosition;
        line = click;

        // 왼쪽 선 y값
        leftY = click.transform.parent.GetComponent<RectTransform>().anchoredPosition.y;    

        // 왼쪽 선 색상
        leftC = click.transform.parent.GetComponent<Image>().color;

        isDrag = true;
    }

    // 미션 성공하면 호출
    public void MissionSuccess()
    {
        ClickCancle();
        missionCtrl_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
