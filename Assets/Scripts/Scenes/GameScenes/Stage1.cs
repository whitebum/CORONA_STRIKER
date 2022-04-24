using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Stage1 : GameScene
{
    #region
    private void Start()
    {
        StartCoroutine("EnemySpawnPattern4");
    }
    #endregion
    #region
    private IEnumerator EnemySpawnPattern1()
    {
        for (int i = 0; i < 3; ++i)
        {
            var newCencerEnemy = enemySpawner.GetEnemy(EnemyType.CENCER, new Vector3(Random.Range(-8.0f, 8.1f), 6, 0), Quaternion.identity);
            var newVirusEnemy = enemySpawner.GetEnemy(EnemyType.VIRUS, new Vector3(Random.Range(-5.0f, 5.1f), 6, 0), Quaternion.identity);

            yield return new WaitForSeconds(2.0f);

        }

        yield return new WaitForSeconds(2.0f);

    }
    private IEnumerator EnemySpawnPattern2()
    {
        for(int i = -4; i <9; i+= 8)
        {
            var newEnemy = enemySpawner.GetEnemy(EnemyType.GERM, new Vector3(i,6,0), Quaternion.identity);
        }
        for (int i = 0; i < Random.Range(2,5); ++i)
        {
            var newEnemy = enemySpawner.GetEnemy(EnemyType.VIRUS, new Vector3(Random.Range(-3.0f, 3.1f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.6f);
        }

        yield return new WaitForSeconds(3.0f);
    }
    private IEnumerator EnemySpawnPattern3()
    {

        for(int i = -2; i < 3; ++i)
        {

            var newEnemy = enemySpawner.GetEnemy(EnemyType.VIRUS, new Vector3(i, 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.7f);
        }
        yield return new WaitForSeconds(2.0f);
    }
    private IEnumerator EnemySpawnPattern4()
    {
        var newEnemy = enemySpawner.GetEnemy(EnemyType.CENCER, new Vector3(0, 6, 0), Quaternion.identity);
        for(int i = -6; i<7; i += 3)
        {
            var newVirusEnemy = enemySpawner.GetEnemy(EnemyType.VIRUS, new Vector3(i, 6, 0), Quaternion.identity);

        }

        yield return new WaitForSeconds(1.0f);

    }

    #endregion


}
