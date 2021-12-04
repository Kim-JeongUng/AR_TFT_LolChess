using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGE : MonoBehaviour
{
    const int NumberOfChampion = 6;
    const int NumberOfItem = 5;

    public GameObject BlueBoard;
    public GameObject RedBoard;

    public static bool isGamePlaying;
    public bool GameCard;

    //상대팀 보드와 내 보드 탐색
    public GameObject EnemyBoard;
    public GameObject MyBoard;
    
    //최소거리 탐색
    public GameObject [] FoundObjects;
    public GameObject enemy;
    public string TagName;
    public float shortDis;

    public static float[] ChampionAttachTimer = new float[NumberOfChampion];
    public static float[] ItemAttachTimer = new float[NumberOfItem];

    public static GameObject[] ChampionCards = new GameObject [NumberOfChampion];
    public static GameObject[] ItemCards = new GameObject[NumberOfItem]; //아이템마커


    public GameObject[] Items = new GameObject[NumberOfItem]; // 아이템 

    public int Round; //게임 라운드

    


    // Start is called before the first frame update
    void Start()
    {
        isGamePlaying = false;
        Round = 0; // 게임 라운드
        for (int i = 0; i < NumberOfChampion; i++)
            ChampionCards[i] = GameObject.Find("ChampionCard" + (i + 1).ToString());

        for (int i = 0; i < NumberOfItem; i++)
            ItemCards[i] = GameObject.Find("Item" + (i + 1).ToString());
    }

    // Update is called once per frame
    void Update()
    {

        for(int i=0; i < NumberOfChampion; i++)
            ChampionAttachTimer[i] = ChampionCards[i].GetComponent<ChampionCard>().attachTimer;
        for (int i = 0; i < NumberOfItem; i++)
            ItemAttachTimer[i] = ItemCards[i].GetComponent<ItemCard>().attachTimer;


        if (BlueBoard.GetComponent<Board>().PlayerHP <= 0)
        {
            //블루팀이 짐
            //GameOver(BlueBoard, RedBoard); //이긴팀, 진팀 다른 화면 출력
        }

        /*else if (RedBoard.GetComponent<Board>().PlayerHP <= 0)
        {
            //레드팀이 짐
        }*/

        // 보드에 챔피언들을 찾음
        else if (BlueBoard.GetComponent<Board>().EqupChampionCount > 0 && RedBoard.GetComponent<Board>().EqupChampionCount > 0) // && RedBoard.GetComponent<Board>().EqupChampionCount == 3 
        {
            if (!isGamePlaying)
            {
                if (!isGamePlaying && GameCard) // 게임 시작 카드 확인
                {
                    Round++; //라운드
                    Debug.Log("startCard Check!");
                    isGamePlaying = true;
                }
            }
            else if (isGamePlaying) 
            {
                /*if (BlueBoard.GetComponent<Board>().MyChampion[0].GetComponent<ChampionIdentity>().ChampHP <= 0 && BlueBoard.GetComponent<Board>().MyChampion[1].GetComponent<ChampionIdentity>().ChampHP <= 0 && BlueBoard.GetComponent<Board>().MyChampion[2].GetComponent<ChampionIdentity>().ChampHP <= 0)
                {
                    //블루 팀 라운드 패 
                    BlueBoard.GetComponent<Board>().PlayerHP -= Round * 7; // 라운드 *3 만큼
                    Debug.Log("RedBoard Win GameOver!");
                    isGamePlaying = false;
                }
                else if (RedBoard.GetComponent<Board>().MyChampion[0].GetComponent<ChampionIdentity>().ChampHP <= 0 && RedBoard.GetComponent<Board>().MyChampion[1].GetComponent<ChampionIdentity>().ChampHP <= 0 && RedBoard.GetComponent<Board>().MyChampion[2].GetComponent<ChampionIdentity>().ChampHP <= 0)
                {
                    //레드 팀 라운드 패
                    RedBoard.GetComponent<Board>().PlayerHP -= Round * 7;
                    Debug.Log("BlueBoard Win GameOver!");
                    isGamePlaying = false;
                }*/
                for (int i = 0; i < 3; i++)
                {
                    // 살아있는 챔피언 출력
                    if (BlueBoard.GetComponent<Board>().MyChampion[i].name != "EmptyGameObject")
                    {
                        Debug.Log("BLUE Team" + (i+1).ToString() + BlueBoard.GetComponent<Board>().MyChampion[i].name + "    HP:" + BlueBoard.GetComponent<Board>().MyChampion[i].GetComponent<ChampionIdentity>().ChampHP.ToString());
                        if (BlueBoard.GetComponent<Board>().MyChampion[i].GetComponent<ChampionIdentity>().ChampHP > 0) // 살아 있으면
                        {
                            //Fight
                            LookAroundEnemyChamp(BlueBoard.GetComponent<Board>().MyChampion, i);
                        }
                    }
                    if (RedBoard.GetComponent<Board>().MyChampion[i].name != "EmptyGameObject")
                    {
                        Debug.Log("RED Team" + (i + 1).ToString() + RedBoard.GetComponent<Board>().MyChampion[i].name + "    HP:" + RedBoard.GetComponent<Board>().MyChampion[i].GetComponent<ChampionIdentity>().ChampHP.ToString());
                        if (RedBoard.GetComponent<Board>().MyChampion[i].GetComponent<ChampionIdentity>().ChampHP > 0) // 살아 있으면
                        {
                            //Fight
                            LookAroundEnemyChamp(RedBoard.GetComponent<Board>().MyChampion, i);
                        }
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
        Debug.Log(OurCard[index].transform.parent.name);
        if (OurCard[index].transform.parent.name == "RedBoard")
        {
            EnemyBoard = BlueBoard;
            MyBoard = RedBoard;
        }
        if (OurCard[index].transform.parent.name == "BlueBoard")
        {
            EnemyBoard = RedBoard;
            MyBoard = BlueBoard;

        }

        if (MyBoard.GetComponent<Board>().MyChampion[index].GetComponent<ChampionIdentity>().ChampHP > 0)
        {
            FoundObjects = EnemyBoard.GetComponent<Board>().MyChampion;

            // 첫번째를 기준으로 잡아주기 
            shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);  // 거리
            enemy = FoundObjects[0];    // 적 GameObject

            foreach (GameObject found in FoundObjects)
            {
                // 상대가 체력이 있는지 확인
                if (found.name != "EmptyGameObject" && found.GetComponent<ChampionIdentity>().ChampHP > 0)
                {
                    float Distance = Vector3.Distance(OurCard[index].transform.position, found.transform.position);
                    if (Distance < shortDis)
                    {
                        enemy = found;
                        shortDis = Distance;
                    }
                }
            }
            if (enemy.name != "EmptyGameObject" && enemy.GetComponent<ChampionIdentity>().ChampHP > 0)
            {
                Quaternion lookOnLook = Quaternion.LookRotation(enemy.transform.GetChild(5).GetChild(0).position - OurCard[index].transform.GetChild(5).GetChild(0).position);
                OurCard[index].transform.GetChild(5).GetChild(0).rotation = Quaternion.Slerp(OurCard[index].transform.GetChild(5).GetChild(0).rotation, lookOnLook, Time.deltaTime);
                OurCard[index].transform.GetChild(5).GetChild(0).GetComponent<BattleManager>().target = enemy;
                OurCard[index].transform.GetChild(5).GetChild(0).GetComponent<Animator>().SetBool("Attack", true);
                Debug.Log(enemy);
            }
            else
            {
                Debug.Log(MyBoard+"Win");
            }
            // 평타
            //Debug.Log(shortDis);
        }
    }
    public void GameStartCard()
    {
        GameCard = true;
    }
    public void LostGameStartCard()
    {
        GameCard = false;
    }
}
