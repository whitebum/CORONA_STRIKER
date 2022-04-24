using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TypeVirus : EnemyCtrl
{
    protected override void SetEnemyDatas()
    {
        entityHP            = 10.0f;
        moveSpeed           = 2.0f;
        bulletSpeed         = 5.0f;
        attackIntervalTime  = 0.5f;
        dropScore           = 500;
        painAmount          = 10.0f;

        myBullets   = Resources.LoadAll<BaseBullet>("Prefabs/Bullets/Enemies/TypeGreen");
        myMagazine  = GetComponent<BulletFactory>();
    }

    protected override IEnumerator AttackEnemy()
    {
        var lookAts = new float[] { 140.0f, 180.0f, 220.0f };

        foreach (var lookAt in lookAts)
        {
            myMagazine.GetBullet(BulletType.NORMAL, transform.position, 2.0f, Quaternion.Euler(0.0f, 0.0f, lookAt));
        }
        
        yield return new WaitForSeconds(attackIntervalTime);
        StartCoroutine(AttackEnemy());
        yield break;
    }

    protected override void MoveEmemy()
    {
        transform.Translate(3.0f * new Vector2(Mathf.Sin(moveSpeed * Time.time), -1) * Time.deltaTime);
    }
}
