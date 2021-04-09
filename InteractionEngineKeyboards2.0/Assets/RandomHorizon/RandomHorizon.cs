using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[ExecuteInEditMode]
public class RandomHorizon : MonoBehaviour
{
    public Material meshMaterial;
    public Mesh meshPrefab;

    [OnValueChanged("RegenerateHillMatrices")]
    public float radius = 50;

    [OnValueChanged("RegenerateHillMatrices")]
    public float maxScale = 10;

    [OnValueChanged("RegenerateHillMatrices")]
    public float noiseScale = 10;

    [OnValueChanged("RegenerateHillMatrices")]
    public int count = 100;

    private List<Matrix4x4> hillMatrices;

    // Start is called before the first frame update
    void Start()
    {
        RegenerateHillMatrices();
    }

    private void RegenerateHillMatrices()
    {
        hillMatrices = new List<Matrix4x4>();
        for(int i = 0; i < count; i++)
        {
            
            float t = (i / (float)count) * Mathf.PI * 2;
            Vector3 position = new Vector3() {
                x = Mathf.Cos(t) * radius,
                y = 0,
                z = Mathf.Sin(t) * radius
            };
            Quaternion rotation = Quaternion.Euler(Random.Range(0,180), Random.Range(0,180), Random.Range(0,180));
            float scale = Mathf.PerlinNoise(position.x * noiseScale, position.z * noiseScale) * maxScale;
            hillMatrices.Add(Matrix4x4.TRS(position, rotation, new Vector3(scale,scale,scale)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.DrawMeshInstanced(meshPrefab, 0, meshMaterial, hillMatrices.ToArray(), hillMatrices.Count, null, UnityEngine.Rendering.ShadowCastingMode.Off, false);   
    }
}
