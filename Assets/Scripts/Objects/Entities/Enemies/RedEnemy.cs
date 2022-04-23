using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : EnemyCtrl
{
    protected override IEnumerator AttackEnemy()
    {
        yield return null;
    }

    protected override void MoveEmemy()
    {
        transform.Translate(moveSpeed * Vector3.down * Time.deltaTime);
    }

    protected override void SetEnemyDatas()
    {
        moveSpeed = 1.0f;
    }
}
