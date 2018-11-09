using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaincameraController : MonoBehaviour
{
    public float zoom = 1;
    public float speed = 10;
    public Vector3 MouseStart;
    public GameObject target;

    private Character character;
    private Vector3 offset;

    public static MaincameraController instance = null;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        target = GameObject.Find("character");

        character = FindObjectOfType<Character>();

        offset = transform.position - target.transform.position;
    }

    // LateUpdate에서 GetComponent<> 사용 x, 변수에 저장해서 쓰는 형식으로 바꿈 
    void LateUpdate()
    {
        // 이동 단계 때 캐릭터를 따라다님
        if (character.characterState == Character.State.MOVE && GameManager.instance.gameState !=GameManager.GameState.finalbattle)
            transform.position = target.transform.position + offset;
        // 신화 단계 때 이동중인 몬스터를 따라다님
        else if (GameManager.instance.gameState == GameManager.GameState.Mythos && GameManager.instance.gameState != GameManager.GameState.finalbattle)
        {
            transform.position = target.transform.position + offset;
        }


        CameraMoveByDrag();
    }

    // 타켓이 특정 장소로 한번에 이동한 경우 카메라도 한번에 타켓에게 이동 
    public void SetPosition( )
    {
        Debug.Log("SetPosition" + target.transform.position);
        transform.position = target.transform.position + offset;
    }

    // 따라갈 타켓 변경
    public void ChangeTarget(GameObject _target)
    {
        target = _target;
    }


    private void CameraMoveByDrag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MouseStart = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.y);
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
            MouseStart.y = transform.position.y;

        }
        else if (Input.GetMouseButton(1))
        {
            var MouseMove = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.y);
            MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
            MouseMove.y = transform.position.y;
            transform.position = transform.position - (MouseMove - MouseStart);

        }

        // 마우스 휠에 따른 카메라 줌인, 줌 아웃 기능 비활성화
        /*
        else
        {
            float keyHorizontal = Input.GetAxis("Horizontal");

            float keyVertical = Input.GetAxis("Vertical");

            transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * keyHorizontal, Space.World);

            transform.Translate(Vector3.up * speed * Time.smoothDeltaTime * keyVertical, Space.World);

            transform.position -= (transform.position - target.transform.position) * Input.GetAxis("Mouse ScrollWheel") * zoom;

        }*/
    }


}