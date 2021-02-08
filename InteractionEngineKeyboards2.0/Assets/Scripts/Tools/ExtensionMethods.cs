using UnityEngine;
public static class ExtensionMethods
{
    public static Bounds CalculateActualBounds(this MeshRenderer meshRenderer)
    {
        Quaternion rot = meshRenderer.transform.rotation;

        meshRenderer.transform.rotation = Quaternion.identity;
        Physics.SyncTransforms();
        Bounds bounds = meshRenderer.bounds;
        meshRenderer.transform.rotation = rot;
        Physics.SyncTransforms();
        return bounds;
    }
}