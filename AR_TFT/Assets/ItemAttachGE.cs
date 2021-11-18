using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttachGE : MonoBehaviour
{
    // 각 챔피언 카드에 붙어서 어떤 아이템이 붙었는지 전달

    public GameObject[] ItemCard;
    float[] ItemAttachTimer;
    public bool[] ItemSlot = { false, false };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ItemAttach(int ItemSlotNum, Transform tf) // tf = 붙을 객체
    {
        if (!ItemSlot[ItemSlotNum])  // 아이템이 안 차있으면
        {
            // 모든 아이템 카드를 찾음 
            ItemCard = GameObject.FindGameObjectsWithTag("Item");
            // 모든 아이템 카드의 타이머를 확인
            ItemAttachTimer = new float[ItemCard.Length];

            float minTime = 8.0f;
            int minIndex = 100;

            // 가장 최근에 인식된 아이템 카드 탐색
            for (int i = 0; i < ItemCard.Length; i++)
            {
                ItemAttachTimer[i] = ItemCard[i].GetComponent<ItemCard>().attachTimer;

                // 최소 시간, 카드 탐색
                if (minTime > ItemAttachTimer[i])
                {
                    minTime = ItemAttachTimer[i];
                    minIndex = i;
                }
            }
            if (minIndex != 100 || minTime != 8.0f)
            {
                // 자식이 아니면
                if (ItemCard[minIndex].transform.parent != tf)
                {
                    // 자식 등록 및 아이템 설정
                    ItemCard[minIndex].transform.parent = tf;
                    ItemSlot[ItemSlotNum] = true;
                    Debug.Log((minIndex + 1).ToString() + "아이템이 " + tf.name + "카드의슬롯" + (ItemSlotNum + 1).ToString() + "칸에 장착");
                }
            }
            else // 버튼은 클릭됐지만 위에 등록된 카드가 없음
                Debug.Log("아이템이 올라가 있지 않습니다. 확인하세요.");
        }
    }
}
