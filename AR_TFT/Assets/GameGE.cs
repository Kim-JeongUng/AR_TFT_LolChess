using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGE : MonoBehaviour
{
    public GameObject BlueBoard;
    public GameObject RedBoard;

    // 인식되면 3초후 게임 시작
    public GameObject GameStartCard;

    public bool isGamePlaying;

    //상대팀 보드(임시)
    public GameObject EnemyBoard;


    //최소거리 탐색
    public GameObject [] FoundObjects;
    public GameObject enemy;
    public string TagName;
    public float shortDis;

    // Start is called before the first frame update
    void Start()
    {
        isGamePlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 보드에 챔피언들을 찾음
        if (BlueBoard.GetComponent<Board>().MyChampion.Length > 0) // && RedBoard.GetComponent<Board>().MyChampion.Length > 0)
        {
            if (!isGamePlaying)
            {
                if (!isGamePlaying && GameStartCard.GetComponent<MyDefaultTrackableEventHandler>().isAttach) // 게임 시작 카드 확인
                {
                    isGamePlaying = true;
                }
            }
            else if (isGamePlaying) // 추후 보드판에 붙은 캐릭터가 6개 다 인식되면
            {
                for (int i = 0; i < Board.ChampSize; i++)
                {
                    // 살아있는 챔피언 출력
                    Debug.Log("BLUE Team" + i.ToString() + BlueBoard.GetComponent<Board>().MyChampion[i] + BlueBoard.GetComponent<Board>().ChampionHP[i].ToString());
                    if (BlueBoard.GetComponent<Board>().ChampionHP[i] > 0) // 살아 있으면
                    {
                        LookAroundEnemyChamp(BlueBoard.GetComponent<Board>().MyChampion, i);
                    }
                }
            }
        }
        // BlueBoard.GetComponent<Board>().MyChampion =  
        // 이곳에서 챔피언카드를 확인하고 해당 챔피언 카드의 아이템까지 확인 

        // 각 보드에 챔피언 카드가 1장이상 붙어 있을 경우

    }

    // 가까운 적 찾기
    public void LookAroundEnemyChamp(GameObject[] OurCard, int index)
    {
        if (OurCard[index].transform.parent.name == "BlueBoard")
        {
            EnemyBoard = RedBoard;
        }
        if (OurCard[index].transform.parent.name == "RedBoard")
        {
            EnemyBoard = BlueBoard;
        }


        FoundObjects = EnemyBoard.GetComponent<Board>().MyChampion;
        
        // 첫번째를 기준으로 잡아주기 
        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);  // 거리
        enemy = FoundObjects[0];    // 적 GameObject

        foreach (GameObject found in FoundObjects)
        {
            // 상대가 체력이 있는지 확인
            float Distance = Vector3.Distance(OurCard[index].transform.position, found.transform.position);
            if (Distance < shortDis) 
            {
                enemy = found;
                shortDis = Distance;
            }
        }

        Debug.Log(enemy);
        Debug.Log(shortDis);

        // 상대팀의 같은 인덱스의 체력 확인, 없으면 +1, -1 확인(+우선), 없으면 +2, -2 확인
        // 내 앞에 적이 있음
        // 가장 가까이 있는 적 확인
    }
}
