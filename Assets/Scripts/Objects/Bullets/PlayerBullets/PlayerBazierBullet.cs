using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerBazierBullet : BaseBazierBullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "WhiteCell")
        {
            home.ReturnBullet(this);
        }
    }
}
