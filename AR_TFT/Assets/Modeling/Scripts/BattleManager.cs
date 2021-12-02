using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //public GameObject Bullet;  // 총알 오브젝트
    //public Transform bltSpawnPoint;  // 총알 스폰위치
    //private float bulletSpeed = 2f;  // 총알 발사 속도

    public int HP = 100;  // 체력
    public int ATK = 50;  // 공격력
    public int MATK = 50;  // 주문력
    public float attackSpeed = 0.6f;  // 공격속도

    public int CDR = 1; // 재사용 대기시간, 스킬가속
    public float skillcool = 5f;  // 기본 스킬 쿨타임
    private float skillcount;  // 스킬가속 계산한 실제 쿨타임

    public Animator anim;  // 공격 애니메이션

    public float attackDelay = 1f;  // 공격 딜레이

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        anim.SetFloat("attackSpeed", attackSpeed);  // 공격딜레이, 공격속도에 영향

        if (Input.GetKeyDown(KeyCode.A))  // 전투마커 인식 시 실행
        {
            anim.SetBool("Attack", true); // Attack Start

            skillcool = (1 - CDR / (100 + CDR)) * skillcool; // 스킬가속에 따른 실제 쿨타임 초기화

            skillcount = skillcool - Time.deltaTime;  //스킬 쿨타임

            // skillcool 감소에 따라 마나 게이지 UI 작동
            if (skillcount <= 1.0f)
            {
                //StartCoroutine("skillanim", (1.0f));
                anim.SetBool("Attack", false); // Attack Stop
                anim.SetBool("Skill", true);  // skill animation start
            }
        }

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
        //***************** (2021-11-18) 일정시간마다 총알 발사하는것 구현하기 메모

        /*if (anim.GetBool("Attack") == true)
        {
            InvokeRepeating("Fire", 0.5f, 1.0f); //InvokeRepeating : 일정시간후 일정시간마다 반복
        }*/
    }
    /*
    void Fire()
    {
        GameObject blt = Instantiate(Bullet);
        blt.transform.position = bltSpawnPoint.transform.position;
        blt.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 1000, ForceMode.Impulse);
    }

    
    IEnumerator Fire()
    {
        GameObject blt = Instantiate(Bullet);
        // 공격 애니메이터 실행 시
        yield return new WaitForSeconds(attackDelay);
        blt.transform.position = bltSpawnPoint.transform.position;
        blt.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 1000, ForceMode.Impulse);
        // 애니메이션딜레이에 맞춰서 총알생성
        //Instantiate(Bullet, bltSpawnPoint.transform.position, bltSpawnPoint.transform.rotation);
        // 총알 발사
        //Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }*/
}
