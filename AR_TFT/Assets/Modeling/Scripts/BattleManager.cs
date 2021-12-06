using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //public GameObject Bullet;  // 총알 오브젝트
    //public Transform bltSpawnPoint;  // 총알 스폰위치
    //private float bulletSpeed = 2f;  // 총알 발사 속도

    public float attackSpeed = 0.6f;  // 공격속도
    public float atk = 0;

    public float skillcount = 0;

    public Animator anim;  // 공격 애니메이션

    public GameObject target;
    public float attackDelay = 1.917f;  // 공격 애니메이션 딜레이******** 챔피언마다 값 교체 필요함
    public float skillDelay = 1.25f;  // 스킬 애니메이션 딜레이 ********* 챔피언마다 값 교체 필요함

    public ParticleSystem skillEfect;
    public ParticleSystem BeltTears;

    public GameObject Bullet;
    public Transform bulletPos;

    public GameObject skill_Bullet;

    Bullet bulletTarget;

    public bool isBowSword =false;

    public bool isTwistedfateSkillHit = false;
    float stunTimer = 0.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isTwistedfateSkillHit)
        {
            attackSpeed = 0.0001f; 
            stunTimer += Time.deltaTime;
            if (stunTimer > 3.0f) //2초 스턴
            {
                stunTimer = 0;
                isTwistedfateSkillHit = false;
            }
        }
        else
            attackSpeed = this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAS;
        anim.SetFloat("attackSpeed", attackSpeed);  // 공격딜레이, 공격속도에 영향
        
        if (this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampHP < 0)
            this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampHP = 0;

        if (this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampHP > this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampFullHP)
            this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampHP = this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampFullHP;

        if (anim.GetBool("Attack") == true)
        {
            skillcount = skillcount + Time.deltaTime;  //스킬 쿨타임
            // skillcool 감소에 따라 마나 게이지 UI 작동
            if (skillcount >= this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampSkillTime)
            {
                //StartCoroutine("skillanim", (1.0f));
                // anim.SetBool("Attack", false); // Attack Stop
                anim.SetBool("Skill", true);  // skill animation start
                

                if (anim.GetCurrentAnimatorStateInfo(0).IsTag("skillanimation"))
                {
                    skillEfect.Play();

                    fireSkill(target);
                    anim.SetBool("Skill", false);
                    skillcount = 0;

                    atk += skillDelay;  // 스킬 딜레이 시간-------------------------------------
                }
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("attack")) // attack 애니메이션 실행되면
            {
                // 일반공격 스피어 발사
                firebullet(target);
            }
        }


        //---------테스트용----------------------------
        //-------------(실행 조건 없음)----------------
        if (Input.GetKeyDown(KeyCode.S))  // 공격종료 조건 없음
        {
            anim.SetBool("Attack", false); // Attack Stop
        }

        if (Input.GetKeyDown(KeyCode.A))  // 테스트 
        {
            anim.SetBool("Attack", true); // Attack Start
            StartCoroutine(Firebullet(target));
        }
        //---------------------------------------------

        if (Input.GetKeyDown(KeyCode.D))  // HP가 0이되면 실행
        {
            anim.SetBool("Death", true); // Death
        }
        if (Input.GetKeyDown(KeyCode.F))  // 모든 팀원이 죽고 전투종료 시 실행
        {
            anim.SetBool("Death", false); // 다시 살아남
            anim.SetBool("Attack", false);
        }
        //-----------------------------------------
    }
    public void fireSkill(GameObject Target)
    {
        if (this.name == "Soraka" || this.name == "Janna")
        {
            //소라카 잔나 체력회복 스킬 구현
            if (this.name == "Soraka") //
            {
            }
            if (this.name == "Janna") // 
            {
            }
        }
        else
        {
            GameObject skill = Instantiate(skill_Bullet, bulletPos.transform.position, bulletPos.transform.rotation);
            bulletTarget = skill.GetComponent<Bullet>();
            bulletTarget.Target = Target.transform;  // 가장 가까운 상대 챔피언
                                                     // 스킬 데미지

            float SkillDamage = 0.0f;

           // SkillDamage = this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAP * 2;
            if (this.name == "Caitlyn") // 
            {
                SkillDamage = 2* this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAD +this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAP ;

            }
            if (this.name == "Vayne") // AD + 대상 최대체력 * 주문력 *0.01;
            {
                SkillDamage = this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAD*1.5f;
                SkillDamage += Target.transform.parent.parent.GetComponent<ChampionIdentity>().ChampFullHP * this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAP*0.001f; // 타겟 최대 체력의 10%
            }
            if (this.name == "Nidalee") // 거리 * AP *0.01
            {
                float dis = Vector3.Distance(this.transform.position, Target.transform.position);
                SkillDamage = dis * this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAP * 0.01f;
            }
            if (this.name == "Twistedfate") // 상대 스턴2초   데미지 : AP
            {
                skill.GetComponent<Bullet>().isTwistedfateSkill = true;
                SkillDamage = this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAP;
            }


            bulletTarget.damage = (int)SkillDamage;
        }
    }
    public void firebullet(GameObject Target)
    {
        atk = atk + Time.deltaTime;
        if (atk >= (attackDelay/ attackSpeed))
        {
            StartCoroutine(Firebullet(Target));
        }
    } 
    IEnumerator Firebullet(GameObject Target)
    {
        GameObject a = Instantiate(Bullet, bulletPos.transform.position, bulletPos.transform.rotation);
        if(this.transform.parent.parent.GetComponent<ChampionIdentity>().isSwordWand) // 총검이면 공격력*0.2만큼 체력 회복
        {
            this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampHP += (int)(this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAD * 0.2f);
        }
        bulletTarget = a.GetComponent<Bullet>();
        bulletTarget.Target = Target.transform;
        float PlusDamage = 0.0f;
        if (isBowSword)
        {
            PlusDamage = Target.transform.parent.parent.GetComponent<ChampionIdentity>().ChampFullHP * 0.03f; // 타겟 최대 체력의 3%
        }
        bulletTarget.damage = this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAD+(int)PlusDamage;  // 평타 데미지
        atk = 0;

        yield return new WaitForSeconds((attackDelay / attackSpeed));
    }

    /*
    IEnumerator skillanim()
    { 
        anim.SetBool("Attack", false); // Attack Stop
        anim.SetBool("Skill", true);  // skill animation start
        Debug.Log("스킬 시작");
        yield return new WaitForSeconds(1.0f);

        skillcount = skillcool;
        // 마나 UI 초기화
        Debug.Log("스킬 종료");
        anim.SetBool("Skill", false);
        anim.SetBool("Attack", true);
    }
    */
    void Update()
    {

    }

}
