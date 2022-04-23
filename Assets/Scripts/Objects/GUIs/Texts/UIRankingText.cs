using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIRankingText : UIText
{
    [field: SerializeField] public string playerName   { get; set; } = "???";
    [field: SerializeField] public string playerScore  { get; set; } = "000000";

    protected override void OnEnabledUI()
    {
        header.text = playerName;
        tail.text   = playerScore;
    }

    protected override void OnUpdatedUI()
    {
        header.text = playerName;
        tail.text   = playerScore;
    }

    protected override void OnDisabledUI()
    {

    }
}
