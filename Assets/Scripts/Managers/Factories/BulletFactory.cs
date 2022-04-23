using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BulletFactory : BaseFactory<BaseBullet>
{
    [field: Header("Bullet Factory's Expension Data")]
    [field: SerializeField] public BaseEntity owner { get; private set; } = null;

    protected override void SetFactoryData()
    {
        owner = GetComponent<BaseEntity>();

        originals = owner.myBullets;
        initCount = 30;
    }
}
