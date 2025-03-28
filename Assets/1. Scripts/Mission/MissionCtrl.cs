using UnityEngine;
using UnityEngine.UI;

public class MissionCtrl : MonoBehaviour
{
    public Slider guage;
    public CircleCollider2D[] colls;
    public GameObject text_anim, mainView;

    int missionCount;

    // �̼� �ʱ�ȭ
    public void MissionReset()
    {
        guage.value = 0;
        missionCount = 0;

        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].enabled = true;
        }

        text_anim.SetActive(false);
    }

    // �̼� �����ϸ� ȣ��
    public void MissionSuccess(CircleCollider2D coll)
    {
        missionCount++;

        guage.value = missionCount / 7f;

        // ������ �̼��� �ٽ� �÷��� X
        coll.enabled = false;

        // ���� ���� üũ
        if(guage.value == 1) 
        {
            text_anim.SetActive(true);

            Invoke("Change", 1.5f);
        }
    }

    // ȭ�� ��ȯ
    public void Change()
    {
        mainView.SetActive(true);
        gameObject.SetActive(false);

        // ĳ���� ���� 
        FindAnyObjectByType<PlayerCtrl>().DestroyPlayer();
    }
}
