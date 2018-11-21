using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour {

    public GameObject gameStatePanel;
    public Image background;
    public Text stateText;
    public bool skip;

    public static GameStateUI instance;

    private void Awake()
    {
        instance = this;
        gameStatePanel = transform.Find("GameState").gameObject;
        background = gameStatePanel.transform.Find("Background").GetComponent<Image>();
        stateText = gameStatePanel.GetComponentInChildren<Text>();
    }

    public void UpdateStateUI(string state)
    {
        gameStatePanel.SetActive(true);

        skip = true;
        UIInit();

        stateText.text = state;

        background.gameObject.SetActive(true);
        stateText.gameObject.SetActive(true);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color alpha = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(0.6f);

        while (background.color.a >= 0.1f)
        {
            background.color = Color.Lerp(background.color, alpha, Time.deltaTime * 10.0f);
            stateText.color = Color.Lerp(stateText.color, alpha, Time.deltaTime * 10.0f);
            yield return new WaitForSeconds(0.01f);

            if (skip)
            {
                UIInit();
                break;
            }
        }

        gameStatePanel.SetActive(false);
    }

    private void UIInit()
    {
        background.color = new Color(0, 0, 0, 1);
        stateText.color = new Color(1, 1, 1, 1);

        skip = false;
    }
    
}