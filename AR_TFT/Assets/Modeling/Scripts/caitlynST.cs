using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caitlynST : MonoBehaviour
{
    public int HP;  // 체력
    public int ATK;  // 공격력
    public int MATK;  // 주문력
    public float attackSpeed;  // 공격속도
    public int CDR; // 재사용 대기시간 감소

    public Animator anim;  // 애니메이션

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        anim.SetFloat("attackSpeed", attackSpeed);  // 공격딜레이, 공격속도에 영향
    }

    void Update()
    {
        
    }
}
