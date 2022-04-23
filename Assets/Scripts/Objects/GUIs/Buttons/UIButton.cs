using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIButton : BaseUI
{
    #region
    [field: Header("Button UI's Base Data")]
    [field: SerializeField] public Button myButton { get; private set; } = null;
    #endregion

    #region
    protected override void InitUIData()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClickedButton);
    }

    protected override void OnEnabledUI()
    {
        return;
    }

    protected override void OnUpdatedUI()
    {
        return;
    }

    protected override void OnDisabledUI()
    {
        return;
    }
    #endregion

    protected virtual void OnClickedButton()
    {
        // SoundManager.GetInstance().PlaySFX("SFX_MenuClick");
    }
}
