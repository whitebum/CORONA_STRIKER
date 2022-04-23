using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIText : BaseUI
{
    #region Text UI's Base Properties
    [field: Header("Text UI's Base Datas")]
    [field: SerializeField] protected Text header   { get; private set; }
    [field: SerializeField] protected Text tail     { get; private set; }
    #endregion

    protected override void InitUIData()
    {
        var texts = GetComponentsInChildren<Text>();

        header  = texts[0];
        tail    = texts[1];
    }
}
