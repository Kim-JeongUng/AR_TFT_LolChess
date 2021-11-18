using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CardGE : MonoBehaviour
{
    // 각 챔피언 카드에 붙어서 버츄얼 버튼을 클릭(아이템이 장착되었는지?)을 담당

    public GameObject GameManager;
    
    // 아이템 카드 두칸
    private static readonly int ItemSize = 4;
    public GameObject[] ItemBtn = new GameObject[ItemSize];

    // 버튼이 눌림 확인
    public bool[] TempItemBtnPressed = new bool[ItemSize];

    // 버튼이 눌린 시간 확인
    private float[] tempBtnTimer = new float[ItemSize];

    // 일정 시간 이상 눌리면 버튼이 제대로 눌린게 맞음
    public bool[] ItemBtnPressed = new bool[ItemSize];

    // 챔피언 슬롯은 3개
    public bool[] ChampionSlot = new bool[ItemSize / 2];

    // 한번만 실행하는 챔피언 슬롯 함수
    public bool[] TempChampionSlot = new bool[ItemSize / 2];


    void Start()
    {
        for (int i = 0; i < ItemSize; i++)
        {
            ItemBtn[i] = transform.Find("ItemCheck" + (i + 1).ToString()).gameObject;
            ItemBtn[i].GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
            ItemBtn[i].GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        }
        GameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        for (int i = 0; i < ItemSize; i++)
        {
            if (TempItemBtnPressed[i] && !ItemBtnPressed[i])
            {
                tempBtnTimer[i] += Time.deltaTime;
                if (tempBtnTimer[i] > 1.0f) // 1초 이상 눌리면 
                {
                    ItemBtnPressed[i] = true;
                }
            }
        }
        for (int i = 0; i < ItemSize/2; i++)
        {
            // 아이템 슬롯에 해당하는 버튼 2개가 동시에 눌리면
            if (ItemBtnPressed[i * 2] && ItemBtnPressed[i * 2 + 1])
            {
                // 아이템 슬롯이 차 있음
                ChampionSlot[i] = true;
                
                // 구문 한번만 실행
                if (!TempChampionSlot[i])
                {
                    // 가장 중요한 구문 
                    // 클릭 달성시 붙었음을 함수로 호출
                    GameManager.GetComponent<ItemAttachGE>().ItemAttach(i,this.transform);
                    TempChampionSlot[i] = true;
                }
            }
            else
                TempChampionSlot[i] = false;

        }
    }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        for (int i = 0; i < ItemSize; i++)
        {
            if (vb.name == "ItemCheck" + (i + 1).ToString())
            {
                TempItemBtnPressed[i] = true;
            }
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        for (int i = 0; i < ItemSize; i++)
        {
            if (vb.name == "ItemCheck" + (i + 1).ToString())
            {
                ItemBtnPressed[i] = false;
                TempItemBtnPressed[i] = false;
                tempBtnTimer[i] = 0;
            }
        }
    }
}
