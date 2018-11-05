using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGateInfo : MonoBehaviour {

    public Text nameText;
    public Text sealLevelText;
    public Text simbolText;

    public void Awake()
    {
        nameText = transform.Find("Name").GetComponentInChildren<Text>();
        sealLevelText = transform.Find("SealLevel").GetComponentInChildren<Text>();
        simbolText = transform.Find("Simbol").GetComponentInChildren<Text>();

    }

    public void UpdateInfoUI(Gate gate)
    {
        nameText.text = gate.name;
        sealLevelText.text = "봉인 수정치 : " + gate.Modifier;
        simbolText.text = "문양 : " + gate.GateSimbol;
    }
}
