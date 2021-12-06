using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_bar : MonoBehaviour
{
    public GameObject red, black;
    public float full_health = 300;//풀 체력이 300이라면
    public float now_health = 150;//현재 체력
    public float per;//비율 계산용
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        now_health -= 0.1f;
        if(now_health <= 0)
        {
            now_health = 300;
        }
        per = now_health / full_health;

        red.transform.localPosition = new Vector3(-50+(50*per), 150, 0);
        red.transform.localScale = new Vector3(13, 50*per, 13);
        black.transform.localPosition = new Vector3((50*per),150,0);
        black.transform.localScale = new Vector3(13, -50+(50 * per), 13);
    }
}
