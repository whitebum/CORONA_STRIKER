using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BulletFactory : MonoBehaviour
{
    [field: Header("Bullet Factory's Expension Data")]
    [SerializeField] private    List<BaseBullet> originals  = null;
    [SerializeField] protected  List<BaseBullet> bulletBank = null;

    [field: SerializeField] public BaseEntity owner { get; private set; } =  null;

    private void Awake()
    {
        owner       = GetComponent<BaseEntity>();

        originals   = new List<BaseBullet>(owner.myBullets);
        bulletBank  = new List<BaseBullet>();

        foreach (var original in originals)
        {
            var storage = new GameObject($"{original.name} Storage").transform;

            storage.transform.SetParent(transform);

            for (byte count = 0; count < 30; ++count)
            {
                var newBullet = Instantiate(original);

                newBullet.home = this;
                newBullet.transform.SetParent(storage);
                newBullet.transform.position = transform.position;
                newBullet.transform.rotation = transform.rotation;
                newBullet.gameObject.SetActive(false);

                bulletBank.Add(newBullet);
            }
        }
    }

    public BaseBullet GetBullet(bool isBazier, Vector3 spawnPos, float scale, Quaternion rotate)
    {
        var nowBullet = isBazier ? bulletBank.Find((b) => b is BaseBazierBullet) : bulletBank.Find((b) => b is BaseBullet);

        if (bulletBank.Count <= 0 || !nowBullet)
        {
            var newBullet = Instantiate(originals.Find((b) => b == nowBullet));

            newBullet.home = this;
            newBullet.transform.position    = spawnPos;
            newBullet.transform.localScale *= scale;
            newBullet.transform.rotation    = rotate;

            return newBullet;
        }

        else
        {
            bulletBank.Remove(nowBullet);

            nowBullet.transform.SetParent(null);
            nowBullet.transform.position    = spawnPos;
            nowBullet.transform.localScale *= scale;
            nowBullet.transform.rotation    = rotate;
            nowBullet.gameObject.SetActive(true);

            return nowBullet;
        }
    }

    public void ReturnObject(BaseBullet usedBullet)
    {
        usedBullet.transform.SetParent(transform);
        usedBullet.transform.position   = transform.position;
        usedBullet.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        usedBullet.transform.rotation   = Quaternion.identity;
        usedBullet.gameObject.SetActive(false);

        bulletBank.Add(usedBullet);
    }
}
