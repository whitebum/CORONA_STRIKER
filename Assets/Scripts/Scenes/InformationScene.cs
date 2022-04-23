using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class InformationScene : BaseScene
{
    [field: Header("")]
    [field: SerializeField] private UIInfoMenu          infoMenu        { get; set; } = null;
    [field: SerializeField] private List<UIGameInfo>    infoPanels      { get; set; } = null;
    [field: SerializeField] private bool                isActivePanel   { get; set; } = false;
    [field: SerializeField] private byte                curPanel        { get; set; } = 1;

    protected override void InitSceneData()
    {
        var canvas = transform.Find("Canvas");

        infoMenu    = canvas.GetComponentInChildren<UIInfoMenu>();
        infoPanels  = new List<UIGameInfo>(canvas.GetComponentsInChildren<UIGameInfo>());
    }

    protected override void OnStartedScene()
    {
        SoundManager.GetInstance().PlayBGM($"BGM_InformationScene", true);

        infoMenu.gameObject.SetActive(true);

        foreach (var panel in infoPanels)
        {
            panel.gameObject.SetActive(false);
        }
    }

    protected override void OnUpdatedScene()
    {
        // 도움말 선택 패널을 제외한 다른 패널이 활성화 되어있는 지 체크합니다.
        if (!isActivePanel)
        {
            infoMenu.gameObject.SetActive(true);

            byte index = 0;
            foreach (var infoPanel in infoPanels)
            { 
                if (infoPanel.gameObject.activeSelf == true)
                {
                    infoMenu.gameObject.SetActive(false);

                    infoPanel.gameObject.SetActive(true);
                    curPanel = index;

                    isActivePanel = true;

                    break;
                }

                index++;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }

        // 현재 다른 패널이 활성화되어 있는 상태라면, 조작키를 통해 패널을 제어할 수 있습니다.
        else
        {
            // 패널을 왼쪽으로 넘기기
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (curPanel > 0)
                {
                    foreach (var infoPanel in infoPanels)
                    {
                        infoPanel.gameObject.SetActive(false);
                    }

                    infoPanels[--curPanel].gameObject.SetActive(true);
                }
            }

            // 패널을 오른쪽으로 넘기기
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (curPanel < infoPanels.Count - 1)
                {
                    foreach (var infoPanel in infoPanels)
                    {
                        infoPanel.gameObject.SetActive(false);
                    }

                    infoPanels[++curPanel].gameObject.SetActive(true);
                }
            }

            // 현재 보고 있는 패널을 끄고, 도움말 패널로 나가기
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                infoMenu.gameObject.SetActive(true);

                foreach (var infoPanel in infoPanels)
                {
                    infoPanel.gameObject.SetActive(false);
                }

                curPanel = 0;
                isActivePanel = false;
            }
        }
    }
}
