using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int ChampSize = 3;

    public GameObject[] MyChampion = new GameObject[ChampSize]; //장착된 카드
    public int[] ChampionHP = new int[ChampSize];


    void Start()
    {
        

    }
    void Update()
    {
    }
}
