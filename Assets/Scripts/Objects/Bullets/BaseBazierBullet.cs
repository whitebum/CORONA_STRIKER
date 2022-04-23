using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBazierBullet : BaseBullet
{
    #region
    [field: Header("Bazier Bullet's Expension Datas")]
    [field: SerializeField] public Transform myTarget { get; set; } = null;

    [field: Space(5.0f)]
    [field: SerializeField] private Vector3[]   checkPoints { get; set; } = new Vector3[4];
    [field: SerializeField] private float       maxMoveTime { get; set; } = 0.0f;
    [field: SerializeField] private float       curMoveTime { get; set; } = 0.0f;
    #endregion

    #region Unity Message
    private void Start()
    {
        maxMoveTime = Random.Range(0.8f, 1.0f);

        checkPoints[0] = home.transform.position;

        checkPoints[1] = checkPoints[0] +
            (6.0f * Random.Range(-1.0f, 1.0f) * home.transform.right) +
            (6.0f * Random.Range(-1.0f, 1.0f) * home.transform.up) +
            (6.0f * Random.Range(-1.0f, 1.0f) * home.transform.forward);

        checkPoints[2] = checkPoints[0] +
           (3.0f * Random.Range(-1.0f, 1.0f) * myTarget.transform.right) +
           (3.0f * Random.Range(-1.0f, 1.0f) * myTarget.transform.up) +
           (3.0f * Random.Range(-1.0f, 1.0f) * myTarget.transform.forward);

        checkPoints[3] = myTarget.transform.position;
    }
    #endregion

    #region
    protected override void MoveBullet()
    {
        if (curMoveTime > maxMoveTime)
        {
            home.ReturnObject(this);
        }

        curMoveTime += Time.deltaTime * moveSpeed;

        transform.position = new Vector3(
            SetBazierCurve(checkPoints[0].x, checkPoints[1].x, checkPoints[2].x, checkPoints[3].x),
            SetBazierCurve(checkPoints[0].y, checkPoints[1].y, checkPoints[2].y, checkPoints[3].y),
            SetBazierCurve(checkPoints[0].z, checkPoints[1].z, checkPoints[2].z, checkPoints[3].z)
        );
    }

    private float SetBazierCurve(float a, float b, float c, float d)
    {
        var time = curMoveTime / maxMoveTime;

        float ab = Mathf.Lerp(a, b, time);
        float bc = Mathf.Lerp(b, c, time);
        float cd = Mathf.Lerp(c, d, time);

        float abbc = Mathf.Lerp(ab, bc, time);
        float bccd = Mathf.Lerp(bc, cd, time);

        return Mathf.Lerp(abbc, bccd, time);
    }
    #endregion
}
