using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionCard : MonoBehaviour
{
    // 알고리즘
    // 챔피언 슬롯 위치에 챔피언 카드가 들어갔는지 판별
    // 1. 카드의 ar 위치가 오른쪽에 붙어있는가? (물리적 좌표)
    // 2. VB될시 에서 CardAttachGE.AttachCard(vb.name) 호출, 앞 뒤 가장 최근에 인식된 마커 챔피언 카드를 attach (보드판의 자식으로 등록), 캐릭터 AR
    // 2번사용
    // Start is called before the first frame update

    public bool isattach = false;
    public float attachTimer = 0.0f;
    public GameObject[] MyItem = new GameObject[2]; //장착된 아이템
    public int EqupItemCount = 0;
    void Start ()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 추후 <자리를 찾는중> 이면으로 수정해야함
        if (isattach == true && attachTimer<100)
        {
            attachTimer += Time.deltaTime;
        }
        
    }

    public void CheckMark()
    {
        isattach = true;
    }
    public void LostMark()
    {
        isattach = false;
    }

}
