using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    #region Entity's Base Datas
    [field: Header("Entity's Base Datas")]
    [field: SerializeField] public float        entityHP        { get; protected set; } = 0.0f;
    [field: SerializeField] public float        moveSpeed       { get; protected set; } = 0.0f;
    [field: SerializeField] public float        ATK             { get; protected set; } = 0.0f;
    [field: SerializeField] public float        bulletSpeed     { get; protected set; } = 0.0f;
    [field: SerializeField] public BaseBullet[] myBullets       { get; protected set; } = null;
    
    [field: Space(5.0f)]
    [field: SerializeField] protected BulletFactory     myMagazine      { get; set; } = null;
    [field: SerializeField] protected SpriteRenderer    entityRenderer  { get; set; } = null;
    [field: SerializeField] protected Animation[]       entityAnims     { get; set; } = null;
    #endregion

    #region Unity Messages
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Entity");

        entityRenderer  = GetComponent<SpriteRenderer>();

        SetEntityDatas();
    }

    private void OnEnable()
    {
        OnEnabledEntity();
    }

    private void Update()
    {
        OnUpdatedEntity();
    }

    private void OnDisable()
    {
        OnDisabledEntity();
    }
    #endregion

    #region Entity's Base Methods
    protected abstract void SetEntityDatas();
    protected abstract void OnEnabledEntity();
    protected abstract void OnUpdatedEntity();
    protected abstract void OnDisabledEntity();
    protected abstract IEnumerator OnDamagedEntity(float damage);
    #endregion
}