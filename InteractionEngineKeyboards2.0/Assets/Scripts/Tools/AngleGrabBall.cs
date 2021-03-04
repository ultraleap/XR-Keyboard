using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;


[RequireComponent(typeof(InteractionBehaviour))]
public class AngleGrabBall : MonoBehaviour
{
    public Transform targetObject;
    public Transform grabGimbal;
    public float lerpSpeed = 20;

    public float startAngle = 35;

    public Vector3 localOffset;

    public Mesh dotMesh;
    public int dotCount = 64;
    public Vector2 minMaxDotSize = new Vector2(0.001f, 0.01f);
    public Material material;
    public Transform rotator;
    private int drawDotCount;
    private InteractionBehaviour behaviour;

    // Start is called before the first frame update
    void Start()
    {
        startAngle = Vector3.SignedAngle(Vector3.up, targetObject.up, targetObject.right);
        behaviour = GetComponent<InteractionBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        rotator.rotation = targetObject.rotation;

        if (!behaviour.isGrasped)
        {
            Vector3 targetPosition = targetObject.TransformPoint(localOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            HideRotationGizmo();
        }
        else
        {
            ShowRotationGizmo();
        }
        if (drawDotCount > 0) DrawRotationGizmo();
    }

    public void UpdateAngle()
    {
        Vector3 targetPosition = grabGimbal.InverseTransformPoint(transform.position);
        targetPosition.x = 0;
        Vector3 targetDirection = targetPosition - grabGimbal.InverseTransformPoint(targetObject.position);
        Quaternion rotation = Quaternion.FromToRotation(grabGimbal.up, targetDirection);
        targetObject.localRotation = rotation;
    }

    private void HideRotationGizmo()
    {
        if (drawDotCount > 0) drawDotCount--;
    }

    private void ShowRotationGizmo()
    {
        if (drawDotCount < dotCount) drawDotCount++;
    }

    private void DrawRotationGizmo()
    {
        List<Matrix4x4> matrices = new List<Matrix4x4>();

        float currentRotationTime = (Vector3.SignedAngle(Vector3.up, targetObject.up, targetObject.right) - startAngle) / 360;
        float dir = Mathf.Sign(currentRotationTime);
        
        currentRotationTime = Mathf.Abs(currentRotationTime);

        for (int i = 0; i < drawDotCount; i++)
        {
            float time = (i / (float)dotCount) ;
            Vector3 position = targetObject.TransformPoint( new Vector3() {
                x = localOffset.x,
                y = Mathf.Cos(time * 2 * Mathf.PI) * localOffset.y,
                z = -dir * Mathf.Sin(time * 2 * Mathf.PI) * localOffset.y
            });

            bool innerAngle = time > currentRotationTime;


            float scale = Mathf.Lerp(minMaxDotSize.x, minMaxDotSize.y, Mathf.Abs(time - currentRotationTime) * 8);
            if (time <= currentRotationTime)
            {
                matrices.Add(Matrix4x4.TRS(position, targetObject.rotation, new Vector3(scale, scale, scale)));
            }
        }

        Graphics.DrawMeshInstanced(dotMesh, 0, material, matrices.ToArray(), matrices.Count, null, UnityEngine.Rendering.ShadowCastingMode.Off, false);
    }
}
