using UnityEngine;

public class NPCCtrl : MonoBehaviour
{
    public Sprite[] idles, deads;

    SpriteRenderer sr;

    int rand;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        rand = Random.Range(0, 5);
        sr.sprite = idles[rand];
    }

    // ���� �̹���
    public void Dead()
    {
        sr.sprite = deads[rand];

        sr.sortingOrder = -1;
    }
}

    

