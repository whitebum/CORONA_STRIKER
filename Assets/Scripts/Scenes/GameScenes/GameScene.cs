using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameScene : BaseScene
{
    public enum ConditionType
    {
        READY,
        ON,
        OVER,
    }

    #region
    [field: Header("Game Stage's Base Datas")]
    [field: SerializeField] public float gameTime { get; private set; } = 0.0f;
    [SerializeField] protected ConditionType curCondition = ConditionType.READY;
    [SerializeField] protected bool isBossAppeared = false;
    [field: SerializeField] public byte killCount { get; set; }

    [field: SerializeField] protected EnemyFactory enemySpawner { get; private set; }

    #endregion

    #region
    protected override void InitSceneData()
    {
        enemySpawner = GetComponent<EnemyFactory>() ?? gameObject.AddComponent<EnemyFactory>();
    }

    protected override void OnStartedScene()
    {
        SoundManager.GetInstance().PlayBGM($"BGM_{GetType()}", true);

        StartCoroutine(StageIntroCoroutine());
    }

    protected override void OnUpdatedScene()
    {
        switch (curCondition)
        {
            case ConditionType.READY:
                {

                }
                break;
            case ConditionType.ON:
                {
                    gameTime += Time.deltaTime;
                    
                    CheckBossPhase();
                    CheckGameClear();
                }
                break;
            case ConditionType.OVER:
                {

                }
                break;
        }
    }
    #endregion

    #region
    private IEnumerator StageIntroCoroutine()
    {
        curCondition = ConditionType.ON;
        yield break;
    }

    private IEnumerator StageClearCoroutine()
    {
        curCondition = ConditionType.OVER;
        yield break;
    }

    private IEnumerator StageFailCoroutine()
    {
        curCondition = ConditionType.OVER;
        yield break;
    }

    private void CheckBossPhase()
    {
        if (!isBossAppeared)
        {
            if (gameTime >= 0.0f || killCount <= 0)
            {
                enemySpawner.GetEnemy(EnemyType.CORONA, new Vector3(), Quaternion.identity);
                isBossAppeared = true;
            }
        }
    }

    private void CheckGameClear()
    {
        if (isBossAppeared)
        {
            var bossObj = FindObjectOfType<TypeCorona>();

            if (!bossObj)
            {

            }
        }
    }
    #endregion
}
