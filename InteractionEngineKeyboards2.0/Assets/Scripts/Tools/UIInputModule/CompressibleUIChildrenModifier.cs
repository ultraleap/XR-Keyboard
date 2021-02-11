using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity.InputModule;
using NaughtyAttributes;
using UnityEngine;

public class CompressibleUIChildrenModifier : MonoBehaviour
{
    public List<Vector2> minMaxLayerFloatDistances;
    public float ExpandSpeed = 0.2f;
    public float ContractSpeed = 0.2f;
    public float PushPaddingDistance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAllChildren();
    }

    [Button]
    private void UpdateAllChildren()
    {
        List<CompressibleUI> compressibleUIs = GetComponentsInChildren<CompressibleUI>().ToList();

        foreach (CompressibleUI compressibleUI in compressibleUIs)
        {
            for (int i = 0; i < minMaxLayerFloatDistances.Count; i++)
            {
                if (compressibleUI.Layers.Length == i) { break; }
                compressibleUI.Layers[i].MinFloatDistance = minMaxLayerFloatDistances[i].x;
                compressibleUI.Layers[i].MaxFloatDistance = minMaxLayerFloatDistances[i].y;
            }
            compressibleUI.ExpandSpeed = ExpandSpeed;
            compressibleUI.ContractSpeed = ContractSpeed;
            compressibleUI.PushPaddingDistance = PushPaddingDistance;
        }
    }
}
