using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    NavMeshAgent agent;

    bool isIdle;


    
    [SerializeField] float thresholdDistance = 0.5f;
    [SerializeField] float minIdleTime = 2f;
    [SerializeField] float maxIdleTime = 7f;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Invoke("SetEnemyPatrolPoint", 2);
    }

    private void Update()
    {
        if (agent.remainingDistance <= thresholdDistance && isIdle)
        {
            isIdle = false;

            Invoke("SetEnemyPatrolPoint", Random.Range(minIdleTime, maxIdleTime));
        }
    }

    void SetEnemyPatrolPoint()
    {
        agent.SetDestination(GetRandomPointOnNavMesh());
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int randomTriangleIndex = Random.Range(0, navMeshData.indices.Length / 3) * 3;

        Vector3 vert1 = navMeshData.vertices[navMeshData.indices[randomTriangleIndex]];
        Vector3 vert2 = navMeshData.vertices[navMeshData.indices[randomTriangleIndex + 1]];
        Vector3 vert3 = navMeshData.vertices[navMeshData.indices[randomTriangleIndex + 2]];

        float u = Random.value;
        float v = Random.value;

        if (u + v > 1)
        {
            u = 1 - u;
            v = 1 - v;
        }

        Vector3 randomPoint = vert1 + u * (vert2 - vert1) + v * (vert3 - vert1);

        isIdle = true;

        return randomPoint;
    }
}