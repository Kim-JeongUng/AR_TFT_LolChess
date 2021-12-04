using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCard : MonoBehaviour
{
    // 각 아이템에 붙어 아이템의 기능들을 전달하는 스크립트
    // 한번 붙었으면 자리 고정 (parent 설정, parent 없을 경우만 붙게)
    public bool isattach = false;
    public float attachTimer = 0.0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 추후 <자리를 찾는중> 이면으로 수정해야함
        if (isattach == true && attachTimer < 100)
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
