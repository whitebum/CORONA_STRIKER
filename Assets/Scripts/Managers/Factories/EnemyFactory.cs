using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyFactory : BaseFactory<EnemyCtrl>
{
    protected override void SetFactoryData()
    {
        originals = Resources.LoadAll<EnemyCtrl>("");
        initCount = 30;
    }
}
