using UnityEngine;

// 1. 스틱 드래그 + 제한
// 2. 드래그한만큼 캐릭터 이동

public class JoyStick : MonoBehaviour
{
    public RectTransform stick, backGround;

    PlayerCtrl playerCtrl_script;
    Animator anim;

    bool isDrag;
    float limit;

    private void Start()
    {
        playerCtrl_script = GetComponent<PlayerCtrl>();
        anim = GetComponent<Animator>();

        limit = backGround.rect.width * 0.5f;
    }

    private void Update()
    {
        // 드래그하는 동안
        if (isDrag)
        {
            Vector2 vec = Input.mousePosition - backGround.position;
            stick.anchoredPosition =  Vector2.ClampMagnitude(vec, limit);

            Vector3 dir = (stick.position - backGround.position).normalized;
            transform.position += dir * playerCtrl_script.speed * Time.deltaTime;

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

            // 드래그 끝나면 
            if (Input.GetMouseButtonUp(0))
            {
                stick.localPosition = new Vector3 (0, 0, 0);
                anim.SetBool("isWalk", false);

                isDrag = false;
            }
        }
    }
    // 스틱을 누르면 호출
    public void ClickStick()
    {
        isDrag = true;
    }
    
}
