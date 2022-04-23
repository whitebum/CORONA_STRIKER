using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TitleScene : BaseScene
{
    #region Member Properties
    [field: Header("Title Scene's Base Data")]
    [field: SerializeField] private UIImage         titleLogo       { get; set; }
    [field: SerializeField] private UIFadeText      titleText       { get; set; }
    [field: SerializeField] private UIMainMenu      mainMenu        { get; set; }
    #endregion

    #region Overrided Methods'
    protected override void InitSceneData()
    {
        var canvas = transform.Find("Canvas");

        titleLogo       = canvas.GetComponentInChildren<UIImage>();
        titleText       = canvas.GetComponentInChildren<UIFadeText>();
        mainMenu        = canvas.GetComponentInChildren<UIMainMenu>();

        titleText.isLoop    = true;
        titleText.fadeTime  = 0.5f;
    }

    protected override void OnStartedScene()
    {
        SoundManager.GetInstance().PlayBGM($"BGM_TitleScene", true);

        StartCoroutine(ShowGameIntro());
    }

    protected override void OnUpdatedScene()
    {
        /*스킵 기능은 인트로 코루틴에서도 만들 수 있으나, 그런 경우에는 부자연스러워질 우려가 존재.*/
        /*그런 경우를 회피하고자, 최대한 Update()에서 처리하고자 함.*/
        if (!backgroundAnim.GetBool("isIntroEnd"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();

                backgroundAnim.SetBool("isIntroEnd", true);

                titleLogo.gameObject.SetActive(true);

                titleText.gameObject.SetActive(true);

                return; // 아래 스코프로 넘어가는 현상 방지 (바로 메인 메뉴 패널이 떠버림.)
            }
        }

        if (backgroundAnim.GetBool("isIntroEnd"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                backgroundAnim.StartPlayback();

                titleLogo.gameObject.SetActive(false);
                titleText.gameObject.SetActive(false);
                mainMenu.gameObject.SetActive(true);
            }
        }
    }
    #endregion

    #region Expension Method
    private IEnumerator ShowGameIntro()
    {
        titleLogo.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);

        yield return new WaitForSeconds(backgroundAnim.GetCurrentAnimatorStateInfo(0).length);

        backgroundAnim.SetBool("isIntroEnd", true);

        titleLogo.gameObject.SetActive(true);
        titleText.gameObject.SetActive(true);
    }
    #endregion
}
