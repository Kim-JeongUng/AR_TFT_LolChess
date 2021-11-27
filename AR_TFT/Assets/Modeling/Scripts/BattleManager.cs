using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject Bullet;  // 총알 오브젝트
    public Transform bltSpawnPoint;  // 총알 스폰위치
    private float bulletSpeed = 2f;  // 총알 발사 속도

    public Animator anim;  // 공격 애니메이션

    public float attackSpeed = 1f;  // 공격 애니메이션 속도
    public float attackDelay = 1f;  // 공격 딜레이

    public int Skill_trig = 5;  // 스킬을 사용하기 위한 공격 횟수 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        anim.SetFloat("attackSpeed", attackSpeed);  // 공격딜레이, 공격속도에 영향

        if (Input.GetKeyDown(KeyCode.A)) // Attack Start
        {
            anim.SetBool("Attack", true);
        }
        if (Input.GetKeyDown(KeyCode.S)) // Stop
        {
            anim.SetBool("Attack", false);
        }
        if (Input.GetKeyDown(KeyCode.D)) // Death
        {
            anim.SetBool("Death", true);
        }
        if (Input.GetKeyDown(KeyCode.F)) // 다시 살아남
        {
            anim.SetBool("Death", false);
            anim.SetBool("Attack", false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        /*if (anim.GetBool("Attack") == true)
        {
            InvokeRepeating("Fire", 0.5f, 1.0f); //InvokeRepeating : 일정시간후 일정시간마다 반복
//***************** (2021-11-18) 일정시간마다 총알 발사하는것 구현하기 메모
        }*/
    }
    
    void Fire()
    {
        GameObject blt = Instantiate(Bullet);
        blt.transform.position = bltSpawnPoint.transform.position;
        blt.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 1000, ForceMode.Impulse);
    }

    /*
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
