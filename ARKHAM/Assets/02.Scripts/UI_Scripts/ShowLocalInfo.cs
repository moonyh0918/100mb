using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLocalInfo : MonoBehaviour {

    public Text nameText;
    public Text encounterText;

    public void Awake()
    {
        nameText = transform.Find("Name").GetComponentInChildren<Text>();
        encounterText = transform.Find("Encounter").GetComponentInChildren<Text>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateInfoUI(Local local)
    {
        // 마우스커서가 Street에 위치할 경우는 InfoUI를 띄워줄 필요 없음
        if (local.local_Id % 10 != 0)
        {
            nameText.text = local.name;
            encounterText.text = local.localFunction;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
