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
        /*��ŵ ����� ��Ʈ�� �ڷ�ƾ������ ���� �� ������, �׷� ��쿡�� ���ڿ��������� ����� ����.*/
        /*�׷� ��츦 ȸ���ϰ���, �ִ��� Update()���� ó���ϰ��� ��.*/
        if (!backgroundAnim.GetBool("isIntroEnd"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();

                backgroundAnim.SetBool("isIntroEnd", true);

                titleLogo.gameObject.SetActive(true);

                titleText.gameObject.SetActive(true);

                return; // �Ʒ� �������� �Ѿ�� ���� ���� (�ٷ� ���� �޴� �г��� ������.)
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
