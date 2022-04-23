using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    #region Bullet's Base Datas
    [field: Header("Bullet's Base Datas")]
    [field: SerializeField] protected BulletFactory   home        { get; private set; } = null;
    [field: SerializeField] protected float           moveSpeed   { get; private set; } = 0.0f;
    #endregion

    #region Unity Messages
    private void OnEnable()
    {
        SetBulletData();
    }

    private void Update()
    {
        MoveBullet();
    }

    private void OnBecameInvisible()
    {
        home.ReturnObject(this);
    }
    #endregion

    #region Bullet's Movement Method
    protected virtual void SetBulletData()
    {
        moveSpeed = home.owner.bulletSpeed;
    }

    protected virtual void MoveBullet()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
    #endregion
}
