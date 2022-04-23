using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemy : EnemyCtrl
{
    protected override IEnumerator AttackEnemy()
    {
        yield return null;
    }

    protected override void MoveEmemy()
    {
        transform.Translate(moveSpeed * Vector2.down * Time.deltaTime);
    }

    protected override void SetEnemyDatas()
    {
        moveSpeed = 2;
    }
}
