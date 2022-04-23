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
        // ���� ���� �г��� ������ �ٸ� �г��� Ȱ��ȭ �Ǿ��ִ� �� üũ�մϴ�.
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

        // ���� �ٸ� �г��� Ȱ��ȭ�Ǿ� �ִ� ���¶��, ����Ű�� ���� �г��� ������ �� �ֽ��ϴ�.
        else
        {
            // �г��� �������� �ѱ��
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

            // �г��� ���������� �ѱ��
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

            // ���� ���� �ִ� �г��� ����, ���� �гη� ������
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
