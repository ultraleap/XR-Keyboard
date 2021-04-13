using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Based off of https://github.com/lormori/FlockingDemo

public class FlockingAgent : MonoBehaviour
{
    private readonly float radiusSquaredDistance = 5.0f;
    private readonly float maxVelocity = 1.0f;
    public int ID = 0;
    private Vector3 velocity = new Vector3();

    void Start()
    {
        velocity = transform.forward;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
    }

    void Update()
    {
        velocity += FlockingBehaviour();

        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);

        transform.position += velocity * Time.deltaTime;

        transform.forward = velocity.normalized;

        Reposition();
    }

    private void Reposition()
    {
        Vector3 boxColliderSize = Flocking.instance.GetComponent<BoxCollider>().size;
        Vector3 flockingManagerPosition = Flocking.instance.transform.position;
        Vector3 maxExtents = flockingManagerPosition + (boxColliderSize/2);
        Vector3 minExtents = flockingManagerPosition - (boxColliderSize/2);


        Vector3 position = transform.position;

        if (position.x >= maxExtents.x)
        {
            position.x = maxExtents.x - 0.2f;
            velocity.x *= -1;
        }
        else if (position.x <= minExtents.x)
        {
            position.x = minExtents.x + 0.2f;
            velocity.x *= -1;
        }

        if (position.y >= maxExtents.y)
        {
            position.y = maxExtents.y - 0.2f;
            velocity.y *= -1;
        }
        else if (position.y <= minExtents.y)
        {
            position.y = minExtents.y + 0.2f;
            velocity.y *= -1;
        }

        if (position.z >= maxExtents.z)
        {
            position.z = maxExtents.z - 0.2f;
            velocity.z *= -1;
        }
        else if (position.z <= minExtents.z)
        {
            position.z = minExtents.z + 0.2f;
            velocity.z *= -1;
        }

        transform.forward = velocity.normalized;
        transform.position = position;
    }

    private Vector3 FlockingBehaviour()
    {
        List<FlockingAgent> theFlock = Flocking.instance.theFlock;

        Vector3 cohesionVector = new Vector3();
        Vector3 separateVector = new Vector3();
        Vector3 alignmentVector = new Vector3();

        int count = 0;

        for (int index = 0; index < theFlock.Count; index++)
        {
            if (ID != theFlock[index].ID)
            {
                float distance = (transform.position - theFlock[index].transform.position).sqrMagnitude;

                if (distance > 0 && distance < radiusSquaredDistance)
                {
                    cohesionVector += theFlock[index].transform.position;
                    separateVector += theFlock[index].transform.position - transform.position;
                    alignmentVector += theFlock[index].transform.forward;

                    count++;
                }
            }
        }

        if (count == 0)
        {
            return Vector3.zero;
        }

        // revert vector
        // separation step
        separateVector /= count;
        separateVector *= -1;

        // forward step
        alignmentVector /= count;

        // cohesion step
        cohesionVector /= count;
        cohesionVector -= transform.position;

        // Add All vectors together to get flocking
        Vector3 flockingVector = ((separateVector.normalized * Flocking.instance.separationWeight) +
                                    (cohesionVector.normalized * Flocking.instance.cohesionWeight) +
                                    (alignmentVector.normalized * Flocking.instance.alignmentWeight));

        return flockingVector;
    }
}