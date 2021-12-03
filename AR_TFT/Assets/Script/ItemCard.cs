using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCard : MonoBehaviour
{
    // 각 아이템에 붙어 아이템의 기능들을 전달하는 스크립트
    // 한번 붙었으면 자리 고정 (parent 설정, parent 없을 경우만 붙게)
    public bool isattach = false;
    public float attachTimer;
    void Awake()
    {
        isattach = true;
        attachTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            if (isattach == true)
            {
                attachTimer += Time.deltaTime;
            }
        }
        // 추후 <자리를 찾는중> 이면으로 수정해야함
        
        if (GetComponent<MyDefaultTrackableEventHandler>().isAttach)
        {
            isattach = true;
        }
        // 추후 <원래 자리에서 이탈하면> 으로 수정해야함
        if (!GetComponent<MyDefaultTrackableEventHandler>().isAttach)
        {
            isattach = false;
            attachTimer = 0.0f;
        }
    }
}
