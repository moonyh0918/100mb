using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour {

    public Image background;
    public Text stateText;

    public void UpdateStateUI(string state)
    {
        UIInit();

        stateText.text = state;

        background.gameObject.SetActive(true);
        stateText.gameObject.SetActive(true);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color alpha = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(2.0f);

        while (background.color != alpha)
        {
            background.color = Color.Lerp(background.color, alpha, Time.deltaTime * 10.0f);
            stateText.color = Color.Lerp(stateText.color, alpha, Time.deltaTime * 10.0f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void UIInit()
    {
        background.color = new Color(0, 0, 0, 1);
        stateText.color = new Color(0, 0, 0, 1);
    }
}