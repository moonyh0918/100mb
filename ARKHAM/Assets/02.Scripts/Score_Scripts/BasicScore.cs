using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicScore : ScoreSetting
{
    [Range(1, 50)]
    [SerializeField]
    readonly float speed = 0.05f;
    RectTransform NoticeRect;

    public Text basicscore;
    public void Setting()
    {
        basicscore = GetComponent<Text>();
        basicscore.text = Scoreontroller.instanse.BasicScore().ToString();
        

        StartCoroutine(IntroEffect());
    }

    
    IEnumerator IntroEffect()
    {
        
  
        NoticeRect = basicscore.GetComponent<RectTransform>();
        Vector2 size = NoticeRect.localScale;
        Debug.Log("코루틴 실행 :" + size.x + size.y);
        while (size.x > 1)
        {
            size.x -= speed;
            size.y -= speed;
            // 1이하로 된경우 1로 만든다.
            if (size.x < 1)
            {
                size.x = 1;
                size.y = 1;
            }
            NoticeRect.localScale = size;
            yield return new WaitForEndOfFrame();
        }
    }
}
