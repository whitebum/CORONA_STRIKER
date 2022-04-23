using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    RED,
    YELLOW,
    GREEN,
    BOSS,
}

public sealed class EnemyFactory : MonoBehaviour
{
    #region Enemy Factory's Managed Datas
    [field: Header("Enemy Factory's Managed Datas")]
    [field: SerializeField] public GameScene owner { get; private set; } = null;

    [SerializeField] private List<EnemyCtrl> originals = null;
    [SerializeField] private List<EnemyCtrl> enemyBank = null;
    #endregion

    #region Unity Message
    private void Awake()
    {
        owner = GetComponent<GameScene>();

        originals   = new List<EnemyCtrl>(Resources.LoadAll<EnemyCtrl>(""));
        enemyBank   = new List<EnemyCtrl>();

        foreach (var original in originals)
        {
            var storage = new GameObject($"{original.name} Storage").transform;

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

    private EnemyCtrl GetEnemyInBank(EnemyType type) => type switch
    {
        EnemyType.GREEN => enemyBank.Find((enemy) => enemy is GreenEnemy),
        EnemyType.YELLOW => enemyBank.Find((enemy) => enemy is /*YellowEnemy*/GreenEnemy),
        EnemyType.RED => enemyBank.Find((enemy) => enemy is RedEnemy),
        EnemyType.BOSS => enemyBank.Find((enemy) => enemy is /*BossEnemy*/RedEnemy),
        _ => null,
    };

    private EnemyCtrl GetEnemyInOriginals(EnemyType type) => type switch
    {
        EnemyType.GREEN => originals.Find((enemy) => enemy is GreenEnemy),
        EnemyType.YELLOW => originals.Find((enemy) => enemy is /*YellowEnemy*/GreenEnemy),
        EnemyType.RED => originals.Find((enemy) => enemy is RedEnemy),
        EnemyType.BOSS => originals.Find((enemy) => enemy is/*BossEnemy*/RedEnemy),
        _ => null,
    };
    #endregion
}
