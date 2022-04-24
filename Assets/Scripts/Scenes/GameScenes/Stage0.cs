using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Stage0 : GameScene
{
    [field: SerializeField] private EnemyFactory     myEnemy      { get; set; } = null;

    private void Start()
    {
        myEnemy.GetComponent<EnemyFactory>();
        var newEnemy = myEnemy.GetEnemy(EnemyType.RED,new Vector3(0,0,0), Quaternion.identity);
    }
}
