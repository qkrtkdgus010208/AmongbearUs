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

        // �̼��̶��
        if (isMission)
        {
            btn.GetComponent<Image>().sprite = use;

            text_cool.text = "";
        }
        // ų ����Ʈ���
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
        // ��Ÿ��
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

        // �ִϸ��̼��� �����ٸ�
        if (isAnim && killCtrl_script.kill_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            killCtrl_script.kill_anim.SetActive(false);
            killCtrl_script.Kill();
            isCantMove = false;
            isAnim = false;
        }
    }

    // ĳ���� ������ ����
    void Move()
    {
        if (settings_script.isJoyStick)
        {
            joyStick.SetActive(true);
        }
        else
        {
            joyStick.SetActive(false);

            // Ŭ���ߴ��� �Ǵ�
            if (Input.GetMouseButton(0))
            {
#if UNITY_EDITOR
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    transform.position += dir * speed * Time.deltaTime;

                    anim.SetBool("isWalk", true);

                    // �������� �̵�
                    if (dir.x < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    // ���������� �̵�
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

                    // �������� �̵�
                    if (dir.x < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    // ���������� �̵�
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
#endif
            }
            // Ŭ������ �ʴ´ٸ�
            else
            {
                anim.SetBool("isWalk", false);
            }
        }
    }

    // ĳ���� ����
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

    // ��ư ������ ȣ��
    public void ClickButton()
    {
        // �̼��� ��
        if (isMission)
        {
            // MissionStart�� ȣ��
            coll.SendMessage("MissionStart");
        }
        // ų ����Ʈ�� ��
        else
        {
            Kill();
        }

        isCantMove = true;
        btn.interactable = false;
    }

    void Kill()
    {
        // ���̴� �ִϸ��̼�
        killCtrl_script.kill_anim.SetActive(true);
        isAnim = true;
        isCool = true;
        timer = 3;

        // ���� �̹��� ����
        coll.SendMessage("Dead");

        // ���� NPC�� �ٽ� ���� �� ����
        coll.GetComponent<CircleCollider2D>().enabled = false;
    }

    // �̼� �����ϸ� ȣ��
    public void MissionEnd()
    {
        isCantMove = false;
    }
}
