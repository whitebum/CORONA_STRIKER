using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TypeCorona : EnemyCtrl
{
    protected override void SetEnemyDatas()
    {
        entityHP            = 255.0f;
        attackIntervalTime  = 2.0f;
        dropScore           = 5000;

        myBullets   = Resources.LoadAll<BaseBullet>("Prefabs/Bullets/Enemies/TypeViolet");
        myMagazine  = GetComponent<BulletFactory>();

        StartCoroutine(BossIntroCoroutine(gameObject.transform, new Vector3(0, 3, 0), 2.0f));
    }

    protected override IEnumerator AttackEnemy()
    {
        yield return null;
    }

    protected override void MoveEmemy()
    {

    }

    private IEnumerator BossIntroCoroutine(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos  = transform.position;
        var moveTime    = 0.0f;

        while (moveTime < 1)
        {
            moveTime += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, moveTime);
        }

        yield break;
    }
}
