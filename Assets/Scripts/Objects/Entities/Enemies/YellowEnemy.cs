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
        transform.Translate(moveSpeed * new Vector2(Random.Range(-3, 3),-1.0f) * Time.deltaTime);
    }

    protected override void SetEnemyDatas()
    {
        moveSpeed = 2;
    }
}
