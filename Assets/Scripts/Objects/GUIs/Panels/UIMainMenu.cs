using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIMainMenu : BaseUI
{
    [field: Header("Main Menu Panel's Expension Data")]
    [field: SerializeField] private List<UIGoToButton> buttons  { get; set; } = null;

    protected override void InitUIData()
    {
        buttons = new List<UIGoToButton>(GetComponentsInChildren<UIGoToButton>());

        buttons.Find((button) => button.name == "StartGame").sceneName = "Stage0";
        buttons.Find((button) => button.name == "GameInformation").sceneName = "InformationScene";
        buttons.Find((button) => button.name == "GoToRanking").sceneName = "RankingScene";
        buttons.Find((button) => button.name == "QuitGame").sceneName = "QuitGame";
    }

    protected override void OnEnabledUI()
    {
        
    }

    protected override void OnUpdatedUI()
    {
        
    }
    
    protected override void OnDisabledUI()
    {
        
    }
}
