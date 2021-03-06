using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Stage0 : GameScene
{
    #region
    private void Start()
    {
        StartCoroutine("EnemySpawnPattern3");
    }
    #endregion
    #region
    private IEnumerator EnemySpawnPattern1()
    {
        for (int i = 0; i < 5; ++i)
        {
            var newEnemy = EnemyFactory.GetInstance().GetEnemy(EnemyType.CENCER, new Vector3(Random.Range(-8.0f, 8.1f), 6, 0), Quaternion.identity);
        }

        yield return new WaitForSeconds(2.0f);

    }
    private IEnumerator EnemySpawnPattern2()
    {
        var newGreenEnemy = EnemyFactory.GetInstance().GetEnemy(EnemyType.VIRUS, new Vector3(0, 6, 0), Quaternion.identity);
        for (int i = 0; i < 4; ++i)
        {
            for (int j = -5; j < 11; j += 10)
            {
                var newYellowEnemy = EnemyFactory.GetInstance().GetEnemy(EnemyType.GERM, new Vector3(j, 6, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(3.0f);
    }
    private IEnumerator EnemySpawnPattern3()
    {
        for (int i = -6; i < 7; i += 6)
        {
            var newEnemy = EnemyFactory.GetInstance().GetEnemy(EnemyType.CENCER, new Vector3(i, 6, 0), Quaternion.identity);
        }
        for (int i = 0; i < 5; ++i)
        {
            var newEnemy = EnemyFactory.GetInstance().GetEnemy(EnemyType.GERM, new Vector3(Random.Range(-8.0f, 8.1f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(2.0f);
    }
    private IEnumerator EnemySpawnPattern4()
    {
        for (int i = 0; i < 2; ++i)
        {
            for (int j = -4; j < 9; j += 8)
            {

                var newEnemy = EnemyFactory.GetInstance().GetEnemy(EnemyType.VIRUS, new Vector3(j, 6, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(1.0f);
    }

    #endregion
}
