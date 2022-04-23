using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GreenEnemy : EnemyCtrl
{
    protected override void SetEnemyDatas()
    {
        entityHP    = 10.0f;
        moveSpeed   = 2.0f;
        bulletSpeed = 30.0f;

        myBullets = Resources.LoadAll<EnemyBullet>("Prefabs/Bullets/Enemy/GreenBullet");
    }

    protected override IEnumerator AttackEnemy()
    {
        yield break;
    }

    protected override void MoveEmemy()
    {
        //transform.Translate(moveSpeed *  * Time.deltaTime);
    }
}
