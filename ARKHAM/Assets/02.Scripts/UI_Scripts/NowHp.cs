using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowHp : MonoBehaviour {

    private Text HP;
    private void Start()
    {
        HP = GetComponent<Text>();
        HP.text = Character.instance.characterStamina.ToString();
    }

    /////////////////////////  Ui변경은 Update()에 넣지 말고 변경되는 경우에만 함수호출 하라고   
    void Update()
    {
        HP.text = Character.instance.characterStamina.ToString();

    }
}
