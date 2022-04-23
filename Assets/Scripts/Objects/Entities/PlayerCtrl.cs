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

    [field: Header("")]
    [field: SerializeField] public ConditionType    curCondition    { get; private set; } = ConditionType.READY;
    [field: SerializeField] public byte             playerLv        { get; private set; } = 1;

    [field: SerializeField] private float curAttackTime { get; set; } = 0.0f;
    [field: SerializeField] private float maxAttackTime { get; set; } = 0.0f;
    #endregion

    #region
    protected override void SetEntityDatas()
    {
        entityHP = 5.0f;
        moveSpeed = 10.0f;
        ATK = 1.0f;

        myMagazine = TryGetComponent(out BulletFactory bf) ? GetComponent<BulletFactory>() : gameObject.AddComponent<BulletFactory>();
    }

    protected override void OnEnabledEntity()
    {
        return;
    }

    protected override void OnUpdatedEntity()
    {
        PlayerMove();
        PlayerAttack();
    }

    protected override void OnDisabledEntity()
    {
        curCondition = ConditionType.DEAD;
    }
    #endregion

    #region
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
                //var newBullet = myMagazine.GetObject(transform.position, 3, Quaternion.identity);
                Debug.Log("ÃÑ¾Ë ¹ß»ç");
                curAttackTime = 0.0f;
            }
        }

        else
        {
            curAttackTime = 0.0f;
        }
    }

    public void GetHP(float value)
    {
        SoundManager.GetInstance().PlaySFX("SFX_PlayerHealed");
        entityHP = value < maxPlayerHP ? value : maxPlayerHP;
    }

    public void GetLv(byte value)
    {
        SoundManager.GetInstance().PlaySFX("SFX_PlayerLvUp");
        playerLv = value < maxPlayerLv ? value : maxPlayerLv;
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