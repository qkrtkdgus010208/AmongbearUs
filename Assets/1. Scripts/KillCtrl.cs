using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class KillCtrl : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject kill_anim, text_anim, mainView;

    List<int> number = new List<int>();

    int count;

    // �ʱ�ȭ
    public void KillReset()
    {
        kill_anim.SetActive(false);
        text_anim.SetActive(false);

        number.Clear();

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            if (spawnPoint[i].childCount != 0)
            {
                Destroy(spawnPoint[i].GetChild(0).gameObject);
            }
        }

        NPCSpawn();
    }

    // NPC ����
    public void NPCSpawn()
    {
        int rand = Random.Range(0, spawnPoint.Length);
      
        for (int i = 0; i < spawnPoint.Length / 2;)
        {
            // �ߺ��Ǿ��ٸ�
            if(number.Contains(rand))
            {
                rand = Random.Range(0, spawnPoint.Length);
            }
            // �ߺ����� �ʾҴٸ�
            else
            {
                number.Add(rand);
                i++;
            }
        }

        // ����
        for (int i = 0; i < number.Count; i++)
        {
            Instantiate(Resources.Load("NPC"), spawnPoint[number[i]]);
        }
    }

    // ų�ϸ� ȣ��
    public void Kill()
    {
        count++;

        if(count == 5)
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
