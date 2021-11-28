using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int ChampSize = 3;

    public GameObject[] MyChampion = new GameObject[ChampSize]; //장착된 카드
    public int EqupChampionCount; //장착된 카드 수

    public int[] ChampionHP = new int[ChampSize]; //장착된 카드 체력
    public int PlayerHP; // 플레이어 체력


    void Start()
    {
        PlayerHP = 100;
        EqupChampionCount = 0;
    }
    void Update()
    {
    }
}
