using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WhiteCell : BaseEntity
{
    #region
    bool isTouchedByEnemy = false;
    #endregion

    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
            case "PlayerBullet":
                {
                    isTouchedByEnemy = false;
                    StartCoroutine(OnDamagedEntity(0.0f));
                }
                break;
            case "Enemy":
            case "EnemyBullet":
                {
                    isTouchedByEnemy = true;
                    StartCoroutine(OnDamagedEntity(0.0f));
                }
                break;
        }
    }
    #endregion

    #region
    protected override void SetEntityDatas()
    {
        return;
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
        entityAnim.SetTrigger("Cell Dead");
        yield return new WaitForSeconds(entityAnim.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false);
    }
    #endregion
}
