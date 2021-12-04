using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CardCheckItem : MonoBehaviour
{
    // 각 챔피언 카드에 붙어서 버츄얼 버튼을 클릭(아이템이 장착되었는지?)을 담당

    public GameObject GameManager;

    public bool[] ItemSlot = { false, false };

    // 아이템 카드 두칸
    private static readonly int ItemSize = 4;
    public GameObject[] ItemBtn = new GameObject[ItemSize];

    // 버튼이 눌림 확인
    public bool[] TempItemBtnPressed = new bool[ItemSize];

    // 버튼이 눌린 시간 확인
    private float[] tempBtnTimer = new float[ItemSize];

    // 일정 시간 이상 눌리면 버튼이 제대로 눌린게 맞음
    public bool[] ItemBtnPressed = new bool[ItemSize];

    // 한번만 실행하는 아이템 슬롯 함수
    public bool[] TempItemSlot = new bool[ItemSize / 2];


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
                
                // 구문 한번만 실행
                if (!TempItemSlot[i])
                {
                    // 가장 중요한 구문 
                    // 클릭 달성시 붙었음을 함수로 호출

                    StartCoroutine(WaitForIt(i));
                    TempItemSlot[i] = true;
                }
            }
            else
                TempItemSlot[i] = false;
        }
    }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        for (int i = 0; i < ItemSize; i++)
        {
            if (vb.name == transform.name+"ItemCheck" + (i + 1).ToString())
            {
                TempItemBtnPressed[i] = true;
            }
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        for (int i = 0; i < ItemSize; i++)
        {
            if (vb.name == transform.name + "ItemCheck" + (i + 1).ToString())
            {
                ItemBtnPressed[i] = false;
                TempItemBtnPressed[i] = false;
                tempBtnTimer[i] = 0;
            }
        }
    }
    IEnumerator WaitForIt(int ItemSlotNum) // 1초 마다 재탐색
    {
        yield return new WaitForSeconds(1.0f);
        CardAttach(ItemSlotNum);
    }
    public void CardAttach(int ItemSlotNum)
    {
        if (!ItemSlot[ItemSlotNum])  // 아이템이 안 차있으면
        {
            // 모든 챔피언 카드의 타이머를 확인

            float minTime = 100.0f;
            int minIndex = 100;

            // 가장 최근에 인식된 카드 탐색
            for (int i = 0; i < 2; i++)
            {
                // 최소 시간, 카드 탐색
                if (minTime > GameGE.ItemAttachTimer[i])
                {
                    float tempTime = GameGE.ItemAttachTimer[i];
                    int tempIndex = i;
                    if (!GameGE.ItemCards[tempIndex].transform.parent && GameGE.ItemCards[tempIndex].GetComponent<ItemCard>().isattach)
                    {
                        minTime = tempTime;
                        minIndex = i;
                    }
                }
            }
            if (minIndex < 100)
            {
                GameGE.ChampionCards[minIndex].transform.parent = this.transform;
                ItemSlot[ItemSlotNum] = true;
                this.GetComponent<ChampionCard>().MyItem[ItemSlotNum] = GameGE.ItemCards[minIndex];
                this.GetComponent<ChampionCard>().EqupItemCount++;
                Debug.Log((minIndex + 1).ToString() + "아이템이 슬롯" + (ItemSlotNum + 1).ToString() + "칸에 장착");
            }
            else // 버튼은 클릭됐지만 위에 등록된 카드가 없음
            {
                Debug.Log("등록된 카드가 올라가 있지 않습니다. 확인하세요.");
                StartCoroutine(WaitForIt(ItemSlotNum));
            }
        }
    }
}
