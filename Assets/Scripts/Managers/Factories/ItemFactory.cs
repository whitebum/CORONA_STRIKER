using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HPUP,
    LVUP,
    COIN,
    BOOST,
    SHIELD,
    INVINCIBILITY,
}

public sealed class ItemFactory : MonoBehaviour
{
    #region Item Factory's Base Datas
    [Header("Item Factory's Base Datas")]
    [SerializeField] private List<BaseItem> originals   = null;
    [SerializeField] private List<BaseItem> itemBank    = null;
    #endregion

    #region
    private void Awake()
    {
        itemBank    = new List<BaseItem>();
        originals   = new List<BaseItem>(Resources.LoadAll<BaseItem>(""));
    }
    #endregion
}
