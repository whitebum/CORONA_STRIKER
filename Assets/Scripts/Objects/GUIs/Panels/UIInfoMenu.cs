using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIInfoMenu : BaseUI
{
    #region
    [field: Header("Info Menu's Expension Data")]
    [field: SerializeField] private List<UIActiveButton> buttons { get; set; } = null;
    #endregion

    #region
    protected override void InitUIData()
    {
        buttons = new List<UIActiveButton>(GetComponentsInChildren<UIActiveButton>());

        foreach (var button in buttons)
        {
            button.SetTarget(GameObject.Find(button.name).GetComponent<UIGameInfo>());
        }
    }

    protected override void OnUpdatedUI()
    {
        return;
    }

    protected override void OnDisabledUI()
    {
        return;
    }

    protected override void OnEnabledUI()
    {
        return;
    }
    #endregion
}
