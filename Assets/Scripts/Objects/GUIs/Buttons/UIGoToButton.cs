using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIGoToButton : UIButton
{
    #region Go To Button's Expension Data
    [field: SerializeField] public string sceneName { get; set; }
    #endregion

    #region Overrided Method
    protected override void OnClickedButton()
    {
        base.OnClickedButton();

        if (sceneName == "QuitGame")
        {
            Application.Quit();
        }

        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    #endregion
}
