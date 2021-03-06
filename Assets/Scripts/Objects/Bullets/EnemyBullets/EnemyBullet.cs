using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyBullet : BaseBullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "RedCell")
        {
            home.ReturnBullet(this);
        }
    }
}
