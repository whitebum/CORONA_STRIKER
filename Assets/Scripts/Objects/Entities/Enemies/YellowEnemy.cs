using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class YellowEnemy : EnemyCtrl
{
    protected override void SetEnemyDatas()
    {
        entityHP = 10.0f;
        moveSpeed = 2.0f;
        bulletSpeed = 30.0f;

        myBullets = Resources.LoadAll<BaseBullet>("Prefabs/Bullets/Enemies/YellowBullet");
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
