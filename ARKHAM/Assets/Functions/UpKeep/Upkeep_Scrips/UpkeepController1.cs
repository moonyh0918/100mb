using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpkeepController1 : MonoBehaviour {

    private int Speed1 = 0;
    private int sneak1 = 0;

    private int Speed2 = 0;
    private int sneak2 = 0;

    private int Speed3 = 0;
    private int sneak3 = 0;

    private int Speed4 = 0;
    private int sneak4 = 0;


    private void Start()
    {
        Speed1 = Character.instance.CharacterSpeed;
        sneak1 = Character.instance.CharacterSneak;

        Speed2 = Speed1+1;
        sneak2 = sneak1-1;

        Speed3 = Speed1+2;
        sneak3 = sneak1+2;

        Speed4 = Speed1+3;
        sneak4 = sneak1-3;
    }

    public void SpeedPos1(RectTransform rt)
    {

        if (1 == Mathf.Abs(Speed1 -Character.instance.CharacterSpeed))
        {
            OnClickMove(rt,Speed1,sneak1);
        }
    }
    public void SpeedPos2(RectTransform rt)
    {

        if (1 == Mathf.Abs(Speed2 - Character.instance.CharacterSpeed))
        {
            OnClickMove(rt, Speed2, sneak2);
        }
    }
    public void SpeedPos3(RectTransform rt)
    {

        if (1 == Mathf.Abs(Speed3 - Character.instance.CharacterSpeed))
        {
            OnClickMove(rt, Speed3, sneak3);
        }
    }

    public void SpeedPos4(RectTransform rt)
    {

        if (1 == Mathf.Abs(Speed4 - Character.instance.CharacterSpeed))
        {
            OnClickMove(rt, Speed4, sneak4);
        }
    }

    public void OnClickMove(RectTransform rt, int num1, int num2)
    {

        if (Character.instance.CharacterFocus > 0)
        {
            this.transform.position = rt.transform.position;
            GameObject.FindWithTag("Player").GetComponent<Character>().CharacterFocus--;
            Character.instance.CharacterSpeed = num1;
            Character.instance.CharacterSneak = num2;
        }
    }
}
