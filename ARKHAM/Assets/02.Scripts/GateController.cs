using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GateController : MonoBehaviour {
    //////////////////  대문자 진짜 디진다 
    public GameObject AbyssGate;
    public GameObject CeleanoGate;
    public GameObject DementionGate;
    public GameObject DreamLandGate;
    public GameObject GreatRaceGate;
    public GameObject PlateauOfLengGate;
    public GameObject RlyehGate;
    public GameObject YuggoyhGate;
    public List<GameObject> GateDeck;


    public GameObject CloseGatePanel;
    public GameObject SealGatePanel;
    public Image GateImage;

    public Gate CharacterInGate;

    public GameObject MainButton;

    public static GateController instance = null;

    private void Awake()
    {
        instance = this;
        GateDeck = new List<GameObject>();

        GateDeck.Add(DreamLandGate);
        GateDeck.Add(AbyssGate);
        GateDeck.Add(CeleanoGate);
        GateDeck.Add(DementionGate);
        GateDeck.Add(GreatRaceGate);
        GateDeck.Add(PlateauOfLengGate);
        GateDeck.Add(RlyehGate);
        GateDeck.Add(YuggoyhGate);
        GateDeck.Add(AbyssGate);
        GateDeck.Add(CeleanoGate);
        GateDeck.Add(DementionGate);
        GateDeck.Add(DreamLandGate);
        GateDeck.Add(GreatRaceGate);
        GateDeck.Add(PlateauOfLengGate);
        GateDeck.Add(RlyehGate);
        GateDeck.Add(YuggoyhGate);

        GateDeck = ShuffleList<GameObject>(GateDeck);
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
                GameObject gateClone = Instantiate(GateDeck[0], parent.transform);
                gateClone.transform.localPosition = new Vector3(0.16f, 0.12f, 2.0f);

                Local otherWold = gateClone.GetComponent<Gate>().OpenLocal;
                gateClone.name = otherWold.name;


                if (otherWold.allowLocal_Id[0]==0) //돌아오는 장소 연결
                    otherWold.allowLocal_Id[0] = parent.GetComponent<Local>().local_Id;
                else
                    otherWold.allowLocal_Id[1] = parent.GetComponent<Local>().local_Id;

                sealMarkCheck.gateOpenCheck = true;

                FinalBattle.instance.DoomTrackCheck();

                GateDeck.RemoveAt(0);
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
        CloseGatePanel.SetActive(true);
        GateImage.sprite = CharacterInGate.GateImage;

    }
    public void SealPanel()
    {
        SealGatePanel.SetActive(true);
    }


    public void CloseGateBtn(int n)
    {
        Debug.Log("게이트 봉인 스텟 선택");
        CloseGatePanel.SetActive(false);
        CharacterInGate.ClosesGateCheck(n);
    }

 
    public void SealGateBten(int n)
    {
        SealGatePanel.SetActive(false);
        CharacterInGate.SealbuttonCheck(n);
        MainButton.SetActive(true);
    }
    public void SetMainButtonOn()
    {
        MainButton.SetActive(true);
    }
}
