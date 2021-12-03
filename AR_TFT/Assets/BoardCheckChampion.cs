using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BoardCheckChampion : MonoBehaviour
{
    // 보드에 붙어서 버츄얼 버튼을 클릭(챔피언이 장착되었는지?)을 담당
    public bool[] ChampionSlot = { false, false, false };
    private static readonly int boardSize = 6;
    public GameObject[] BoardBtn = new GameObject[boardSize];

    // 버튼이 눌림 확인
    public bool[] TempBoardBtnPressed = new bool[boardSize];

    // 버튼이 눌린 시간 확인
    public float[] tempBtnTimer = new float[boardSize];

    // 일정 시간 이상 눌리면 버튼이 제대로 눌린게 맞음
    public bool[] BoardBtnPressed = new bool[boardSize];

    // 한번만 실행하는 챔피언 슬롯 함수
    public bool[] TempChampionSlot = new bool[boardSize / 2];


    void Start()
    {
        for (int i = 0; i < boardSize; i++) {
            BoardBtn[i].GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
            BoardBtn[i].GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        }
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
                // 구문 한번만 실행
                if (!TempChampionSlot[i])
                {
                    // 챔피언 장착
                    StartCoroutine(WaitForIt(i));
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
            if (vb.name == transform.name+"Check" + (i + 1).ToString())
            {
                TempBoardBtnPressed[i] = true;
            }
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        for (int i = 0; i < boardSize; i++)
        {
            if (vb.name == transform.name+"Check" + (i + 1).ToString())
            {
                TempBoardBtnPressed[i] = false;
                tempBtnTimer[i] = 0;
            }
        }
    }
    IEnumerator WaitForIt(int ChampionSlotNum) // 1초 뒤 탐색
    {
        yield return new WaitForSeconds(1.0f);
        CardAttach(ChampionSlotNum);
    }
    public void CardAttach(int ChampionSlotNum)
    {
        Debug.Log("AA");
        if (!ChampionSlot[ChampionSlotNum])  // 아이템이 안 차있으면
        {
            // 모든 챔피언 카드의 타이머를 확인

            float minTime = 100.0f;
            int minIndex = 100;

            // 가장 최근에 인식된 카드 탐색
            for (int i = 0; i < GameGE.ChampionCards.Length; i++)
            {
                // 최소 시간, 카드 탐색
                if (minTime > GameGE.ChampionAttachTimer[i])
                {
                    float tempTime = GameGE.ChampionAttachTimer[i];
                    int tempIndex = i;
                    if (!GameGE.ChampionCards[tempIndex].transform.parent && GameGE.ChampionCards[tempIndex].GetComponent<ChampionCard>().isattach)
                    {
                        minTime = tempTime;
                        minIndex = i;
                    }
                }
            }
            if (minIndex < 100)
            {
                GameGE.ChampionCards[minIndex].transform.parent = this.transform;
                ChampionSlot[ChampionSlotNum] = true;
                this.GetComponent<Board>().MyChampion[ChampionSlotNum] = GameGE.ChampionCards[minIndex];
                this.GetComponent<Board>().EqupChampionCount++;
                Debug.Log((minIndex + 1).ToString() + "카드가 슬롯" + (ChampionSlotNum + 1).ToString() + "칸에 장착");
            }
            else // 버튼은 클릭됐지만 위에 등록된 카드가 없음
            { 
                Debug.Log("등록된 카드가 올라가 있지 않습니다. 확인하세요.");
                StartCoroutine(WaitForIt(ChampionSlotNum));
            }
        }
    }
}
