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

public sealed class ItemFactory : BaseManager<EnemyFactory>
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

        foreach (var original in originals)
        {
            var storage = new GameObject($"{original.name}'s Storage").transform;

            storage.transform.SetParent(transform);

            for (byte count = 0; count < 5; ++count)
            {
                var newItem = Instantiate(original);

                newItem.name = original.name;
                newItem.home = this;
                newItem.transform.SetParent(storage);
                newItem.transform.position = transform.position;
                newItem.gameObject.SetActive(false);

                itemBank.Add(newItem);
            }
        }
    }
    #endregion

    #region
    public BaseItem GetItem(ItemType type, Vector3 spawnPos)
    {
        var nowEnemy = GetItemInBank(type);

        if (!nowEnemy)
        {
            nowEnemy = Instantiate(GetItemInOriginals(type));

            nowEnemy.home = this;
            nowEnemy.transform.position = spawnPos;
        }

        else
        {
            itemBank.Remove(nowEnemy);

            nowEnemy.transform.SetParent(null);
            nowEnemy.transform.position = spawnPos;
            nowEnemy.gameObject.SetActive(true);
        }

        return nowEnemy;
    }

    public void ReturnItem(BaseItem usedItem)
    {
        usedItem.transform.SetParent(transform.Find($"{usedItem.name}'s Storage"));
        usedItem.transform.position = transform.position;
        usedItem.gameObject.SetActive(false);

        itemBank.Add(usedItem);
    }

    private BaseItem GetItemInOriginals(ItemType type) => type switch
    {
        ItemType.HPUP => originals.Find((item) => item is TypeHpUp),
        ItemType.LVUP => originals.Find((item) => item is TypeLvUp),
        ItemType.COIN => originals.Find((item) => item is TypeCoin),
        ItemType.BOOST => originals.Find((item) => item is TypeBoost),
        ItemType.SHIELD => originals.Find((item) => item is TypeShield),
        ItemType.INVINCIBILITY => originals.Find((item) => item is TypeInvincibility),
        _ => null,
    };

    private BaseItem GetItemInBank(ItemType type) => type switch
    {
        ItemType.HPUP => itemBank.Find((item) => item is TypeHpUp),
        ItemType.LVUP => itemBank.Find((item) => item is TypeLvUp),
        ItemType.COIN => itemBank.Find((item) => item is TypeCoin),
        ItemType.BOOST => itemBank.Find((item) => item is TypeBoost),
        ItemType.SHIELD => itemBank.Find((item) => item is TypeShield),
        ItemType.INVINCIBILITY => itemBank.Find((item) => item is TypeInvincibility),
        _ => null,
    };
    #endregion
}
