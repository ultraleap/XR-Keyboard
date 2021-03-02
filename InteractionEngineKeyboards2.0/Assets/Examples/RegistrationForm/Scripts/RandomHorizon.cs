using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RandomHorizon : MonoBehaviour
{
    public GameObject prefab;

    public float radius = 50;

    [MinMaxSlider(1, 100)]
    public Vector2 scaleRange = new Vector2(1,10);

    public int count = 100;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < count; i++)
        {
            float t = (i / (float)count) * Mathf.PI * 2;
            Vector3 position = new Vector3() {
                x = Mathf.Cos(t) * radius,
                y = 0,
                z = Mathf.Sin(t) * radius
            };
            Quaternion rotation = Quaternion.Euler(Random.Range(0,180), Random.Range(0,180), Random.Range(0,180));
            GameObject clone = Instantiate(prefab, transform.position + position, rotation, transform);
            float scale = Random.Range(scaleRange.x, scaleRange.y);

            clone.transform.localScale = new Vector3(scale,scale,scale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
