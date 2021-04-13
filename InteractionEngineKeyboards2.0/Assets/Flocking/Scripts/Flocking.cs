using System.Collections.Generic;
using UnityEngine;

//Based off of https://github.com/lormori/FlockingDemo

public class Flocking : MonoBehaviour
{

    public int NumberOfEntities = 20;
    public FlockingAgent templatePrefab = null;
    [Range(0, 1)] public float separationWeight = 0.8f;
    [Range(0, 1)] public float alignmentWeight = 0.5f;
    [Range(0, 1)] public float cohesionWeight = 0.7f;


    public static Flocking instance = null;

    private List<FlockingAgent> flockingAgents = new List<FlockingAgent>();

    void Start()
    {
        instance = this;
        InstantiateFlock();
    }

    private void InstantiateFlock()
    {
        Vector3 boxColliderSize = GetComponent<BoxCollider>().size;
        Vector3 flockingManagerPosition = transform.position;
        Vector3 maxExtents = flockingManagerPosition + (boxColliderSize / 2);
        Vector3 minExtents = flockingManagerPosition - (boxColliderSize / 2);

        for (int i = 0; i < NumberOfEntities; i++)
        {
            Vector3 position = new Vector3()
            {
                x = Random.Range(minExtents.x, maxExtents.x),
                y = Random.Range(minExtents.y, maxExtents.y),
                z = Random.Range(minExtents.z, maxExtents.z)
            };

            FlockingAgent flockEntity = Instantiate(templatePrefab, position, Quaternion.identity);
            flockEntity.transform.parent = transform;
            flockEntity.transform.rotation = Random.rotation;

            flockEntity.ID = i;

            flockingAgents.Add(flockEntity);
        }
    }

    public List<FlockingAgent> theFlock
    {
        get { return flockingAgents; }
    }
}