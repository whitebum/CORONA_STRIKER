using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIRankingPanel : BaseUI
{
    [field: Header("")]
    [field: SerializeField] private List<UIRankingText> rankingTexts { get; set; } = null;

    protected override void InitUIData()
    {
        rankingTexts = new List<UIRankingText>(GetComponentsInChildren<UIRankingText>());

        byte index = 0;
        foreach (var rankingText in rankingTexts)
        {
            rankingText.playerName  = RankingManager.GetInstance().rankingBank[index].playerName;
            rankingText.playerScore = RankingManager.GetInstance().rankingBank[index].playerScore;

            ++index;
        }
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
