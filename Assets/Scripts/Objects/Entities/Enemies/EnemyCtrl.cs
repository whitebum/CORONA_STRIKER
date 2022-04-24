using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCtrl : BaseEntity
{
    public enum ConditionType
    {
        READY,
        IDLE,
        DEAD,
    }

    #region Enemy's Expension Datas
    [Header("Enemy's Expension Datas")]
    [SerializeField] protected ConditionType    curConditon         = ConditionType.READY;
    [SerializeField] protected Transform        myTarget            = null;
    [SerializeField] protected float            attackIntervalTime  = 0.0f;
    [SerializeField] protected uint             dropScore           = 0;
    [SerializeField] protected float            painAmount          = 0.0f;
    #endregion

    #region Unity Messages
    private void OnBecameVisible()
    {
        if (curConditon == ConditionType.READY)
        {
            curConditon = ConditionType.IDLE;

            StartCoroutine(AttackEnemy());
        }
    }

    private void OnBecameInvisible()
    {
        // IDLE인 상태로 화면 밖으로 나갔을 경우의 처리.
        if (curConditon == ConditionType.IDLE)
        {
            curConditon = ConditionType.DEAD;

            // 고통 게이지 상승

            EnemyFactory.GetInstance().ReturnEnemy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (curConditon != ConditionType.DEAD)
        {
            if (collision.tag == "PlayerBullet")
            {
                StartCoroutine(OnDamagedEntity(1.0f));
            }
        }
    }
    #endregion

    #region Enemy's Overrided Methods
    protected override void SetEntityDatas()
    {
        tag = "Enemy";

        SetEnemyDatas();
    }

    protected override void OnEnabledEntity()
    {
        
    }

    protected override void OnUpdatedEntity()
    {
        MoveEmemy();
    }

    protected override void OnDisabledEntity()
    {
        EnemyFactory.GetInstance().ReturnEnemy(this);
    }

    protected override IEnumerator OnDamagedEntity(float damage)
    {
        entityHP -= damage;

        if (entityHP <= 0.0f)
        {
            curConditon = ConditionType.DEAD;

            entityAnim.SetTrigger("Enemy Dead");
            yield return new WaitForSeconds(entityAnim.GetCurrentAnimatorClipInfo(0).Length);

            EnemyFactory.GetInstance().ReturnEnemy(this);
        }

        else
        {
            var r = entityRenderer.color.r;
            var g = entityRenderer.color.g;
            var b = entityRenderer.color.b;

            entityRenderer.color = new Color(r, g, b, 0.5f);
            yield return new WaitForSeconds(0.2f);
            entityRenderer.color = new Color(r, g, b, 1.0f);
        }

        yield break;
    }
    #endregion

    #region Enemy's Base Methods
    protected abstract void SetEnemyDatas();
    protected abstract void MoveEmemy();
    protected abstract IEnumerator AttackEnemy();
    #endregion
}
