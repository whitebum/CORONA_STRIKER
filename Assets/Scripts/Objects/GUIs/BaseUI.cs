using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    #region Unity Messages
    private void Awake()
    {
        gameObject.tag      = "UI";
        gameObject.layer    = LayerMask.NameToLayer("UI");

        InitUIData();
    }

    private void OnEnable()
    {
        OnEnabledUI();
    }

    private void FixedUpdate()
    {
        OnUpdatedUI();
    }

    private void OnDisable()
    {
        OnDisabledUI();
    }
    #endregion

    #region UI's Base Methods
    protected abstract void InitUIData();
    protected abstract void OnEnabledUI();
    protected abstract void OnUpdatedUI();
    protected abstract void OnDisabledUI();
    #endregion
}
