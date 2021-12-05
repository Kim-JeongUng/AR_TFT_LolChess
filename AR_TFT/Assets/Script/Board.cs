using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int ChampSize = 3;

    public GameObject[] MyChampion = new GameObject[ChampSize]; //장착된 카드
    public int EqupChampionCount; //장착된 카드 수
    public int PlayerHP; // 플레이어 체력

    public GameObject Enemy;

    void Start()
    {
        PlayerHP = 100;
        EqupChampionCount = 0;
    }
    void Update()
    {
    }
}
