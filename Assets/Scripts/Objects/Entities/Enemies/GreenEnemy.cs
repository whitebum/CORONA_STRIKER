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
        for (byte count = 0; count < 6; ++count)
        {
            var newBullet       = myMagazine.GetBullet(true, transform.position, 1.0f, Quaternion.identity) as BaseBazierBullet;
            newBullet.myTarget  = myTarget;

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1.0f);

        StartCoroutine("AttackEnemy");
    }

    protected override void MoveEmemy()
    {
        //transform.Translate(moveSpeed *  * Time.deltaTime);
    }
}
