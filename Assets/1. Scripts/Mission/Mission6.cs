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
        // �巡��
        if (isDrag)
        {
            line.SetPosition(1, new Vector3((Input.mousePosition.x - clickpos.x) * 1920f / Screen.width, (Input.mousePosition.y - clickpos.y) * 1080f / Screen.height, -10));

            // �巡�� ��
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                // ������ ���� ��Ҵٸ�
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject rightLine = hit.transform.gameObject;

                    // ������ �� y��
                    rightY = rightLine.GetComponent<RectTransform>().anchoredPosition.y;

                    // ������ �� ����
                    rightC = rightLine.GetComponent<Image>().color;

                    line.SetPosition(1, new Vector3(500, rightY - leftY, -10));

                    // �� ��
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

                    // ���� ���� üũ
                    if (isColor[0]&& isColor[1] && isColor[2] && isColor[3])
                    {
                        Invoke("MissionSuccess", 0.2f);
                    }
                }
                // ���� �ʾҴٸ�
                else
                {
                    line.SetPosition(1, new Vector3(0, 0, -10));
                }
                
                isDrag = false;
            }
        }    
    }

    // �̼� ����
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindAnyObjectByType<PlayerCtrl>();

        // �ʱ�ȭ
        for (int i = 0; i < 4; i++)
        {
            isColor[i] = false;
            lines[i].SetPosition(1, new Vector3(0, 0, -10));
        }

        // ����
        for (int i = 0; i < rights.Length; i++)
        {
            Vector3 temp = rights[i].anchoredPosition;

            int rand = Random.Range(0, 4);
            rights[i].anchoredPosition = rights[rand].anchoredPosition;

            rights[rand].anchoredPosition = temp;
        }
    }

    // ���� ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    // �� ������ ȣ��
    public void ClickLine(LineRenderer click)
    {
        clickpos = Input.mousePosition;
        line = click;

        // ���� �� y��
        leftY = click.transform.parent.GetComponent<RectTransform>().anchoredPosition.y;    

        // ���� �� ����
        leftC = click.transform.parent.GetComponent<Image>().color;

        isDrag = true;
    }

    // �̼� �����ϸ� ȣ��
    public void MissionSuccess()
    {
        ClickCancle();
        missionCtrl_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
