using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIFadeText : BaseUI
{
    #region
    [field: Header("Fade Text's Expension Datas")]
    [field: SerializeField] public Text     myText      { get; private set; }       = null;
    [field: SerializeField] public bool     isLoop      { get; set; }               = false;
    [field: SerializeField] public float    fadeTime    { get; set; }               = 0.0f;
    [field: SerializeField] public float    waitTime    { get; set; }               = 0.0f;
    #endregion

    #region
    protected override void InitUIData()
    {
        myText = GetComponent<Text>();
    }

    protected override void OnEnabledUI()
    {
        StartCoroutine(FadeRoutineText());
    }

    protected override void OnUpdatedUI()
    {
        return;
    }

    protected override void OnDisabledUI()
    {
        StopCoroutine(FadeRoutineText());
    }
    #endregion

    #region
    private IEnumerator FadeRoutineText()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        if (isLoop)
        {
            while (true)
            {
                yield return StartCoroutine(FadeOutText());
                yield return StartCoroutine(FadeInText());
            }
        }

        else
        {
            yield return StartCoroutine(FadeOutText());
            yield return StartCoroutine(FadeInText());

            gameObject.SetActive(false);
        }
    }

    private IEnumerator FadeInText()
    {
        myText.color = new Color(myText.color.r,
                                 myText.color.g,
                                 myText.color.b, 0);

        while (myText.color.a < 1.0f)
        {
            myText.color = new Color(myText.color.r,
                                     myText.color.g,
                                     myText.color.b,
                                     myText.color.a + (Time.deltaTime / fadeTime));
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        yield break;
    }

    private IEnumerator FadeOutText()
    {
        myText.color = new Color(myText.color.r,
                                 myText.color.g,
                                 myText.color.b, 1);

        while (myText.color.a > 0.0f)
        {
            myText.color = new Color(myText.color.r,
                                     myText.color.g,
                                     myText.color.b,
                                     myText.color.a - (Time.deltaTime / fadeTime));
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        yield break;
    }
    #endregion
}
