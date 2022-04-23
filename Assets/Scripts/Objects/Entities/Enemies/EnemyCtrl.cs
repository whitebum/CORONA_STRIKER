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
    [field: Header("Enemy's Expension Datas")]
    [field: SerializeField] protected EnemyFactory      home        { get; set; } = null;
    [field: SerializeField] protected ConditionType     curConditon { get; set; } = ConditionType.READY;
    [field: SerializeField] protected Transform         myTarget    { get; set; } = null;
    [field: SerializeField] protected uint              dropScore   { get; set; } = 0;
    [field: SerializeField] protected float             painAmount  { get; set; } = 0.0f;
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

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            var damage = collision.GetComponent<BaseBullet>().damage;

            StartCoroutine(OnDamagedEntity(damage));
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
        myTarget = FindObjectOfType<BaseEntity>().transform;
    }

    protected override void OnUpdatedEntity()
    {
        MoveEmemy();
    }

    protected override void OnDisabledEntity()
    {
        home.ReturnObject(this);
    }

    protected override IEnumerator OnDamagedEntity(float damage)
    {
        entityHP -= damage;

        if (entityHP <= 0.0f)
        {
            curConditon = ConditionType.DEAD;

            entityAnims[1].Play();
            yield return new WaitForSeconds(entityAnims[1].clip.length);

            gameObject.SetActive(false);
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
