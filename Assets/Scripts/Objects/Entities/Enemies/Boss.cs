using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyCtrl
{

    protected override IEnumerator AttackEnemy()
    {
        yield return null;
    }

    protected override void MoveEmemy()
    {

    }

    protected override void SetEnemyDatas()
    {
        StartCoroutine(BossIntroCoroutine(gameObject.transform, new Vector3(0,3,0),2.0f));
    }

    private IEnumerator BossIntroCoroutine(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

}
