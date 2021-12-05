using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionIdentity : MonoBehaviour
{   // 각 챔피언에 붙어서 기본 체력, 아이템 부여체력, 스킬사용 에니메이션 담당

    public string ChampName;
    public GameObject CompleteItemSpawn; //완성아이템 등장 위치 <<입력필요>>
    public GameObject CompleteItem; // 선택된 완성 아이템 
    public GameObject HPred, HPblack;

    public int ChampFullHP;//최대 체력
    public int ChampHP; // 체력
    public int ChampAD; // 공격력
    public int ChampAP; // 주문력
    public float ChampAS; // 공격속도
    public float ChampSkillTime; // 스킬재사용대기시간
    public float ChampSkillDamage; // 스킬 데미지

    public float per;//체력 비율 계산용

    public GameGE GameManager;

    public float Gametimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameGE>();
        // 기본 스텍 정의
        if (ChampName == "Vayne")
        {
            ChampFullHP = 450;
            ChampHP = 450;
            ChampAD = 70;
            ChampAP = 0;
            ChampAS = 0.8f;
            ChampSkillTime = 5.0f;
        }
        if (ChampName == "Soraka")
        {
            ChampFullHP = 450;
            ChampHP = 450;
            ChampAD = 70;
            ChampAP = 0;
            ChampAS = 0.8f;
            ChampSkillTime = 5.0f;
        }
        if (ChampName == "Janna")
        {
            ChampFullHP = 450;
            ChampHP = 450;
            ChampAD = 70;
            ChampAP = 0;
            ChampAS = 0.8f;
            ChampSkillTime = 5.0f;
        }
        if (ChampName == "Caitlyn")
        {
            ChampFullHP = 450;
            ChampHP = 450;
            ChampAD = 70;
            ChampAP = 0;
            ChampAS = 0.8f;
            ChampSkillTime = 5.0f;
        }
        if (ChampName == "Nidalee")
        {
            ChampFullHP = 450;
            ChampHP = 450;
            ChampAD = 70;
            ChampAP = 0;
            ChampAS = 0.8f;
            ChampSkillTime = 5.0f;
        }
        if (ChampName == "Twistedfate")
        {
            ChampFullHP = 450;
            ChampHP = 450;
            ChampAD = 70;
            ChampAP = 0;
            ChampAS = 0.8f;
            ChampSkillTime = 5.0f;
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
        if (!GameGE.isGamePlaying)
        {
            Gametimer = 0.0f;
            ChampHP = ChampFullHP;
            HPred.SetActive(false);
            HPblack.SetActive(false);
        }
        else if (GameGE.isGamePlaying)
        {
            Gametimer += Time.deltaTime;
            HPred.SetActive(true);
            HPblack.SetActive(true);
        }

        if (ChampHP <= 0 && GameGE.isGamePlaying)
        {
            this.transform.GetChild(5).GetChild(0).gameObject.GetComponent<Animator>().SetBool("Attack", false);
            this.transform.GetChild(5).GetChild(0).gameObject.GetComponent<Animator>().SetBool("Death", true);
        }
    }

    //테스트 완료
    public void GetNewItem(GameObject NewItem)
    {
        Debug.Log(NewItem);
        if (NewItem.name == "Belt")
        {
            ChampFullHP += 200;
            ChampHP += 200;
            Debug.Log("AAB");
        }
        if (NewItem.name == "Bow")
        {
            ChampAS += 0.1f;
        }
        if (NewItem.name == "Sword")
        {
            ChampAD += 30;
        }
        if (NewItem.name == "Tears")
        {
            ChampSkillTime *= 0.8f;   // 스킬쿨타임 20퍼 감소
        }
        if (NewItem.name == "Wand")
        {
            ChampAP += 30;
        }
        if(!this.GetComponent<ChampionCard>().MyItem[0] || !this.GetComponent<ChampionCard>().MyItem[0])
        {

        }
        else
            GetCompleteItem(this.GetComponent<ChampionCard>().MyItem[0], this.GetComponent<ChampionCard>().MyItem[1]);
    }

    //테스트 미시도
    public void GetCompleteItem(GameObject Item1, GameObject Item2) // 완성 아이템 관여
    {
        foreach (GameObject PickItems in GameManager.CompleteItems)
        {
            // 아이템 이름으로 완성아이템 검색 ex) Sword + Wand = SwordWand
            if (PickItems.name == Item1.name + Item2.name || PickItems.name == Item2.name + Item1.name)
            {
                CompleteItem = PickItems;
                Debug.Log("PICK!" + CompleteItem.name);

                Instantiate(CompleteItem, CompleteItemSpawn.transform.position, Quaternion.identity);

                //삭제를 해야하나?
                Destroy(this.GetComponent<CardCheckItem>().ItemSlotSpace[0]);
                Destroy(this.GetComponent<CardCheckItem>().ItemSlotSpace[1]);
            }
        }
        if (CompleteItem.name == "BeltBelt")  //워모그
        {
            ChampFullHP += 200;
            ChampHP += 200;
            // 기능 구현 - 체력 + 200 (총 600)
        }
        else if (CompleteItem.name == "BeltBow") // 즈롯 - 기능구현 앞으로 10만큼 이동함 (어그로 받아줌)
        {
            this.transform.GetChild(5).GetChild(0).localPosition = new Vector3(this.transform.GetChild(5).GetChild(0).localPosition.x, this.transform.GetChild(5).GetChild(0).localPosition.y, this.transform.GetChild(5).GetChild(0).localPosition.z + 100);
        }
        else if (CompleteItem.name == "BeltSword") // 지크 - 아군 전체 공격속도 0.1 빨라짐
        {
            foreach (ChampionIdentity Team in this.transform.parent.GetComponentsInChildren<ChampionIdentity>())
            {
                Team.ChampAS += 0.1f;
            }
        }
        else if (CompleteItem.name == "BeltTears") // 구원
        {
        }
        else if (CompleteItem.name == "BeltWand") // 모렐 - 치유, 보호막감소
        {
        }
        else if (CompleteItem.name == "BowBow") // 고연포 - 멀리서 때림
        {
            this.transform.GetChild(5).GetChild(0).localPosition = new Vector3(this.transform.GetChild(5).GetChild(0).localPosition.x, this.transform.GetChild(5).GetChild(0).localPosition.y, this.transform.GetChild(5).GetChild(0).localPosition.z - 100);
        }
        else if (CompleteItem.name == "BowSword") // 거학 - 체력비례
        {
        }
        else if (CompleteItem.name == "BowTears") // 스태틱
        {
        }
        else if (CompleteItem.name == "BowWand") // 구인수 - 공속 - 게임시간비례 더 늘어나게
        {
            float temp = Gametimer % 3.0f;

        }
        else if (CompleteItem.name == "SwordSword") // 죽검 - 공격력 뻥튀기
        {
            ChampAD += 30;
        }
        else if (CompleteItem.name == "SwordTears") // 
        {
        }
        else if (CompleteItem.name == "SwordWand") //
        {
        }
        else if (CompleteItem.name == "TearsTears") //
        {
        }
        else if (CompleteItem.name == "TearsWand") //
        {
        }
        else if (CompleteItem.name == "WandWand") //
        {
        }
    }

    //테스트 미시도
    public void UseSkill()
    {
        //ChampName.GetComponent<Animation>().Play("Skill");
        if (ChampName == "Caitlyn")
        {
            ChampSkillDamage = (ChampAP * 3) + (ChampAD * 0.8f);
            // 가장 먼 적에게 거리비례 데미지 궁극기
        }
        if (ChampName == "Vayne")
        {
            // 3대 마다 대상 체력 ?퍼 추가피해
        }
        if (ChampName == "Soraka")
        {
            // 아군 전체 체력 회복
        }
        if (ChampName == "Janna")
        {
            // 피해가 가장 심한 챔프 3초간 큰 보호막 제공
        }


    }
}
