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

    public float ChampSkillTime = 5.0f; //스킬사용 대기시간
    public float skillcount = 0;

    public Animator anim;  // 공격 애니메이션

    public GameObject target;
    public float attackDelay = 1.917f;  // 공격 애니메이션 딜레이******** 챔피언마다 값 교체 필요함
    public float skillDelay = 1.25f;  // 스킬 애니메이션 딜레이 ********* 챔피언마다 값 교체 필요함

    public ParticleSystem skillEfect;

    public GameObject Bullet;
    public Transform bulletPos;

    public GameObject skill_Bullet;

    Bullet bulletTarget;


    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        attackSpeed = this.transform.parent.parent.GetComponent<ChampionIdentity>().ChampAS;
        anim.SetFloat("attackSpeed", attackSpeed);  // 공격딜레이, 공격속도에 영향

        if (Input.GetKeyDown(KeyCode.A))  // 전투마커 인식 시 실행
        {
            anim.SetBool("Attack", true); // Attack Start
            StartCoroutine(Firebullet(target));

        }
        if (anim.GetBool("Attack") == true)
        {
            skillcount = skillcount + Time.deltaTime;  //스킬 쿨타임
            // skillcool 감소에 따라 마나 게이지 UI 작동
            if (skillcount >= ChampSkillTime)
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
        GameObject skill = Instantiate(skill_Bullet, bulletPos.transform.position, bulletPos.transform.rotation);
        bulletTarget = skill.GetComponent<Bullet>();
        bulletTarget.Target = Target.transform;  // 가장 가까운 상대 챔피언으로 교체 필요함-----------------
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
        bulletTarget = a.GetComponent<Bullet>();
        bulletTarget.Target = Target.transform;  // 가장 가까운 상대 챔피언으로 교체 필요함-----------------
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
