using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowPhase : MonoBehaviour {

    private Text nowPhase;
    public static NowPhase instance = null;

    private void Awake()
    {
        instance = this;
    
    }
    private void Start()
    {
        nowPhase = GetComponent<Text>();
        nowPhase.text = GameManager.instance.gameState.ToString();
    }
    public void PopNowPhase()
    {
            nowPhase.text = GameManager.instance.gameState.ToString();
    }
}
