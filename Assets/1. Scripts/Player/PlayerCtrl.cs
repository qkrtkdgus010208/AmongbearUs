using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Mathematics;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject joyStick, mainView, playView;
    public Settings settings_script;
    public Button btn;
    public Sprite use, kill;
    public Text text_cool;

    Animator anim;
    GameObject coll;
    KillCtrl killCtrl_script;

    public float speed;

    public bool isCantMove, isMission;

    float timer;
    bool isCool, isAnim;


    public void Start()
    {
        anim = GetComponent<Animator>();

        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        // 미션이라면
        if (isMission)
        {
            btn.GetComponent<Image>().sprite = use;

            text_cool.text = "";
        }
        // 킬 퀘스트라면
        else
        {
            killCtrl_script = FindAnyObjectByType<KillCtrl>();
            btn.GetComponent<Image>().sprite = kill;

            timer = 3;
            isCool = true;
        }
    }
    private void Update()
    {
        // 쿨타임
        if (isCool)
        {
            timer -= Time.deltaTime;
            text_cool.text = Mathf.Ceil(timer).ToString();

            if (text_cool.text == "0")
            {
                text_cool.text = "";
                isCool = false;
            }
        }

        if (isCantMove)
        {
            joyStick.SetActive(false);
        }
        else
        {
            Move();
        }

        // 애니매이션이 끝났다면
        if (isAnim && killCtrl_script.kill_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            killCtrl_script.kill_anim.SetActive(false);
            killCtrl_script.Kill();
            isCantMove = false;
            isAnim = false;
        }
    }

    // 캐릭터 움직임 관리
    void Move()
    {
        if (settings_script.isJoyStick)
        {
            joyStick.SetActive(true);
        }
        else
        {
            joyStick.SetActive(false);

            // 클릭했는지 판단
            if (Input.GetMouseButton(0))
            {
#if UNITY_EDITOR
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    transform.position += dir * speed * Time.deltaTime;

                    anim.SetBool("isWalk", true);

                    // 왼쪽으로 이동
                    if (dir.x < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    // 오른쪽으로 이동
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
#else
if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    transform.position += dir * speed * Time.deltaTime;

                    anim.SetBool("isWalk", true);

                    // 왼쪽으로 이동
                    if (dir.x < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    // 오른쪽으로 이동
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
#endif
            }
            // 클릭하지 않는다면
            else
            {
                anim.SetBool("isWalk", false);
            }
        }
    }

    // 캐릭터 삭제
    public void DestroyPlayer()
    {
        Camera.main.transform.parent = null;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mission" && isMission)
        {
            coll = collision.gameObject;

            btn.interactable = true;
        }

        if (collision.tag == "NPC" && !isMission && !isCool)
        {
            coll = collision.gameObject;

            btn.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mission")
        {
            coll = null;

            btn.interactable = false;
        }

        if (collision.tag == "NPC" && !isMission)
        {
            coll = null;

            btn.interactable = false;
        }
    }

    // 버튼 누르면 호출
    public void ClickButton()
    {
        // 미션일 때
        if (isMission)
        {
            // MissionStart를 호출
            coll.SendMessage("MissionStart");
        }
        // 킬 퀘스트일 때
        else
        {
            Kill();
        }

        isCantMove = true;
        btn.interactable = false;
    }

    void Kill()
    {
        // 죽이는 애니매이션
        killCtrl_script.kill_anim.SetActive(true);
        isAnim = true;
        isCool = true;
        timer = 3;

        // 죽은 이미지 변경
        coll.SendMessage("Dead");

        // 죽은 NPC는 다시 죽일 수 없게
        coll.GetComponent<CircleCollider2D>().enabled = false;
    }

    // 미션 종료하면 호출
    public void MissionEnd()
    {
        isCantMove = false;
    }
}
