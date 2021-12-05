using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    public GameObject PlayerHP_bar1;
    public GameObject PlayerHP_bar2;
    public int PlayerHP1, PlayerHP2;
    void Start()
    {
    }

    void Update()
    {
        PlayerHP_bar1.GetComponent<RectTransform>().sizeDelta = new Vector2(PlayerHP1, 100);
        PlayerHP_bar2.GetComponent<RectTransform>().sizeDelta = new Vector2(PlayerHP2, 100);
    }
}
