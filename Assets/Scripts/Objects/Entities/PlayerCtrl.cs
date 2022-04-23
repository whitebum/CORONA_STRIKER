using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerCtrl : BaseEntity
{
    public enum ConditionType
    {
        READY,
        IDLE,
        DAMAGED,
        INVINCIBILITY,
        DEAD,
    }

    #region
    private const float maxPlayerHP = 5.0f;
    private const byte  maxPlayerLv = 5;

    [field: Header("Player's Expension Datas")]
    [field: SerializeField] public ConditionType    curCondition    { get; private set; } = ConditionType.READY;
    [field: SerializeField] public byte             playerLv        { get; private set; } = 1;

    [field: SerializeField] private float curAttackTime { get; set; } = 0.0f;
    [field: SerializeField] private float maxAttackTime { get; set; } = 0.0f;
    #endregion

    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                {
                    StartCoroutine(OnDamagedEntity(0.25f));
                }
                break;
            case "EnemyBullet":
                {
                    StartCoroutine(OnDamagedEntity(0.5f));
                }
                break;
            case "Item":
                {

                }
                break;
        }
    }
    #endregion

    #region
    protected override void SetEntityDatas()
    {
        tag         = "Player";

        entityHP    = maxPlayerHP;
        moveSpeed   = 10.0f;
        bulletSpeed = 30.0f;

        myBullets   = Resources.LoadAll<BaseBullet>("Prefabs/Bullets/Player/BlueBullet");
        myMagazine  = GetComponent<BulletFactory>();

        entityAnim.SetFloat("Player HP", entityHP);
    }

    protected override void OnEnabledEntity()
    {
        
    }

    protected override void OnUpdatedEntity()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine(OnDamagedEntity(0.5f));
        }

        PlayerMove();
        PlayerAttack();
    }

    protected override void OnDisabledEntity()
    {
        curCondition = ConditionType.DEAD;
    }

    protected override IEnumerator OnDamagedEntity(float damage)
    {
        if (curCondition != ConditionType.READY ||
            curCondition != ConditionType.DAMAGED || 
            curCondition != ConditionType.DEAD)
        {
            entityHP -= damage;

            if (entityHP <= 0.0f)
            {
                curCondition = ConditionType.DEAD;

                entityAnim.SetTrigger("Player Dead");

                yield return new WaitForSeconds(entityAnim.GetCurrentAnimatorStateInfo(0).length);

                gameObject.SetActive(false);
            }

            else
            {
                SoundManager.GetInstance().PlaySFX("SFX_PlayerHealed");

                entityAnim.SetFloat("Player HP", entityHP);

                var r = entityRenderer.color.r;
                var g = entityRenderer.color.g;
                var b = entityRenderer.color.b;

                for (byte count = 0; count < 4; ++count)
                {
                    entityRenderer.color = new Color(r, g, b, 0.5f);
                    yield return new WaitForSeconds(0.2f);
                    entityRenderer.color = new Color(r, g, b, 1.0f);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
    }
    #endregion

    #region 
    public void GetHP(float value)
    {
        SoundManager.GetInstance().PlaySFX("SFX_PlayerHealed");

        entityHP = value < maxPlayerHP ? value : maxPlayerHP;
        entityAnim.SetFloat("Player HP", entityHP);
    }

    public void GetHP(byte value)
    {
        SoundManager.GetInstance().PlaySFX("SFX_PlayerLvUp");
        playerLv = value < maxPlayerLv ? value : maxPlayerLv;
    }

    private void PlayerMove()
    {
        var horizontal  = Input.GetAxisRaw("Horizontal");
        var vertical    = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(horizontal, vertical, 0.0f) * moveSpeed * Time.deltaTime);
    }

    private void PlayerAttack()
    {
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.LeftShift))
        {
            curAttackTime += Time.deltaTime;

            if (curAttackTime >= maxAttackTime)
            {
                var newBullet = myMagazine.GetBullet(BulletType.NORMAL, transform.position, 1, Quaternion.identity);
                curAttackTime = 0.0f;
            }
        }

        else
        {
            curAttackTime = 0.0f;
        }
    }
    #endregion
}

public static class PlayerRanking
{
    private static uint _maxPlayerScore = 999999;
    private static uint _playerScore = 0;

    public static uint playerScore
    {
        get
        {
            return _playerScore;
        }

        set
        {
            if (_playerScore > _maxPlayerScore)
            {
                value = _maxPlayerScore;
            }

            _playerScore = value;
        }
    }
}