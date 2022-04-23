using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RankingScene : BaseScene
{
    protected override void InitSceneData()
    {
        return;
    }

    protected override void OnStartedScene()
    {
        SoundManager.GetInstance().PlayBGM($"BGM_RankingScene", true);
    }

    protected override void OnUpdatedScene()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
