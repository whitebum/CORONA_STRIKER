using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Base Factory's Base Datas
    [Header("Base Factory's Base Datas")]
    [SerializeField] protected T[] originals = null;
    [SerializeField] protected byte initCount = 0;
    [SerializeField] protected List<GameObject> objectBank = null;
    #endregion

    #region Base Factory's Base Methods
    protected virtual GameObject CreateObject(T original)
    {
        var newObj = Instantiate(original) as GameObject;

        return newObj;
    }

    public GameObject GetObject(Vector3 spawnPos, float scale, Quaternion rotate)
    {
        if (objectBank.Count <= 0)
        {
            return null;
        }

        var newObj = objectBank[0];

        objectBank.Remove(newObj);

        newObj.transform.SetParent(null);
        newObj.transform.localScale *= scale;
        newObj.transform.rotation   = rotate;
        newObj.gameObject.SetActive(true);

        return newObj;
    }

    public void ReturnObject(T usedObject)
    {
        usedObject.transform.SetParent(null);
        usedObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        usedObject.transform.rotation   = Quaternion.identity;
        usedObject.gameObject.SetActive(false);

        objectBank.Add(usedObject.gameObject);
    }
    #endregion
}