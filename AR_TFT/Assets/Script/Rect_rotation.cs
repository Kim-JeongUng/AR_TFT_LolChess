using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rect_rotation : MonoBehaviour
{
    private RectTransform RectTransform;
    private float Shake = 0.5f;
    private int ro_z = 20;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*요약 : row_image의 로테이션 값을 주기적으로 변경해 좌우로 조금씩
        흔들거리는 느낌을 주기 위함, 귀여움 요소 +++                    */
        Shake -= Time.deltaTime;
        if(Shake < 0)
        {
            if (ro_z == -20)
                ro_z = 20;
            else if (ro_z == 20)
                ro_z = -20;
            RectTransform.rotation = new Quaternion(0, 0, 1, ro_z);
            Shake = 0.5f;
        }
    }
}