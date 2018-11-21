using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MythosController : MonoBehaviour {

    MythosDeck mythosDeck;
    Mythos pulledMythos;

    public GameObject eventCard;
    public GameObject mythosPanel;
    public Image mythosImage;

    public GameObject cluePrefab;
    public GameObject clue;

    Vector3 currentPosition;

    public GameObject mainButton;

    public static MythosController instance = null;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mythosDeck = FindObjectOfType<MythosDeck>();
    }


    public void FirstMythos()
    {
        mythosDeck.FindNotRumor();

        FistClue();

        MythosStep();
    }


    //첫 단서 배치
    public void FistClue()
    {
        CreateFirstClue("Woods");
        CreateFirstClue("Historical_Society");
        CreateFirstClue("Science_Building");
        CreateFirstClue("Unvisited_Isle");
        CreateFirstClue("The_Unnamable");
        CreateFirstClue("Indefendence_Square");
        CreateFirstClue("Hibb's_RoadHouse");
        CreateFirstClue("GraveYard");
        CreateFirstClue("Black_Cave");
        CreateFirstClue("The_Wrrch_House");
        CreateFirstClue("Silver_Twilight_Lodge");
    }

    public void CreateFirstClue(string localName)
    {
        clue = Instantiate(cluePrefab, GameObject.Find(localName).GetComponent<Transform>());
        clue.transform.localPosition = new Vector3(0.18f, 2.3f, 0.75f);
    }


    public void MythosStep()
    {
        StartCoroutine("MythosStepCoroutine");
    }

    IEnumerator MythosStepCoroutine()
    {
        mainButton.SetActive(false);
        yield return new WaitForSeconds(0.8f);

        pulledMythos = mythosDeck.DrawCard();

        mythosImage.sprite = Resources.Load<Sprite>("MythosImages/" + pulledMythos.title);
        mythosPanel.SetActive(true);

        currentPosition = eventCard.transform.position;


        // 카드 좌측 상단으로 이동 애니메이션 
        eventCard.GetComponent<Animator>().SetBool("Flip", true);
        yield return new WaitForSeconds(1.5f);

        // 1. 차원문 생성 
        GameStateUI.instance.UpdateStateUI("차원문 생성");
        GateController.instance.OpenGate(pulledMythos.gateLocal.name);
        // 2. 몬스터 생성
        MonsterController.instance.CreateMonster(pulledMythos.gateLocal);

        MaincameraController.instance.ChangeTarget(pulledMythos.gateLocal.gameObject);
        yield return new WaitForSeconds(2.0f);

        // 3. 단서 생성
        GameStateUI.instance.UpdateStateUI("단서 생성");
        clue = Instantiate(cluePrefab, pulledMythos.clueLocal.transform);
        clue.transform.localPosition = new Vector3(0.18f, 2.3f, 0.75f);

        MaincameraController.instance.ChangeTarget(pulledMythos.clueLocal.gameObject);
        yield return new WaitForSeconds(2.0f);
        
        // 4. 몬스터 이동
        IEnumerator coroutine = MonsterController.instance.MoveOneByOne(pulledMythos.whiteSimbol, MonsterMoveController.Color.White);
        yield return StartCoroutine(coroutine);
    
        coroutine = MonsterController.instance.MoveOneByOne(pulledMythos.blackSimbol, MonsterMoveController.Color.Black);
        yield return StartCoroutine(coroutine);

        eventCard.GetComponent<Animator>().SetBool("Flip", false);

        // 5. 이벤트 발생
        pulledMythos.OccurEvent();

        mainButton.SetActive(true);
    }

    public void MythosStateEnd()
    {
        MaincameraController.instance.ChangeTarget(FindObjectOfType<Character>().gameObject);

        eventCard.GetComponent<Animator>().SetBool("Reset", true);
        mythosPanel.SetActive(false);

        //eventCard.transform.position = currentPosition;
        //eventCard.transform.rotation = Quaternion.Euler(0, 180, 0);

        eventCard.transform.GetChild(3).gameObject.SetActive(true);
        eventCard.GetComponent<Animator>().SetBool("Reset", false);

        /*
        eventCard.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        eventCard.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        eventCard.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(260, 380);
        eventCard.transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2(260, 380);
        */
    }
}
