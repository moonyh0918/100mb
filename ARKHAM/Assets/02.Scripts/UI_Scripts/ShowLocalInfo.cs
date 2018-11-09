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
        if (local.local_Id % 10 != 0)
        {
            
            nameText.text = local.name;
            encounterText.text = local.localFunction;
        }
        if(local.local_Id % 10 == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
