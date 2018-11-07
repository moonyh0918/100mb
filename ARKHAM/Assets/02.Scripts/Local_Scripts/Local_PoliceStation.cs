using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local_PoliceStation : Local {

    void Awake()
    {
        local_Id = 23;  
        allowLocal_Id = new int[1];
        allowLocal_Id[0] = 20;

        position = transform.position;
        localFunction = "획득한 괴물을 체력의 합이 10 이상이 되게 내거나, 획득한 차원문 2개를 내거나, 획득한 괴물을 체력의 합이 5 이상이 되게 내고 획득한 차원문 1개를 내면 아컴 보안관 부관이 될 수 있습니다. 그렇게 하면 아컴 보안관 부관 카드를 얻습니다.";
    }

    protected override void EventOne()
    {
        Debug.Log(gameObject.name + "EventOne");
    }

    protected override void EventTwo()
    {
        Debug.Log(gameObject.name + "EventTwo");
    }

    protected override void EventThree()
    {
        Debug.Log(gameObject.name + "EventThree");
    }

    protected override void EventFour()
    {

    }

    protected override void EventFive()
    {

    }

    protected override void EventSiz()
    {

    }

    protected override void EventSeven()
    {

    }
}
