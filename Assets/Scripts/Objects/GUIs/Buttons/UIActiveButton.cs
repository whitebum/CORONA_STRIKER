using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIActiveButton : UIButton
{
    #region Active Button's Expension Data
    [field: Header("Active Button's Expension Data")]
    [field: SerializeField] public BaseUI target { get; private set; } = null;
    #endregion

    #region Overrided Method
    protected override void OnClickedButton()
    {
        target.gameObject.SetActive(true);
    }
    #endregion

    #region Method
    public void SetTarget(BaseUI UI)
    {
        target = UI;
    }
    #endregion
}
