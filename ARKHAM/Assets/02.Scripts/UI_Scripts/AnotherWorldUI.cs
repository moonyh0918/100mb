using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnotherWorldUI : MonoBehaviour {

    public GameObject otherWorldPanel;
    public Image enterTheWorldImg;

    public static AnotherWorldUI instance;


    private void Awake()
    {
        instance = this;

        otherWorldPanel = transform.GetChild(0).gameObject;
    }


    public void InOtherWorld()
    {
        StartCoroutine(EnterTheWorld());
        otherWorldPanel.SetActive(true);
    }


    public void OutOtherWorld()
    {
        otherWorldPanel.SetActive(false);
    }


    private IEnumerator EnterTheWorld()
    {
        GameStateUI.instance.UpdateStateUI("이 계 진 입");

        enterTheWorldImg.gameObject.SetActive(true);
        enterTheWorldImg.color = new Color(130/255f, 1, 0, 1);

        Color alpha = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(0.6f);

        while (enterTheWorldImg.color.a >= 0.1f)
        {
            enterTheWorldImg.color = Color.Lerp(enterTheWorldImg.color, alpha, Time.deltaTime * 9.0f);
            yield return new WaitForSeconds(0.01f);
        }

        enterTheWorldImg.gameObject.SetActive(false);
    }
}
