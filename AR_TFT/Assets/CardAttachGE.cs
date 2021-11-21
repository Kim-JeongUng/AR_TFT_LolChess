using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardAttachGE : MonoBehaviour
{
    // 보드에 붙어서 어떤 챔피언이 붙었는지 전달

    public GameObject[] ChampionCard;
    float[] ChampionAttachTimer;
    public bool[] ChampionSlot = { false, false, false };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CardAttach(int ChampionSlotNum, Transform tf)
    {
        if (!ChampionSlot[ChampionSlotNum])  // 아이템이 안 차있으면
        {
            // 모든 챔피언 카드를 찾음 
            ChampionCard = GameObject.FindGameObjectsWithTag("Champion");
            // 모든 챔피언 카드의 타이머를 확인
            ChampionAttachTimer = new float[ChampionCard.Length];

            float minTime = 8.0f;
            int minIndex = 100;

            // 가장 최근에 인식된 카드 탐색

            for (int i = 0; i < ChampionCard.Length; i++)
            {
                ChampionAttachTimer[i] = ChampionCard[i].GetComponent<ChampionCard>().attachTimer;

                // 최소 시간, 카드 탐색
                if (minTime > ChampionAttachTimer[i])
                {
                    minTime = ChampionAttachTimer[i];
                    minIndex = i;
                }
            }
            if (minIndex != 100 || minTime != 8.0f)
            {
                ChampionCard[minIndex].transform.parent = tf;
                ChampionSlot[minIndex] = true;
                Debug.Log((minIndex + 1).ToString() + "카드가 슬롯" + (ChampionSlotNum + 1).ToString() + "칸에 장착");
            }
            else // 버튼은 클릭됐지만 위에 등록된 카드가 없음
                Debug.Log("등록된 카드가 올라가 있지 않습니다. 확인하세요.");
        }
    }
}
