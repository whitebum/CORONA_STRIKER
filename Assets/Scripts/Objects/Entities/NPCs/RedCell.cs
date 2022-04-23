using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RedCell : BaseEntity
{
    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
            case "PlayerBullet":
                {
                    gameObject.SetActive(false);
                }
                break;
            case "Enemy":
            case "EnemyBullet":
                {
                    gameObject.SetActive(false);
                }
                break;
        }
    }
    #endregion

    #region
    protected override void SetEntityDatas()
    {
        
    }

    protected override void OnEnabledEntity()
    {
        return;
    }

    protected override void OnUpdatedEntity()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    protected override void OnDisabledEntity()
    {
        return;
    }

    protected override IEnumerator OnDamagedEntity(float damage)
    {
        yield break;
    }
    #endregion
}
