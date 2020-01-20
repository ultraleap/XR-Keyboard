using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFixer : MonoBehaviour
{
    [ContextMenu("Fix Scale")]
    public void FixScale()
    {
        List<Transform> keys = new List<Transform>();
        foreach (Transform key in transform)
        {
            keys.Add(key);
        }

        foreach(Transform key in keys)
        {
            GameObject keyBase = new GameObject
            {
                name = key.name + " Base"
            };

            keyBase.transform.parent = transform;
            keyBase.transform.localScale = key.localScale;
            keyBase.transform.position = key.position;
            keyBase.transform.rotation = key.rotation;
            key.parent = keyBase.transform;
            key.localScale = keyBase.transform.localScale;
            keyBase.transform.localScale = Vector3.one;


        }
    }
}
