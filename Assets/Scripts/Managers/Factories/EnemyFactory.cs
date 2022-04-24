using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    VIRUS,
    CENCER,
    GERM,
    CORONA,
}

public sealed class EnemyFactory : MonoBehaviour
{
    #region Enemy Factory's Managed Datas
    [field: Header("Enemy Factory's Managed Datas")]
    [SerializeField] private List<EnemyCtrl> originals = null;
    [SerializeField] private List<EnemyCtrl> enemyBank = null;
    #endregion

    #region Unity Message
    private void Awake()
    {
        enemyBank   = new List<EnemyCtrl>();
        originals   = new List<EnemyCtrl>(Resources.LoadAll<EnemyCtrl>(""));

        foreach (var original in originals)
        {
            var storage = new GameObject($"{original.name}'s Storage").transform;

            storage.transform.SetParent(transform);

            for (byte count = 0; count < 30; ++count)
            {
                var newEnemy = Instantiate(original);

                newEnemy.name = original.name;
                newEnemy.home = this;
                newEnemy.transform.SetParent(storage);
                newEnemy.transform.position = transform.position;
                newEnemy.transform.rotation = transform.rotation;
                newEnemy.gameObject.SetActive(false);

                enemyBank.Add(newEnemy);
            }
        }
    }
    #endregion

    #region
    public EnemyCtrl GetEnemy(EnemyType type, Vector3 spawnPos, Quaternion rotate)
    {
        var nowEnemy = GetEnemyInBank(type);

        if (!nowEnemy)
        {
            nowEnemy = Instantiate(GetEnemyInOriginals(type));

            nowEnemy.home = this;
            nowEnemy.transform.position = spawnPos;
            nowEnemy.transform.rotation = rotate;
        }

        else
        {
            enemyBank.Remove(nowEnemy);

            nowEnemy.transform.SetParent(null);
            nowEnemy.transform.position = spawnPos;
            nowEnemy.transform.rotation = rotate;
            nowEnemy.gameObject.SetActive(true);
        }

        return nowEnemy;
    }

    public void ReturnEnemy(EnemyCtrl usedEnemy)
    {
        usedEnemy.transform.SetParent(transform.Find($"{usedEnemy.name}'s Storage"));
        usedEnemy.transform.position    = transform.position;
        usedEnemy.transform.rotation    = Quaternion.identity;
        usedEnemy.gameObject.SetActive(false);

        enemyBank.Add(usedEnemy);
    }

    private EnemyCtrl GetEnemyInOriginals(EnemyType type) => type switch
    {
        EnemyType.VIRUS => originals.Find((enemy) => enemy is TypeVirus),
        EnemyType.CENCER => originals.Find((enemy) => enemy is TypeCencer),
        EnemyType.GERM => originals.Find((enemy) => enemy is TypeGerm),
        EnemyType.CORONA => originals.Find((enemy) => enemy is TypeCorona),
        _ => null,
    };

    private EnemyCtrl GetEnemyInBank(EnemyType type) => type switch
    {
        EnemyType.VIRUS => enemyBank.Find((enemy) => enemy is TypeVirus),
        EnemyType.CENCER => enemyBank.Find((enemy) => enemy is TypeCencer),
        EnemyType.GERM => enemyBank.Find((enemy) => enemy is TypeGerm),
        EnemyType.CORONA => enemyBank.Find((enemy) => enemy is TypeCorona),
        _ => null,
    };
    #endregion
}
