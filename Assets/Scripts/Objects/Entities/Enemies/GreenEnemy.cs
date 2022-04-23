using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : EnemyCtrl
{

    protected override IEnumerator AttackEnemy()
    {
        yield return null;
    }

    protected override void MoveEmemy()
    {
        transform.Translate( 3.0f * new Vector2(Mathf.Sin(moveSpeed * Time.time),-1) * Time.deltaTime);
    }

    protected override void SetEnemyDatas()
    {
        moveSpeed = 2.0f;

    }

}
