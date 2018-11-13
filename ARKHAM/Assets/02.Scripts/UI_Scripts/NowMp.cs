using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowMp : MonoBehaviour
{


    private Text mp;
    public static NowMp instance = null;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mp = GetComponent<Text>();
        mp.text = Character.instance.CharacterStamina.ToString();
    }
    public void PopMpUi()
    {
        mp.text = Character.instance.CharacterStamina.ToString();

    }
}
