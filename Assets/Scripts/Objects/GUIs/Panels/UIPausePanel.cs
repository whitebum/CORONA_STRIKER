using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIPausePanel : BaseUI
{
    protected override void InitUIData()
    {
        return;
    }

    protected override void OnEnabledUI()
    {
        Time.timeScale = 0.0f;
        SoundManager.GetInstance().PauseBGM();
    }

    protected override void OnUpdatedUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnDisabledUI()
    {
        Time.timeScale = 1.0f;
        SoundManager.GetInstance().PlayBGM();
    }
}
