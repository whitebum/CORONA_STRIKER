using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    #region Bullet's Base Datas
    [field: Header("Bullet's Base Datas")]
    [field: SerializeField] public BulletFactory home = null;

    public float moveSpeed { get => home.owner.bulletSpeed; }
    #endregion

    #region Unity Messages
    private void Update()
    {
        MoveBullet();
    }

    private void OnBecameInvisible()
    {
        home.ReturnBullet(this);
    }
    #endregion

    #region Bullet's Movement Method
    protected virtual void MoveBullet()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
    #endregion
}
