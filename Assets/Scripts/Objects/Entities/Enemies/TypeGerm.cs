using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TypeGerm : EnemyCtrl
{
    protected override void SetEnemyDatas()
    {
        entityHP            = 10.0f;
        moveSpeed           = /*2*/0.0f;
        bulletSpeed         = 5.0f;
        attackIntervalTime  = 2.0f;
        dropScore           = 500;
        painAmount          = 10.0f;

        myBullets   = Resources.LoadAll<BaseBullet>("Prefabs/Bullets/Enemies/TypeYellow");
        myMagazine  = GetComponent<BulletFactory>();
    }

    protected override IEnumerator AttackEnemy()
    {
        yield break;
    }

    protected override void MoveEmemy()
    {
        transform.Translate(moveSpeed * Vector2.down * Time.deltaTime);
    }
}
