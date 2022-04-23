using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RedEnemy : EnemyCtrl
{
    protected override void SetEnemyDatas()
    {
        entityHP    = 10.0f;
        moveSpeed   = 10.0f;
        bulletSpeed = 30.0f;

        myBullets   = Resources.LoadAll<PlayerBullet>("Prefabs/Bullets/Enemy/RedBullet");
    }

    protected override IEnumerator AttackEnemy()
    {



        yield return null;
    }

    protected override void MoveEmemy()
    {
        transform.Translate(moveSpeed * Vector2.down * Time.deltaTime);
    }
}
