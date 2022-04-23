using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Base Factory's Base Datas
    [field: Header("Base Factory's Base Datas")]
    [field: SerializeField] protected T[] originals { get; set; } = null;
    [field: SerializeField] protected byte initCount { get; set; } = 0;
    [field: SerializeField] private List<GameObject> objectBank { get; set; } = null;
    #endregion

    #region Unity Messages
    private void Awake()
    {
        SetFactoryData();

        foreach (var original in originals)
        {
            var storage = new GameObject($"{original.GetType()} Storage").transform;

            storage.transform.SetParent(transform);

            for (byte count2 = 0; count2 < initCount; ++count2)
            {
                objectBank.Add(CreateObject(original, storage));
            }
        }
    }
    #endregion

    #region Base Factory's Base Methods
    protected abstract void SetFactoryData();

    private GameObject CreateObject(T original, Transform storage)
    {
        var newObj = Instantiate(original) as GameObject;

        newObj.transform.SetParent(storage);
        newObj.gameObject.SetActive(false);

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