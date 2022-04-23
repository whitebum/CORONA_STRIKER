using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameScene : BaseScene
{
    protected override void InitSceneData()
    {
        
    }

    protected override void OnStartedScene()
    {
        SoundManager.GetInstance().PlayBGM($"BGM_{GetType()}", true);
    }

    protected override void OnUpdatedScene()
    {
        
    }
}
