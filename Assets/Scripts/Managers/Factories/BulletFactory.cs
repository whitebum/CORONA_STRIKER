using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    NORMAL,
    BAZIER,
}

public sealed class BulletFactory : MonoBehaviour
{
    #region Bullet Factory's Managed Datas
    [field: Header("Bullet Factory's Managed Datas")]
    [field: SerializeField] public BaseEntity owner { get; private set; } =  null;

    [SerializeField] private List<BaseBullet> originals     = null;
    [SerializeField] private List<BaseBullet> bulletBank    = null;
    #endregion

    #region Unity Message
    private void Awake()
    {
        owner       = GetComponent<BaseEntity>();

        originals   = new List<BaseBullet>(owner.myBullets);
        bulletBank  = new List<BaseBullet>();

        Debug.Log("asd");

        foreach (var original in originals)
        {
            var storage = new GameObject($"{original.name}'s Storage").transform;

            storage.transform.SetParent(transform);

            for (byte count = 0; count < 30; ++count)
            {
                var newBullet = Instantiate(original);

                newBullet.name = original.name;
                newBullet.home = this;
                newBullet.transform.SetParent(storage);
                newBullet.transform.position = transform.position;
                newBullet.transform.rotation = transform.rotation;
                newBullet.gameObject.SetActive(false);

                bulletBank.Add(newBullet);
            }
        }
    }
    #endregion

    #region Bullet Factory's Methods
    public BaseBullet GetBullet(BulletType type, Vector3 spawnPos, float scale, Quaternion rotate)
    {
        var nowBullet = GetBulletInBank(type);

        if (!nowBullet)
        {
            nowBullet = GetBulletInOriginal(type);

            nowBullet.home = this;
            nowBullet.transform.position    = spawnPos;
            nowBullet.transform.localScale *= scale;
            nowBullet.transform.rotation    = rotate;
        }

        else
        {
            bulletBank.Remove(nowBullet);

            nowBullet.transform.SetParent(null);
            nowBullet.transform.position    = spawnPos;
            nowBullet.transform.localScale *= scale;
            nowBullet.transform.rotation    = rotate;
            nowBullet.gameObject.SetActive(true);
        }

        return nowBullet;
    }

    public void ReturnBullet(BaseBullet usedBullet)
    {
        usedBullet.transform.SetParent(transform.Find($"{usedBullet.name}'s Storage"));
        usedBullet.transform.position   = transform.position;
        usedBullet.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        usedBullet.transform.rotation   = Quaternion.identity;
        usedBullet.gameObject.SetActive(false);

        bulletBank.Add(usedBullet);
    }

    private BaseBullet GetBulletInOriginal(BulletType type) => type switch
    {
        BulletType.NORMAL => originals.Find((bullet) => bullet is BaseBullet),
        BulletType.BAZIER => originals.Find((bullet) => bullet is BaseBazierBullet),
        _ => null,
    };

    private BaseBullet GetBulletInBank(BulletType type) => type switch
    {
        BulletType.NORMAL => bulletBank.Find((bullet) => bullet is BaseBullet),
        BulletType.BAZIER => bulletBank.Find((bullet) => bullet is BaseBazierBullet),
        _ => null,
    };
    #endregion
}
