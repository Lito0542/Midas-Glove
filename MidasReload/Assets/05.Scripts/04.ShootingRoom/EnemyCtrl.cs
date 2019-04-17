using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour {

    public int hp = 20;
    public float speed = 3;
    int count = 0;

    public int[] partsHp = new int[6] { 5, 20, 15, 15, 15, 15 };
    public float[] burnTime = new float[6];
    public bool[] isBurn = new bool[6] { false, false, false, false, false, false };

    public GameObject[] bodyParts = new GameObject[6];//Head. body, LLeg, RLeg, LArm, RArm;
    public GameObject[] bodyFlame = new GameObject[6];//Head, body, LLeg, RLeg, LArm, RArm;

    bool isDead = false;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Burns(0, 0);
        }
        for(int i = 0; i < 5; i++)
        {
            if (isBurn[i])
            {
                burnTime[i] += Time.deltaTime;
                if(burnTime[i] > 1)
                {
                    burnTime[i] = 0;
                    Damage(1, i);
                    count++;
                    if(count > 2)
                    {
                        count = 0;
                        Burns(1, Random.Range(0,6));
                    }
                }
            }
        }
    }

    public void Damage(int dmg, int part)
    {
        hp -= dmg;
        partsHp[part] -= dmg;
        if (hp <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
        if (partsHp[part] <= 0)
        {
            Destroy(bodyParts[part]);
            if (part == 2 || part == 3)
            {
                speed *= 0.7f;
            }
            else if (part == 0)
            {
                isDead = true;
                Destroy(gameObject);
            }
        }
    }

    public void Burns(int dmg,int part)
    {
        isBurn[part] = true;
        bodyFlame[part].SetActive(true);
    }
}
