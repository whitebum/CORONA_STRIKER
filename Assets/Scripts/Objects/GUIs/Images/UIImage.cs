using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIImage : BaseUI
{
    #region Text UI's Base Properties
    [field: Header("Image UI's Base Data")]
    [field: SerializeField] public Image myImage { get; private set; }
    #endregion

    protected override void InitUIData()
    {
        myImage = GetComponent<Image>();
    }
}
