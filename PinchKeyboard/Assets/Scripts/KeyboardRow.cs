using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRow : MonoBehaviour
{
    [Range(0, 2)]
    public float spacing;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            float multiplier = transform.childCount - i;
            float width = child.gameObject.GetComponent<BoxCollider>().bounds.size.x;

            child.position = new Vector3((width * multiplier) + (spacing * width), child.position.y, child.position.z);
        }
    }
}
