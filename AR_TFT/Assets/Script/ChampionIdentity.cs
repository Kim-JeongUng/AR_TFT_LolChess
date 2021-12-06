using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionIdentity : MonoBehaviour
{   // 각 챔피언에 붙어서 기본 체력, 아이템 부여체력, 스킬사용 에니메이션 담당

    public string ChampName;
    public GameObject CompleteItemSpawn; //완성아이템 등장 위치 <<입력필요>>
    public GameObject CompleteItem; // 선택된 완성 아이템 
    public GameObject HPred, HPblack;

    // HP를 제외하고는 모두 FULL값을 넘을 수 있음(조합 템 효과)
    public int ChampFullHP;//최대 체력
    public int ChampHP; // 체력

    public int ChampFullAD; // 아이템까지 낀 총 합(조합템 영향 X)
    public int ChampAD; // 공격력

    public int ChampFullAP;
    public int ChampAP; // 주문력

    public float ChampFullAS;
    public float ChampAS; // 공격속도

    public float ChampFullSkillTime;
    public float ChampSkillTime; // 스킬재사용대기시간

    public float ChampFullSkillDamage; 
    public float ChampSkillDamage; // 스킬 데미지


    public float per;//체력 비율 계산용

    public GameGE GameManager;

    public float Gametimer = 0;

    public float ItemTimer = 0.0f;

    //아이템
    bool isBeltTears = false;  //구원
    bool isBeltWand = false;  //모렐로
    bool isSwordTears = false; //쇼진
    bool isBowWand = false; //구인수
    bool isTearsWand = false; //대천사
    public bool isSwordWand = false; //총검


    bool GameStartOnceCheck = true;

    float stunTimer = 0.0f;

    public bool sorakaSkill = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameGE>();
        // 기본 스텍 정의
        if (ChampName == "Vayne")//공속위주
        {
            ChampFullHP = 515;
            ChampHP = 515;
            ChampFullAD = 55;
            ChampAD = 55;
            ChampFullAP = 55;
            ChampAP = 55;
            ChampFullAS = 0.658f;
            ChampAS = 0.658f;
            ChampFullSkillTime = 8f;
            ChampSkillTime = 8f;
        }
        if (ChampName == "Soraka")//탱위주
        {
            ChampFullHP = 535;
            ChampHP = 535;
            ChampFullAD = 50;
            ChampAD = 50;
            ChampFullAP = 50;
            ChampAP = 50;
            ChampFullAS = 0.625f;
            ChampAS = 0.625f;
            ChampFullSkillTime = 10f;
            ChampSkillTime = 10f;
        }
        if (ChampName == "Janna")//보호막스킬위주
        {
            ChampFullHP = 500;
            ChampHP = 500;
            ChampFullAD = 52;
            ChampAD = 52;
            ChampFullAP = 52;
            ChampAP = 52;
            ChampFullAS = 0.625f;
            ChampAS = 0.625f;
            ChampFullSkillTime = 7f;
            ChampSkillTime = 7f;
        }
        if (ChampName == "Caitlyn")//공격력위주
        {
            ChampFullHP = 510;
            ChampHP = 510;
            ChampFullAD = 62;
            ChampAD = 62;
            ChampFullAP = 62;
            ChampAP = 62;
            ChampFullAS = 0.605f;
            ChampAS = 0.620f; 
            ChampFullSkillTime = 9f;
            ChampSkillTime = 9f;
        }
        if (ChampName == "Nidalee")//평타스킬 밸런스
        {
            ChampFullHP = 570;
            ChampHP = 570;
            ChampFullAD = 58;
            ChampAD = 58;
            ChampFullAP = 58;
            ChampAP = 58;
            ChampFullAS = 0.638f;
            ChampAS = 0.638f;
            ChampFullSkillTime = 6f;
            ChampSkillTime = 6f;
        }
        if (ChampName == "Twistedfate")//딜스킬 위주 / 스킬 상대 스턴 2초
        {
            ChampFullHP = 534;
            ChampHP = 534;
            ChampFullAD = 60;
            ChampAD = 60;
            ChampFullAP = 52;
            ChampAP = 52;
            ChampFullAS = 0.651f;
            ChampAS = 0.651f;
            ChampFullSkillTime = 10f;
            ChampSkillTime = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        per = (float)ChampHP / (float)ChampFullHP;
        HPred.transform.localPosition = new Vector3(-50 + (50 * per), 150, 0);
        HPred.transform.localScale = new Vector3(13, 50 * per, 13);
        HPblack.transform.localPosition = new Vector3((50 * per), 150, 0);
        HPblack.transform.localScale = new Vector3(13, -50 + (50 * per), 13);
        if (!GameGE.isGamePlaying) // 라운드 끝
        {
            Gametimer = 0.0f;

            //매 라운드마다 초기화
            ChampHP = ChampFullHP;
            ChampAD = ChampFullAD;
            ChampAP = ChampFullAP;
            ChampAS = ChampFullAS;
            ChampSkillTime = ChampFullSkillTime;
            ChampSkillDamage = ChampFullSkillDamage;

            HPred.SetActive(false);
            HPblack.SetActive(false);
            isBeltTears = false;
            ItemTimer = 0.0f;
            GameStartOnceCheck = true;
            sorakaSkill = false;

            this.transform.GetChild(5).GetChild(0).GetComponent<BattleManager>().skillcount = 0;
            this.transform.GetChild(5).GetChild(0).localPosition = new Vector3(0, 0, 0);
            this.transform.GetChild(5).GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);

            this.transform.GetChild(5).GetChild(0).GetComponent<BattleManager>().Shield.gameObject.SetActive(false);
            if (CompleteItemSpawn.transform.childCount == 1)
                CompleteItemSpawn.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (GameGE.isGamePlaying)
        {
            if (GameStartOnceCheck) // 게임시작시 한번만 실행
            {
                if (this.GetComponent<ChampionCard>().MyItem[0] != null && this.GetComponent<ChampionCard>().MyItem[1] != null)
                {
                    GetCompleteItem(this.GetComponent<ChampionCard>().MyItem[0], this.GetComponent<ChampionCard>().MyItem[1]);
                }
                HPred.SetActive(true);
                HPblack.SetActive(true);
                GameStartOnceCheck = false;
            }
            Gametimer += Time.deltaTime;
            //라운드 시작시 각 아이템 확인


        }

        if (ChampHP <= 0 && GameGE.isGamePlaying)
        {
            this.transform.GetChild(5).GetChild(0).gameObject.GetComponent<Animator>().SetBool("Attack", false);
            this.transform.GetChild(5).GetChild(0).gameObject.GetComponent<Animator>().SetBool("Death", true);
        }

        if (isBeltTears || isSwordTears || isBowWand || isTearsWand || isBeltWand) // 구원 / 쇼진 / 구인수 / 대천사 / 모렐로
        {
            ItemTimer += Time.deltaTime;
            if (ItemTimer > 3.0f)
            {
                ItemTimer = 0.0f;

                if (isSwordTears)
                    ChampAD += 3;
                else if (isBowWand)
                    ChampAS += 0.05f;
                else if (isTearsWand)
                    ChampAP += 3;
                else if (isBeltTears)
                {
                    foreach (ChampionIdentity Team in this.transform.parent.GetComponentsInChildren<ChampionIdentity>())
                    {
                        if (this.ChampHP > 0)
                        {
                            if (Team.ChampHP > 0)
                            {
                                Team.ChampHP += 10;
                                Team.transform.GetChild(5).GetChild(0).GetComponent<BattleManager>().BeltTears.startColor = Color.white;
                                Team.transform.GetChild(5).GetChild(0).GetComponent<BattleManager>().BeltTears.Play();  // 구원 이팩트 재생
                            }
                            
                        }
                    }
                }
                else if (isBeltWand)
                {
                    foreach (ChampionIdentity Enemy in this.transform.parent.GetComponent<Board>().Enemy.GetComponent<Board>().GetComponentsInChildren<ChampionIdentity>())
                    {
                        if (this.ChampHP > 0)
                        {
                            if (Enemy.ChampHP > 0)
                            {
                                Enemy.ChampHP -= 10;
                                Enemy.transform.GetChild(5).GetChild(0).GetComponent<BattleManager>().BeltTears.startColor = Color.black;
                                Enemy.transform.GetChild(5).GetChild(0).GetComponent<BattleManager>().BeltTears.Play();
                            }
                        }
                    }
                }
            }
        }
    }

    //테스트 완료
    public void GetNewItem(GameObject NewItem)
    {
        if (NewItem.name == "Belt")
        {
            ChampFullHP += 150;
            ChampHP += 150;
        }
        if (NewItem.name == "Bow")
        { 
            ChampFullAS += 0.1f;
            ChampAS += 0.1f;
        }
        if (NewItem.name == "Sword")
        {
            ChampFullAD += 30;
            ChampAD += 30;
        }
        if (NewItem.name == "Tears")
        {
            ChampFullSkillTime *= 0.8f;
            ChampSkillTime *= 0.8f;   // 스킬쿨타임 20퍼 감소
        }
        if (NewItem.name == "Wand")
        {
            ChampFullAP += 30;
            ChampAP += 30;
        }
    }

    //테스트 미시도
    public void GetCompleteItem(GameObject Item1, GameObject Item2) // 장착 아이템이 2개일 때 완성 아이템 관여
    {
        foreach (GameObject PickItems in GameManager.CompleteItems)
        {
            // 아이템 이름으로 완성아이템 검색 ex) Sword + Wand = SwordWand
            if (PickItems.name == Item1.name + Item2.name || PickItems.name == Item2.name + Item1.name)
            {
                CompleteItem = PickItems;
            }
        }
        if(CompleteItemSpawn.transform.childCount==0)
            Instantiate(CompleteItem, CompleteItemSpawn.transform.position, Quaternion.identity, CompleteItemSpawn.transform);

        CompleteItemSpawn.transform.GetChild(0).gameObject.SetActive(true);
        this.GetComponent<CardCheckItem>().ItemSlotSpace[0].transform.GetChild(0).gameObject.SetActive(false);
        this.GetComponent<CardCheckItem>().ItemSlotSpace[1].transform.GetChild(0).gameObject.SetActive(false);

        if (CompleteItem.name == "BeltBelt")  //워모그 - 매 라운드 체력 + 50씩 증가
        {
            ChampFullHP += 50;
            ChampHP += 50;
        }
        else if (CompleteItem.name == "BeltBow") // 즈롯 - 기능구현 앞으로 10만큼 이동함 (어그로 받아줌)
        {
            this.transform.GetChild(5).GetChild(0).localPosition = new Vector3(this.transform.GetChild(5).GetChild(0).localPosition.x, this.transform.GetChild(5).GetChild(0).localPosition.y, this.transform.GetChild(5).GetChild(0).localPosition.z + 70);
        }
        else if (CompleteItem.name == "BeltSword") // 지크 - 아군 전체 공격속도 0.1 빨라짐
        {
            foreach (ChampionIdentity Team in this.transform.parent.GetComponentsInChildren<ChampionIdentity>())
            {
                Team.ChampAS += 0.1f;
            }
        }
        //구원 죽은애는 ㄴㄴ 하게 구원 에니메이션 팀 전부 / 캔버스 끝나고 안뜸 / 베인 일반공격 이상 / 트페 이상함 / 베인 레이저 ??
        // 
        else if (CompleteItem.name == "BeltTears") // 구원  3초마다 아군 체력 +10
        {
            isBeltTears = true;
        }
        else if (CompleteItem.name == "BeltWand") // 모렐 3초마다 상대 체력 -10
        {
            isBeltWand = true;
        }
        else if (CompleteItem.name == "BowBow") // 고연포 - 멀리서 때림
        {
            this.transform.GetChild(5).GetChild(0).localPosition = new Vector3(this.transform.GetChild(5).GetChild(0).localPosition.x, this.transform.GetChild(5).GetChild(0).localPosition.y, this.transform.GetChild(5).GetChild(0).localPosition.z - 70);
        }
        else if (CompleteItem.name == "BowSword") // 거학 - 상대 체력 3% 만큼 추가 데미지
        {
            this.transform.GetChild(5).GetChild(0).GetComponent<BattleManager>().isBowSword = true;
        }
        else if (CompleteItem.name == "BowTears") // 얼심 - 상대 공속 - 0.1
        {
            foreach (ChampionIdentity Enemy in this.transform.parent.GetComponent<Board>().Enemy.GetComponent<Board>().GetComponentsInChildren<ChampionIdentity>())
            {
                Enemy.ChampAS -= 0.1f;
            }
        }
        else if (CompleteItem.name == "BowWand") // 구인수 - 공속 - 게임시간비례 더 늘어나게
        {
            isBowWand = true;
        }
        else if (CompleteItem.name == "SwordSword") // 죽검 - 공격력 추가증가
        {
            ChampAD += 30;
        }
        else if (CompleteItem.name == "SwordTears") // 쇼진 - 3초마다 공격력 + 3
        {
            isSwordTears = true;
        }
        else if (CompleteItem.name == "SwordWand") // 총검 - 쏠 때 마다 공격력 비례 체력회복
        {
            isSwordWand = true;
        }
        else if (CompleteItem.name == "TearsTears") //블루 - 쿨타임 추가 20퍼 
        {
            ChampFullSkillTime *= 0.8f;
        }
        else if (CompleteItem.name == "TearsWand") //대천사 - 3초마다 주문력 +3
        {
            isTearsWand = true;
        }
        else if (CompleteItem.name == "WandWand") //라바돈 - 주문력 추가증가
        {
            ChampAP += 30;
        }
        
    }

}
