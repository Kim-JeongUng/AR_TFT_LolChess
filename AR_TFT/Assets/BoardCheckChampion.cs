using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BoardCheckChampion : MonoBehaviour
{
    // 보드에 붙어서 버츄얼 버튼을 클릭(챔피언이 장착되었는지?)을 담당
    
    public GameObject GameManager;
    // 일단 블루팀만 해당하게 개발(11.10) 김정웅
    private static readonly int boardSize = 6;
    public GameObject[] BoardBtn = new GameObject[boardSize];

    // 버튼이 눌림 확인
    public bool[] TempBoardBtnPressed = new bool[boardSize];

    // 버튼이 눌린 시간 확인
    private float[] tempBtnTimer = new float[boardSize];

    // 일정 시간 이상 눌리면 버튼이 제대로 눌린게 맞음
    public bool[] BoardBtnPressed = new bool[boardSize];

    // 챔피언 슬롯은 3개
    public bool[] ChampionSlot = new bool[boardSize / 2];

    // 한번만 실행하는 챔피언 슬롯 함수
    public bool[] TempChampionSlot = new bool[boardSize / 2];


    void Start()
    {
        for (int i = 0; i < boardSize; i++) {
            BoardBtn[i] = GameObject.Find("BoardCheck" + (i + 1).ToString());
            BoardBtn[i].GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
            BoardBtn[i].GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        }
        GameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        for (int i = 0; i < boardSize; i++)
        {
            if (TempBoardBtnPressed[i] && !BoardBtnPressed[i])
            {
                tempBtnTimer[i] += Time.deltaTime;
                if (tempBtnTimer[i] > 1.0f) // 1초 이상 눌리면 
                {
                    BoardBtnPressed[i] = true;
                }
            }
        }
        for (int i = 0; i < boardSize/2; i++)
        {
            // 챔피언 슬롯에 해당하는 버튼 2개가 동시에 눌리면
            if (BoardBtnPressed[i * 2] && BoardBtnPressed[i * 2 + 1])
            {
                // 챔피언 슬롯이 차 있음
                ChampionSlot[i] = true;
                // 구문 한번만 실행
                if (!TempChampionSlot[i])
                {
                    this.GetComponent<CardAttachGE>().CardAttach(i,this.transform); 
                    TempChampionSlot[i] = true;
                }
            }
            else
                TempChampionSlot[i] = false;
        }
    }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        for (int i = 0; i < boardSize; i++) {
            if (vb.name == "BoardCheck" + (i + 1).ToString())
            {
                TempBoardBtnPressed[i] = true;
            }
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        for (int i = 0; i < boardSize; i++)
        {
            if (vb.name == "BoardCheck" + (i + 1).ToString())
            {
                BoardBtnPressed[i] = false;
                TempBoardBtnPressed[i] = false;
                tempBtnTimer[i] = 0;
            }
        }
    }
}
