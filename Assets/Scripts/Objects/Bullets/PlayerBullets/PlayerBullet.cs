using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerBullet : BaseBullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "WhiteCell")
        {
            home.ReturnObject(this);
        }
    }
}
