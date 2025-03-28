using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class KillCtrl : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject kill_anim, text_anim, mainView;

    List<int> number = new List<int>();

    int count;

    // 초기화
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

    // NPC 스폰
    public void NPCSpawn()
    {
        int rand = Random.Range(0, spawnPoint.Length);
      
        for (int i = 0; i < spawnPoint.Length / 2;)
        {
            // 중복되었다면
            if(number.Contains(rand))
            {
                rand = Random.Range(0, spawnPoint.Length);
            }
            // 중복되지 않았다면
            else
            {
                number.Add(rand);
                i++;
            }
        }

        // 스폰
        for (int i = 0; i < number.Count; i++)
        {
            Instantiate(Resources.Load("NPC"), spawnPoint[number[i]]);
        }
    }

    // 킬하면 호출
    public void Kill()
    {
        count++;

        if(count == 5)
        {
            text_anim.SetActive(true);
            Invoke("Change", 1.5f);
        }
    }

    // 화면 전환
    public void Change()
    {
        mainView.SetActive(true);
        gameObject.SetActive(false);

        // 캐릭터 삭제 
        FindAnyObjectByType<PlayerCtrl>().DestroyPlayer();
    }
}
