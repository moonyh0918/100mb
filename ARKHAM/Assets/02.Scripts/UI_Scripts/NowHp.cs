using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NowHp : MonoBehaviour
{

    private Text hp;
    public static NowHp instance = null;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        hp = GetComponent<Text>();
        hp.text = Character.instance.CharacterStamina.ToString();
    }
    public void PopHpUi()
    {
        hp.text = Character.instance.CharacterStamina.ToString();

    }
}
