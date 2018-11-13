using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GateController : MonoBehaviour {
    //////////////////  대문자 진짜 디진다 
    public GameObject abyssGate;
    public GameObject celeanoGate;
    public GameObject dementionGate;
    public GameObject dreamLandGate;
    public GameObject greatRaceGate;
    public GameObject plateauOfLengGate;
    public GameObject rlyehGate;
    public GameObject yuggoyhGate;
    public List<GameObject> gateDeck;


    public GameObject closeGatePanel;
    public GameObject sealGatePanel;
    public Image gateImage;

    public Gate characterInGate;

    public GameObject mainButton;

    public static GateController instance = null;

    private void Awake()
    {
        instance = this;
        gateDeck = new List<GameObject>();

        gateDeck.Add(dreamLandGate);
        gateDeck.Add(abyssGate);
        gateDeck.Add(celeanoGate);
        gateDeck.Add(dementionGate);
        gateDeck.Add(greatRaceGate);
        gateDeck.Add(plateauOfLengGate);
        gateDeck.Add(rlyehGate);
        gateDeck.Add(yuggoyhGate);
        gateDeck.Add(abyssGate);
        gateDeck.Add(celeanoGate);
        gateDeck.Add(dementionGate);
        gateDeck.Add(dreamLandGate);
        gateDeck.Add(greatRaceGate);
        gateDeck.Add(plateauOfLengGate);
        gateDeck.Add(rlyehGate);
        gateDeck.Add(yuggoyhGate);

        gateDeck = ShuffleList<GameObject>(gateDeck);
    }

    public void OpenGate(string openloacl)
    {
        GameObject parent = GameObject.Find(openloacl);
        Local sealMarkCheck = parent.GetComponent<Local>();

        if(sealMarkCheck.SealMark)
        {
            Debug.Log("봉인된지역");
        }
        else if(sealMarkCheck.local_Id==Character.instance.currentLocal_Id &&Character.instance.characterid == 0)
        {
            Debug.Log("아만다 사프 능력, 게이트 발생 x");
        }
        else
        {
            if (!sealMarkCheck.gateOpenCheck)
            {
                GameObject gateClone = Instantiate(gateDeck[0], parent.transform);
                gateClone.transform.localPosition = new Vector3(0.16f, 0.12f, 2.0f);

                Local otherWold = gateClone.GetComponent<Gate>().OpenLocal;
                gateClone.name = otherWold.name;


                if (otherWold.allowLocal_Id[0]==0) //돌아오는 장소 연결
                    otherWold.allowLocal_Id[0] = parent.GetComponent<Local>().local_Id;
                else
                    otherWold.allowLocal_Id[1] = parent.GetComponent<Local>().local_Id;

                sealMarkCheck.gateOpenCheck = true;

                FinalBattle.instance.DoomTrackCheck();

                gateDeck.RemoveAt(0);
            }
            else if(sealMarkCheck.gateOpenCheck)
            {
                Debug.Log("차원문 충돌");

            }
        }
       
    }


    //배열 요소 랜덤 배치
    private List<GameObject> ShuffleList<GameObject>(List<GameObject> inputList)
    {
        List<GameObject> randomList = new List<GameObject>();

        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = Random.Range(0, inputList.Count); 
            randomList.Add(inputList[randomIndex]); 
            inputList.RemoveAt(randomIndex);
        }

        return randomList; 
    }


    public void ClosePanel()
    {
        closeGatePanel.SetActive(true);
        gateImage.sprite = characterInGate.gateImage;

    }
    public void SealPanel()
    {
        sealGatePanel.SetActive(true);
    }


    public void CloseGateBtn(int n)
    {
        Debug.Log("게이트 봉인 스텟 선택");
        closeGatePanel.SetActive(false);
        characterInGate.ClosesGateCheck(n);
    }

 
    public void SealGateBten(int n)
    {
        sealGatePanel.SetActive(false);
        characterInGate.SealbuttonCheck(n);
        mainButton.SetActive(true);
    }
    public void SetmainButtonOn()
    {
        mainButton.SetActive(true);
    }
}
