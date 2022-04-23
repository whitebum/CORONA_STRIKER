using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityLoad = UnityEngine.SceneManagement.SceneManager;

public sealed class LoadingScene : BaseScene
{
    #region Loading Scene's Expension Properties
    protected override void InitSceneData()
    {
        return;
    }

    protected override void OnStartedScene()
    {
        SoundManager.GetInstance().StopBGM();

        UnityLoad.LoadSceneAsync(SceneManager.nextScene);
    }

    protected override void OnUpdatedScene()
    {
        return;
    }
    #endregion 
}

public sealed class SceneManager
{
    public static string nextScene { get; private set; } = "TitleScene";

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;

        UnityLoad.LoadScene("LoadingScene");
    }
}