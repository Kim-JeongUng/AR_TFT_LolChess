using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    public GameObject PlayerHP;
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            PlayerHP.GetComponent<RectTransform>().sizeDelta = new Vector2(hp, 100);
    }
}
