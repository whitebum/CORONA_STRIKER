using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RedEnemy : EnemyCtrl
{
    protected override void SetEnemyDatas()
    {
        entityHP    = 10.0f;
        moveSpeed   = 2.0f;
        bulletSpeed = 30.0f;

        myBullets   = Resources.LoadAll<EnemyBullet>("Prefabs/Bullets/Enemy/RedBullet");
    }

    protected override IEnumerator AttackEnemy()
    {
        var newBullet       = myMagazine.GetBullet(BulletType.BAZIER, transform.position, 1.0f, Quaternion.identity) as BaseBazierBullet;
        newBullet.myTarget  = myTarget;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("AttackEnemy");
    }

    protected override void MoveEmemy()
    {
        transform.Translate(moveSpeed * Vector2.down * Time.deltaTime);
    }
}
